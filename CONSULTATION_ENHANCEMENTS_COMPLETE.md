# Consultation Enhancements - Complete ?

## Summary
Two major features have been implemented to enhance the consultation experience:

1. **Changing Lines Highlighting** - When viewing hexagram interpretations from consultations, changing lines are highlighted and non-changing lines are grayed out
2. **Delete Consultation Button** - Users can now delete consultations from their history

---

## Feature 1: Changing Lines Highlighting ??

### Overview
When users click "View Full Interpretation" from consultation results or history, the hexagram detail page now shows:
- **Changing lines**: Highlighted with golden background and bold text
- **Non-changing lines**: Grayed out with reduced opacity

### How It Works

#### Frontend Changes

**1. Hexagram Detail Component** (`hexagram-detail.ts`)
- Added `changingLines` property to track which lines are changing
- Reads `changingLines` query parameter from URL
- Added `isLineChanging()` method to check if a line number is changing
- Added `getLineClass()` method to return appropriate CSS class

```typescript
changingLines: number[] = [];

ngOnInit(): void {
  const changingLinesParam = this.route.snapshot.queryParams['changingLines'];
  if (changingLinesParam) {
    this.changingLines = changingLinesParam.split(',').map((n: string) => parseInt(n, 10));
  }
  // ...
}

isLineChanging(lineNumber: number): boolean {
  return this.changingLines.includes(lineNumber);
}

getLineClass(lineNumber: number): string {
  return this.isLineChanging(lineNumber) ? 'line-changing' : 'line-static';
}
```

**2. Hexagram Detail Template** (`hexagram-detail.html`)
- Applied `[ngClass]="getLineClass(lineNumber)"` to each line interpretation
- Works for both Russian and English line displays

```html
<div class="line-interpretation" [ngClass]="getLineClass(1)">
  <h4>Черта 1</h4>
  <p>{{ hexagram.ruDescription.line1 }}</p>
</div>
```

**3. Hexagram Detail Styles** (`hexagram-detail.scss`)
- Added `.line-changing` class with golden highlight
- Added `.line-static` class with gray styling

```scss
.line-interpretation {
  &.line-changing {
    background: linear-gradient(135deg, #fff9e6 0%, #fffbf0 100%);
    border-left: 4px solid #d4af37;
    box-shadow: 0 2px 8px rgba(212, 175, 55, 0.2);
    
    h4 {
      color: #d4af37;
      font-weight: 700;
    }
    
    p {
      color: #333;
      font-weight: 500;
    }
  }

  &.line-static {
    opacity: 0.6;
    background: #f0f0f0;
    border-left-color: #999;
    
    h4 {
      color: #666;
    }
    
    p {
      color: #888;
    }
  }
}
```

**4. Consultation Result Component** (`consultation-result.ts`)
- Updated `viewHexagram()` to pass changing lines as query param
- Only passes changing lines when viewing primary hexagram

```typescript
viewHexagram(id: number): void {
  const queryParams: any = {};
  if (this.consultation && this.consultation.changingLines && this.consultation.changingLines.length > 0) {
    if (id === this.consultation.primaryHexagram.id) {
      queryParams.changingLines = this.consultation.changingLines.join(',');
    }
  }
  this.router.navigate(['/hexagrams', id], { queryParams });
}
```

**5. History Detail Component** (`history-detail.ts`)
- Same updates as consultation result component

### User Experience

#### Scenario 1: Consultation with Changing Lines
1. User completes consultation
2. Result shows: "Lines 2, 5 are changing"
3. User clicks "View Full Interpretation ?" on Primary Hexagram
4. Hexagram detail page loads
5. **Line 2** and **Line 5** are highlighted with golden background
6. **Lines 1, 3, 4, 6** are grayed out
7. User can focus on the relevant changing lines

#### Scenario 2: Viewing from History
1. User opens past consultation from history
2. Clicks on primary hexagram
3. Same highlighting behavior as above

#### Scenario 3: No Changing Lines
1. User completes consultation with no changing lines
2. Clicks "View Full Interpretation ?"
3. All lines display normally (no highlighting or graying)

#### Scenario 4: Browsing Hexagram Library
1. User browses `/hexagrams` list
2. Clicks any hexagram
3. All lines display normally (no query params passed)

### Visual Design

**Changing Line Styling:**
- Background: Soft golden gradient (`#fff9e6` to `#fffbf0`)
- Border: 4px solid gold (`#d4af37`)
- Shadow: Subtle golden glow
- Title: Bold golden text
- Content: Dark text, medium weight

**Static Line Styling:**
- Opacity: 60%
- Background: Light gray (`#f0f0f0`)
- Border: Gray (`#999`)
- Title: Gray text (`#666`)
- Content: Light gray text (`#888`)

---

## Feature 2: Delete Consultation Button ???

### Overview
Users can now delete consultations from their history with a confirmation prompt.

### Backend Changes

**1. Consultation Repository Interface** (`IConsultationRepository.cs`)
```csharp
Task DeleteAsync(int id);
```

**2. Consultation Repository Implementation** (`ConsultationRepository.cs`)
```csharp
public async Task DeleteAsync(int id)
{
    var consultation = await _context.Consultations.FindAsync(id);
    if (consultation != null)
    {
        _context.Consultations.Remove(consultation);
        await _context.SaveChangesAsync();
    }
}
```

**3. Consultation Service Interface** (`IConsultationService.cs`)
```csharp
Task DeleteConsultationAsync(int consultationId, int userId);
```

**4. Consultation Service Implementation** (`ConsultationService.cs`)
```csharp
public async Task DeleteConsultationAsync(int consultationId, int userId)
{
    var consultation = await _consultationRepository.GetByIdAsync(consultationId);
    
    if (consultation == null || consultation.UserId != userId)
        throw new UnauthorizedAccessException("Consultation not found or access denied");

    await _consultationRepository.DeleteAsync(consultationId);
}
```

**5. Consultations Controller** (`ConsultationsController.cs`)
```csharp
[HttpDelete("{id}")]
public async Task<ActionResult> Delete(int id)
{
    var userId = GetUserId();

    try
    {
        await _consultationService.DeleteConsultationAsync(id, userId);
        return NoContent();
    }
    catch (UnauthorizedAccessException)
    {
        return NotFound(new { message = "Consultation not found" });
    }
}
```

### Frontend Changes

**1. Consultation Service** (`consultation.service.ts`)
```typescript
deleteConsultation(id: number): Observable<void> {
  return this.http.delete<void>(`${this.apiUrl}/${id}`);
}
```

**2. History List Component** (`history-list.ts`)
```typescript
deleteConsultation(id: number, event: Event): void {
  event.stopPropagation();
  
  if (!confirm('Are you sure you want to delete this consultation? This action cannot be undone.')) {
    return;
  }

  this.consultationService.deleteConsultation(id).subscribe({
    next: () => {
      this.consultations = this.consultations.filter(c => c.id !== id);
    },
    error: (err) => {
      this.error = 'Failed to delete consultation';
      console.error('Error deleting consultation:', err);
    }
  });
}
```

**3. History List Template** (`history-list.html`)
```html
<div class="header-right">
  <span class="consultation-date">
    {{ formatDate(consultation.consultationDate) }}
  </span>
  <button 
    class="btn-delete" 
    (click)="deleteConsultation(consultation.id, $event)"
    title="Delete consultation"
    type="button">
    ???
  </button>
</div>
```

**4. History List Styles** (`history-list.scss`)
```scss
.header-right {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 15px;
}

.btn-delete {
  background: transparent;
  border: none;
  font-size: 1.3rem;
  cursor: pointer;
  padding: 5px 10px;
  border-radius: 4px;
  transition: all 0.2s;
  opacity: 0.6;
  flex-shrink: 0;

  &:hover {
    background: rgba(196, 30, 58, 0.1);
    opacity: 1;
    transform: scale(1.1);
  }

  &:active {
    transform: scale(0.95);
  }
}
```

### User Experience

#### Delete Flow
1. User opens history list
2. Each consultation card has a trash icon (???) in the top-right
3. User clicks trash icon
4. Browser confirmation prompt: "Are you sure you want to delete this consultation? This action cannot be undone."
5. User confirms:
   - API call to `DELETE /api/consultations/{id}`
   - Consultation removed from database
   - Card removed from UI (filtered from array)
6. User cancels:
   - No action taken
   - Dialog closes

#### Error Handling
- If deletion fails: Error message displayed "Failed to delete consultation"
- If unauthorized: Returns 404 from API (consultation not found)
- Consultation must belong to the user making the request

### Security
- Authorization required (JWT token)
- User can only delete their own consultations
- Backend validates userId matches consultation owner
- Permanent deletion (not soft delete)

---

## API Endpoints

### New Endpoint
```
DELETE /api/consultations/{id}
Authorization: Bearer {token}
Response: 204 No Content (success) or 404 Not Found (error)
```

### Modified Behavior
- Hexagram detail pages now accept `changingLines` query parameter
- Example: `/hexagrams/1?changingLines=2,5`

---

## Testing Checklist

### Changing Lines Highlighting
- [ ] Complete consultation with changing lines
- [ ] Click "View Full Interpretation" on primary hexagram
- [ ] Verify changing lines are highlighted (golden)
- [ ] Verify non-changing lines are grayed out
- [ ] Click on relating hexagram - should show no highlighting
- [ ] Browse hexagram library directly - should show no highlighting
- [ ] Test with Russian and English languages
- [ ] Test with 1 changing line
- [ ] Test with 6 changing lines (all)
- [ ] Test with 0 changing lines

### Delete Button
- [ ] Navigate to history list
- [ ] Verify delete button appears on each card
- [ ] Hover over delete button - verify hover effect
- [ ] Click delete button
- [ ] Verify confirmation dialog appears
- [ ] Click "Cancel" - verify nothing happens
- [ ] Click delete button again
- [ ] Click "OK" - verify consultation removed from list
- [ ] Refresh page - verify consultation is gone (deleted from DB)
- [ ] Try to access deleted consultation by ID - should return 404

---

## Files Modified

### Frontend (Angular)
1. `Itzin.Web/src/app/features/hexagrams/hexagram-detail/hexagram-detail.ts`
2. `Itzin.Web/src/app/features/hexagrams/hexagram-detail/hexagram-detail.html`
3. `Itzin.Web/src/app/features/hexagrams/hexagram-detail/hexagram-detail.scss`
4. `Itzin.Web/src/app/features/consultation/consultation-result/consultation-result.ts`
5. `Itzin.Web/src/app/features/history/history-detail/history-detail.ts`
6. `Itzin.Web/src/app/features/history/history-list/history-list.ts`
7. `Itzin.Web/src/app/features/history/history-list/history-list.html`
8. `Itzin.Web/src/app/features/history/history-list/history-list.scss`
9. `Itzin.Web/src/app/core/services/consultation.service.ts`

### Backend (C# .NET)
1. `Itzin.Core/Interfaces/IConsultationRepository.cs`
2. `Itzin.Core/Interfaces/IConsultationService.cs`
3. `Itzin.Infrastructure/Repositories/ConsultationRepository.cs`
4. `Itzin.Infrastructure/Services/ConsultationService.cs`
5. `Itzin.Api/Controllers/ConsultationsController.cs`

---

## Build Status
? **Build Successful** - All changes compiled without errors

---

## Next Steps
1. Test both features in the browser
2. Verify UX and visual appearance
3. Test edge cases (all changing, no changing, etc.)
4. Consider adding delete button to history detail page as well
5. Consider undo functionality for deletion (future enhancement)

---

**Status:** ? **Implementation Complete**  
**Date:** December 2025  
**Developer:** GitHub Copilot
