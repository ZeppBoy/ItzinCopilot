# Hidden Opportunity and Subsequence Properties - Frontend Implementation

## Summary
Successfully added `hiddenOpportunity` and `subsequence` properties to the frontend to match the backend API structure.

## Changes Made

### 1. TypeScript Model Update
**File**: `/Itzin.Web/src/app/core/models/hexagram.model.ts`

Added two new properties to the `HexagramRuDescription` interface:
- `hiddenOpportunity: string;` - Скрытая возможность (Hidden Opportunity)
- `subsequence: string;` - Последствия (Consequences/Subsequence)

These properties are positioned after `innerOuterWorlds` to match the backend DTO structure in `HexagramRuDescriptionDto`.

### 2. HTML Template Update
**File**: `/Itzin.Web/src/app/features/hexagrams/hexagram-detail/hexagram-detail.html`

Added two new display sections in the Russian description view:

```html
@if (hexagram.ruDescription.hiddenOpportunity) {
  <section class="interpretation-section">
    <h3>Скрытая возможность</h3>
    <div class="interpretation-text">
      <p>{{ hexagram.ruDescription.hiddenOpportunity }}</p>
    </div>
  </section>
}

@if (hexagram.ruDescription.subsequence) {
  <section class="interpretation-section">
    <h3>Последствия</h3>
    <div class="interpretation-text">
      <p>{{ hexagram.ruDescription.subsequence }}</p>
    </div>
  </section>
}
```

These sections are positioned after "Внутренний и внешний миры" (Inner and Outer Worlds) and before the "Черты" (Lines) section.

## Backend Alignment
The frontend now fully aligns with the backend structure:

**Backend DTO** (`Itzin.Api/DTOs/HexagramDto.cs`):
```csharp
public class HexagramRuDescriptionDto
{
    public string Short { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ImageRow { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string InnerOuterWorlds { get; set; } = string.Empty;
    public string HiddenOpportunity { get; set; } = string.Empty;  // ✓ Added
    public string Subsequence { get; set; } = string.Empty;        // ✓ Added
    public string Definition { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    // ... lines ...
}
```

**Frontend Model** (`hexagram.model.ts`):
```typescript
export interface HexagramRuDescription {
  short: string;
  name: string;
  imageRow: string;
  description: string;
  innerOuterWorlds: string;
  hiddenOpportunity: string;  // ✓ Added
  subsequence: string;        // ✓ Added
  definition: string;
  symbol: string;
  // ... lines ...
}
```

## Testing
- ✅ TypeScript compilation: No errors
- ✅ Model interface updated
- ✅ Display template updated
- ✅ Properties will display when data is available from the API

## Display Behavior
The new sections will automatically appear in the hexagram detail view when:
1. Viewing a hexagram in Russian (RU) mode
2. The hexagram has `ruDescription` data
3. The `hiddenOpportunity` or `subsequence` fields contain non-empty values

## Notes
- The properties use camelCase naming convention in TypeScript (matching Angular/TypeScript conventions)
- The backend uses PascalCase (matching C# conventions)
- Angular's HTTP client automatically converts between these naming conventions
- Russian translations used:
  - **HiddenOpportunity** → "Скрытая возможность"
  - **Subsequence** → "Последствия"

