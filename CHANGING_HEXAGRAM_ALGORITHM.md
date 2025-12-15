# Changing Hexagram Algorithm - Implementation Details

## Overview
The `CalculateChangingHexagram` method creates a special hexagram that represents the pattern of change in the consultation, where:
- **Changing lines ? Yin (0)**
- **Non-changing lines ? Yang (1)**

## Algorithm Logic

### Purpose
This hexagram serves as a **change pattern indicator**, highlighting which lines are in transformation regardless of their original state.

### Implementation
```csharp
/// <summary>
/// Calculates the Changing Hexagram based on changing lines pattern
/// Changing lines become Yin (0), non-changing lines become Yang (1)
/// This creates a hexagram that highlights which lines are in flux
/// </summary>
private async Task<int?> CalculateChangingHexagram(string primaryBinary, List<int> changingLines)
{
    var charArray = new char[primaryBinary.Length];
    
    for (int i = 0; i < primaryBinary.Length; i++)
    {
        int linePosition = i + 1; // Lines are 1-indexed
        
        if (changingLines.Contains(linePosition))
        {
            charArray[i] = '0'; // Changing line ? Yin
        }
        else
        {
            charArray[i] = '1'; // Non-changing line ? Yang
        }
    }
    
    var changingHexagramBinary = new string(charArray);
    var changingHexagram = await _hexagramService.GetHexagramByBinaryAsync(changingHexagramBinary);
    return changingHexagram?.Id;
}
```

## Examples

### Example 1: Primary Hexagram with Lines 2 and 5 Changing

**Primary Hexagram:** `111000` (Heaven ?)
**Changing Lines:** [2, 5]

**Calculation:**
- Line 1: Non-changing ? `1` (Yang)
- Line 2: **Changing** ? `0` (Yin)
- Line 3: Non-changing ? `1` (Yang)
- Line 4: Non-changing ? `1` (Yang)
- Line 5: **Changing** ? `0` (Yin)
- Line 6: Non-changing ? `1` (Yang)

**Changing Hexagram Binary:** `101101`
**Result:** Hexagram that shows the pattern of change

### Example 2: Primary Hexagram with Line 3 Changing

**Primary Hexagram:** `101010` 
**Changing Lines:** [3]

**Calculation:**
- Line 1: Non-changing ? `1` (Yang)
- Line 2: Non-changing ? `1` (Yang)
- Line 3: **Changing** ? `0` (Yin)
- Line 4: Non-changing ? `1` (Yang)
- Line 5: Non-changing ? `1` (Yang)
- Line 6: Non-changing ? `1` (Yang)

**Changing Hexagram Binary:** `110111`
**Result:** Hexagram highlighting line 3 in transformation

### Example 3: No Changing Lines

**Primary Hexagram:** `111111`
**Changing Lines:** [] (empty)

**Result:** This method is **not called** when `changingLines.Count == 0`
```csharp
if (changingLines.Count > 0)
{
    consultation.ChangingHexagramId = await CalculateChangingHexagram(primaryBinary, changingLines);
}
```

## Interpretation Significance

### What the Changing Hexagram Represents

1. **Pattern of Transformation**
   - Visual representation of which lines are in flux
   - Independent of whether lines are originally Yin or Yang
   - Shows the **structure** of change, not the **direction**

2. **Divination Insight**
   - Reveals which aspects of the situation are unstable
   - Highlights areas requiring attention or adaptation
   - Can be consulted for guidance on managing transitions

3. **Complementary to Relating Hexagram**
   - **Relating Hexagram:** Shows the final state after transformation
   - **Changing Hexagram:** Shows the pattern/structure of the transformation itself
   - Together they provide complete picture: current ? pattern ? future

## I Ching Theory Perspective

### Traditional Context
In traditional I Ching readings:
- Changing lines indicate unstable situations
- These lines will transform into their opposites
- The pattern of change itself has meaning

### Modern Interpretation
The Changing Hexagram as implemented:
- Abstracts the change pattern from the original hexagram
- Creates a "meta-hexagram" representing transformation dynamics
- Can be interpreted through traditional hexagram meanings to understand the nature of change

## Comparison with Other Advanced Hexagrams

| Hexagram Type | Logic | Purpose |
|---------------|-------|---------|
| **Primary** | Original divination result | Current situation |
| **Relating** | Flip only changing lines | Future situation after transformation |
| **Anti-Hexagram** | Flip ALL lines | Complete opposite/inverse perspective |
| **Changing Hexagram** | Changing?Yin, Non-changing?Yang | Pattern/structure of transformation |
| **Additional** | Individual line changes | Progressive transformations |

## Technical Details

### Line Indexing
- **Binary string:** 0-indexed from left (0, 1, 2, 3, 4, 5)
- **Line positions:** 1-indexed from bottom (1, 2, 3, 4, 5, 6)
- **Conversion:** `linePosition = index + 1`

### Binary Representation
- `'1'` = Yang line (solid line: ??????)
- `'0'` = Yin line (broken line: ??  ??)

### Edge Cases
1. **No changing lines:** Method not called (checked before invocation)
2. **All lines changing:** Result is all Yin (`000000`)
3. **One line changing:** Result has single Yin among Yang lines

## Testing Examples

### Test Case 1: Single Changing Line
```csharp
Primary: "111111" (all Yang)
Changing Lines: [3]
Expected Result: "110111" (line 3 is Yin, rest Yang)
```

### Test Case 2: Multiple Changing Lines
```csharp
Primary: "000000" (all Yin)
Changing Lines: [1, 4, 6]
Expected Result: "011010" (lines 1, 4, 6 are Yin, rest Yang)
```

### Test Case 3: Mixed Primary with Changes
```csharp
Primary: "101010"
Changing Lines: [2, 4, 5]
Expected Result: "101001" (lines 2, 4, 5 are Yin, rest Yang)
```

## Implementation Status

? **Fully Implemented**
- Logic complete and tested
- Build successful
- Ready for use in Advanced Consultations

## Related Methods

1. **CalculateAntiHexagram:** Flips ALL lines
2. **CalculateAdditionalChangingHexagrams:** Progressive individual line changes
3. **CalculateRelatingHexagramBinary:** (in HexagramService) Flips only changing lines

---

**Date:** January 2025  
**Status:** ? Complete  
**Build:** ? Successful
