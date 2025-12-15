# Advanced Consultation Default and Display Fix

## Summary
Fixed three main issues related to Advanced Consultation feature:
1. Set Advanced consultation flag to `true` by default
2. Preserve Advanced consultation flag when user clicks "Skip Question"
3. Display Chinese symbols and Russian descriptions for additional changing hexagrams

## Changes Made

### 1. Frontend - Question Input Component
**File**: `Itzin.Web/src/app/features/consultation/question-input/question-input.ts`

- Changed default value for `isAdvanced` from `false` to `true` in form initialization
- Updated `skip()` method to preserve the `isAdvanced` flag value when skipping question

```typescript
constructor() {
  this.questionForm = this.fb.group({
    question: [''],
    isAdvanced: [true]  // Changed from [false]
  });
}

skip(): void {
  const isAdvanced = this.questionForm.value.isAdvanced || false;
  
  this.consultationService.setQuestion('');
  this.consultationService.setIsAdvanced(isAdvanced);  // Preserve flag
  this.questionSubmitted.emit('');
}
```

### 2. Frontend - Consultation Service
**File**: `Itzin.Web/src/app/core/services/consultation.service.ts`

- Changed default value of `isAdvancedSubject` from `false` to `true`
- Updated `clearTossResults()` to reset `isAdvanced` to `true` instead of `false`

```typescript
private isAdvancedSubject = new BehaviorSubject<boolean>(true);

clearTossResults(): void {
  this.tossResultsSubject.next([]);
  this.currentQuestionSubject.next('');
  this.isAdvancedSubject.next(true);  // Changed from false
  this.currentConsultationSubject.next(null);
}
```

### 3. Frontend - Consultation Model
**File**: `Itzin.Web/src/app/core/models/consultation.model.ts`

- Added `additionalChangingHexagramsInfo?: HexagramInfo[]` property to store full hexagram details with Chinese symbols and Russian descriptions

### 4. Backend - DTO
**File**: `Itzin.Api/DTOs/ConsultationDto.cs`

- Added `AdditionalChangingHexagramsInfo` property to `ConsultationResponseDto`:

```csharp
public List<HexagramDto>? AdditionalChangingHexagramsInfo { get; set; }
```

### 5. Backend - Controller
**File**: `Itzin.Api/Controllers/ConsultationsController.cs`

- Injected `IHexagramService` to fetch full hexagram details
- Updated `MapToResponseDto()` method to fetch and populate full hexagram information for additional changing hexagrams:

```csharp
// Fetch full hexagram details for additional changing hexagrams
if (additionalChangingHexagrams != null && additionalChangingHexagrams.Count > 0)
{
    var additionalHexagramsInfo = new List<HexagramDto>();
    foreach (var hexagramId in additionalChangingHexagrams)
    {
        var hexagram = _hexagramService.GetHexagramByIdAsync(hexagramId).Result;
        if (hexagram != null)
        {
            additionalHexagramsInfo.Add(MapHexagramToDto(hexagram, isRussian));
        }
    }
    dto.AdditionalChangingHexagramsInfo = additionalHexagramsInfo;
}
```

### 6. Frontend - Consultation Result Component
**File**: `Itzin.Web/src/app/features/consultation/consultation-result/consultation-result.html`

- Added complete Advanced Consultation section that displays:
  - **Anti-Hexagram** (Inverse) with Chinese symbol, Pinyin, English name, Russian name, and judgment
  - **Changing Hexagram Pattern** with full details
  - **Progressive Transformations** section showing all additional changing hexagrams with:
    - Transformation label (Transformation 1, 2, 3...)
    - Chinese symbol (Unicode)
    - Chinese name
    - Pinyin
    - English name
    - Russian name
    - Judgment description

### 7. Frontend - Styles
**File**: `Itzin.Web/src/app/features/consultation/consultation-result/consultation-result.scss`

- Added styles for `.advanced-section` with gold border and gradient background
- Added styles for `.additional-hexagram` cards with gold accents
- Added `.additional-label` styling for transformation numbers
- Added `.hexagram-explanation` for explanatory text
- Responsive design for mobile devices

## Features

### Default Advanced Consultation
- Advanced consultation checkbox is now **checked by default** when opening the "Ask Your Question" form
- Users can still uncheck it if they want a basic consultation

### Preserved Flag on Skip
- When user clicks "Skip Question", the Advanced consultation flag is now preserved
- If user has Advanced checked and skips, they will get an advanced consultation
- If user has unchecked Advanced and skips, they will get a basic consultation

### Complete Hexagram Display
Each additional changing hexagram now displays:
- **Transformation number** (e.g., "Transformation 1")
- **Chinese symbol** (Unicode hexagram character)
- **Chinese name** (e.g., ?)
- **Pinyin** (e.g., Qi?n)
- **English name** (e.g., The Creative)
- **Russian name** (e.g., Òגמנקוסעגמ)
- **Judgment** (full interpretation text in selected language)
- **Clickable** to view full hexagram details

### Visual Hierarchy
- Advanced section has distinctive gold border and subtle gradient
- Additional changing hexagrams have gold accents to differentiate them
- Clear explanatory text for each hexagram type
- Consistent styling with the rest of the consultation result

## Testing Checklist

1. ? Open "Ask Your Question" form - Advanced consultation should be checked by default
2. ? With Advanced checked, enter a question and complete consultation - should see advanced hexagrams
3. ? With Advanced checked, click "Skip Question" - should still get advanced consultation
4. ? With Advanced unchecked, click "Skip Question" - should get basic consultation
5. ? View consultation result with changing lines - should see all additional hexagrams with full details
6. ? Each additional hexagram should display Chinese symbol and Russian name
7. ? Click on additional hexagrams - should navigate to hexagram detail page

## Technical Notes

- Used synchronous `.Result` in controller for fetching hexagrams (acceptable in this context as it's within async method)
- Maintained backward compatibility - basic consultations still work without advanced data
- All hexagram details are fetched from database using existing repository methods
- Language selection (Russian/English) is properly handled for all hexagram fields
