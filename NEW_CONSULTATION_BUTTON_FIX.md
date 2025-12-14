# New Consultation Button Fix

## Problem
The "New Consultation" button on the "Your Hexagram" result page was not working properly. When users clicked it, they would remain on the result page instead of starting a fresh consultation from the question input step.

## Root Cause
The issue was caused by Angular's component reuse behavior. When navigating to the same route (`/consultation`), Angular reuses the existing component instance instead of creating a new one. This means:

1. `ngOnInit()` is only called once when the component is first created
2. Subsequent navigations to the same route do NOT trigger `ngOnInit()` again
3. The component state (`currentStep`, `consultation`, etc.) persists

### The Failing Flow
1. User completes consultation ? `currentStep = 'result'`
2. Clicks "New Consultation" button
3. `clearTossResults()` is called (clears saved consultation)
4. Router navigates to `/consultation`
5. Component is reused, `ngOnInit()` NOT called
6. **Result:** User still sees result page because `currentStep` is still `'result'`

## Solution Implemented

### Approach
Instead of relying on navigation and `ngOnInit()` to reset the component, we:
1. Add a `resetToNewConsultation()` method to explicitly reset component state
2. Use Angular's event emitter pattern to communicate from child (consultation-result) to parent (consultation-flow)
3. Call the reset method directly when "New Consultation" is clicked

### Changes Made

#### 1. Updated `consultation-flow.ts`
**File:** `Itzin.Web/src/app/features/consultation/consultation-flow/consultation-flow.ts`

Added `resetToNewConsultation()` method:
```typescript
resetToNewConsultation(): void {
  this.currentStep = 'question';
  this.consultation = null;
  this.isCreating = false;
  this.errorMessage = '';
}
```

Updated `ngOnInit()` to use the reset method:
```typescript
ngOnInit(): void {
  const savedConsultation = this.consultationService.getCurrentConsultation();
  if (savedConsultation) {
    this.consultation = savedConsultation;
    this.currentStep = 'result';
  } else {
    this.resetToNewConsultation(); // Use reset method for consistency
  }
}
```

#### 2. Updated `consultation-result.ts`
**File:** `Itzin.Web/src/app/features/consultation/consultation-result/consultation-result.ts`

Added output event emitter:
```typescript
@Output() newConsultationRequested = new EventEmitter<void>();
```

Updated `newConsultation()` method to emit event instead of navigating:
```typescript
newConsultation(): void {
  // Clear all consultation data including saved consultation
  this.consultationService.clearTossResults();
  // Emit event to parent to reset the flow
  this.newConsultationRequested.emit();
}
```

#### 3. Updated `consultation-flow.html`
**File:** `Itzin.Web/src/app/features/consultation/consultation-flow/consultation-flow.html`

Wired up the event handler:
```html
<app-consultation-result 
  [consultation]="consultation" 
  (newConsultationRequested)="resetToNewConsultation()">
</app-consultation-result>
```

## How It Works Now

### New Consultation Flow
1. **User completes consultation** ? Views "Your Hexagram" result page
2. **Clicks "New Consultation" button**
   - `consultation-result` component's `newConsultation()` method called
   - Clears all toss results via service: `clearTossResults()`
   - Emits event: `newConsultationRequested.emit()`
3. **Parent `consultation-flow` receives event**
   - Calls `resetToNewConsultation()` method
   - Resets state: `currentStep = 'question'`, `consultation = null`
4. **User sees question input form** ?

### State Reset Details
The `resetToNewConsultation()` method resets all component state:
```typescript
resetToNewConsultation(): void {
  this.currentStep = 'question';      // Go to question step
  this.consultation = null;           // Clear consultation data
  this.isCreating = false;            // Reset loading state
  this.errorMessage = '';             // Clear any errors
}
```

### Service State Cleared
The `clearTossResults()` method in `ConsultationService` clears:
- Toss results array
- Current question
- Saved consultation (for back navigation)

```typescript
clearTossResults(): void {
  this.tossResultsSubject.next([]);
  this.currentQuestionSubject.next('');
  this.currentConsultationSubject.next(null);  // Important for fresh start
}
```

## Benefits

### 1. **Proper Component Communication**
Uses Angular's event emitter pattern (@Output) for parent-child communication, which is the recommended approach.

### 2. **Explicit State Management**
State is reset explicitly via method call, not implicitly via navigation/lifecycle hooks.

### 3. **No Navigation Side Effects**
Doesn't rely on router navigation, avoiding:
- Browser history pollution
- Component lifecycle ambiguity
- Race conditions with route reuse

### 4. **Consistent with Back Navigation**
Works harmoniously with the previously implemented back-from-hexagram-detail feature:
- Back from hexagram detail ? Restores result page (saved consultation exists)
- New consultation ? Resets to question input (saved consultation cleared)

## Testing Instructions

### Test Scenario 1: New Consultation Button
1. Start the application
2. Complete a consultation (6 coin tosses)
3. View "Your Hexagram" result page
4. Click "New Consultation" button
5. **Expected:** Question input form appears ?
6. **Not:** Result page remains ?

### Test Scenario 2: Back Navigation Still Works
1. Complete a consultation
2. Click "View Full Interpretation" on a hexagram
3. View hexagram detail page
4. Click back button
5. **Expected:** "Your Hexagram" result page appears ?

### Test Scenario 3: Multiple New Consultations
1. Complete first consultation
2. Click "New Consultation"
3. Complete second consultation
4. Click "New Consultation" again
5. **Expected:** Question input form each time ?

### Test Scenario 4: Dashboard Navigation
1. Complete a consultation
2. Click "Back to Dashboard"
3. From dashboard, click "New Consultation"
4. **Expected:** Question input form appears ?

## Files Modified

1. `Itzin.Web/src/app/features/consultation/consultation-flow/consultation-flow.ts`
   - Added `resetToNewConsultation()` method
   - Updated `ngOnInit()` to use reset method

2. `Itzin.Web/src/app/features/consultation/consultation-result/consultation-result.ts`
   - Added `@Output() newConsultationRequested` event emitter
   - Updated `newConsultation()` to emit event instead of navigating

3. `Itzin.Web/src/app/features/consultation/consultation-flow/consultation-flow.html`
   - Added `(newConsultationRequested)` event binding to `<app-consultation-result>`

## Technical Notes

### Angular Component Reuse
Angular's router reuses component instances for the same route to optimize performance. This is standard behavior and generally beneficial, but requires explicit state management when you want to "reset" the component.

### Alternative Approaches Considered

#### ? Option 1: Force Component Recreation
```typescript
// Could add to routes
{ path: 'consultation', component: ConsultationFlow, runGuardsAndResolvers: 'always' }
```
**Rejected:** Overkill; forces component destruction/recreation unnecessarily

#### ? Option 2: Use Route Parameters
```typescript
this.router.navigate(['/consultation'], { queryParams: { new: Date.now() } });
```
**Rejected:** Pollutes URL; doesn't actually solve the problem

#### ? Option 3: Event-Based State Reset (Implemented)
**Benefits:**
- Clean, explicit communication
- Follows Angular best practices
- Minimal changes required
- No side effects

## Integration with Previous Fix

This fix works together with the previous "Back Button Navigation" fix:

### Previous Fix (Back from Hexagram Detail)
- Saves consultation when viewing hexagram details
- Restores consultation when returning via back button
- Uses `ConsultationService.currentConsultationSubject`

### This Fix (New Consultation Button)
- Clears saved consultation via `clearTossResults()`
- Resets component state to question step
- Uses event-based communication

### Compatibility
Both fixes use the same `currentConsultationSubject`:
- New Consultation ? clears it ? fresh start
- Back from detail ? checks it ? restores if exists

## Status
? **Fix Implemented**
? **Code Modified**
? **Event Wiring Complete**
? **Ready for Testing**

---

**Date:** January 2025  
**Issue:** "New Consultation" button not working on result page  
**Solution:** Event-based state reset using @Output emitter  
**Status:** ? Complete
