# Hexagram Line Design - Final Version

**Date:** December 3, 2025
**Status:** ✅ Implemented (All lines solid, equal length)

## Core Design Principle

**ALL LINES ARE SOLID AND HAVE EXACTLY THE SAME LENGTH AND WIDTH**

## Line Types and Visual Representation

### 1. Young Yang (Solid Line - Not Changing)
```
▬▬▬▬▬▬▬
```
- **Meaning:** Stable yang energy
- **Color:** Black (#1a1a1a)
- **Value:** 7 (from coin toss)
- **Description:** Solid line, no indicator

### 2. Old Yang (Changing Yang → Yin)
```
▬▬▬ × ▬▬▬
```
- **Meaning:** Yang changing to Yin
- **Color:** Red (#c41e3a) with glow effect
- **Value:** 9 (from coin toss)
- **Indicator:** × (multiplication sign) **in the center**
- **Animation:** Subtle pulse effect
- **Description:** Solid line with × in the middle

### 3. Young Yin (Solid Line - Not Changing)
```
▬▬▬▬▬▬▬
```
- **Meaning:** Stable yin energy
- **Color:** Black (#1a1a1a)
- **Value:** 8 (from coin toss)
- **Description:** Solid line, identical to Young Yang

### 4. Old Yin (Changing Yin → Yang)
```
▬▬▬ ○ ▬▬▬
```
- **Meaning:** Yin changing to Yang
- **Color:** Red (#c41e3a) with glow effect
- **Value:** 6 (from coin toss)
- **Indicator:** ○ (circle) **in the center**
- **Animation:** Subtle pulse effect
- **Description:** Solid line with ○ in the middle

### 5. Empty Slot (Pending Toss)
```
▬▬▬▬▬▬▬
```
- **Meaning:** Line not yet cast
- **Color:** Gray (#999)
- **Opacity:** 15%
- **Description:** Same solid line, faded

## Visual Layout in Hexagram Display

```
┌─────────────────────┐
│  Building Hexagram  │
│                     │
│   ▬▬▬▬▬▬▬          │ Line 6 (empty - faded)
│   ▬▬▬▬▬▬▬          │ Line 5 (empty - faded)
│   ▬▬▬ × ▬▬▬        │ Line 4 (Old Yang - RED)
│   ▬▬▬▬▬▬▬          │ Line 3 (Young Yin - BLACK)
│   ▬▬▬ ○ ▬▬▬        │ Line 2 (Old Yin - RED)
│   ▬▬▬▬▬▬▬          │ Line 1 (Young Yang - BLACK)
│                     │
└─────────────────────┘
```

## Key Features

### 1. Uniform Line Length
- **ALL lines:** 7 characters (▬▬▬▬▬▬▬)
- **ALL lines:** 220px width
- **ALL lines:** Same font size (2.2rem)
- **ALL lines:** Same font weight (900)
- **ALL lines:** Solid appearance

### 2. Centered Changing Indicators
- **×** for Old Yang (changing)
- **○** for Old Yin (changing)
- Indicators placed **in the center** of the line
- Pattern: `▬▬▬ × ▬▬▬` or `▬▬▬ ○ ▬▬▬`

### 3. Color Differentiation Only
- **Black lines:** Young Yang or Young Yin (stable)
- **Red lines:** Old Yang or Old Yin (changing)
- **Gray lines:** Not yet cast (empty)

### 4. No Visual Difference Between Yang and Yin
- Traditional I Ching used broken lines for Yin
- This design uses **solid lines for all**
- Distinction is made only by **color** and **changing indicators**
- Yang vs Yin is tracked in the data model, not visually

## Symbol Meanings

### × (Multiplication Sign)
- **Old Yang** (value 9)
- Yang energy transforming to Yin
- Active transformation
- Red color + pulse animation

### ○ (Circle)
- **Old Yin** (value 6)
- Yin energy transforming to Yang
- Receptive transformation
- Red color + pulse animation

## Technical Implementation

### Character Details
```typescript
// All lines use the same base:
const solidLine = '▬▬▬▬▬▬▬';

// Changing yang:
const oldYang = '▬▬▬ × ▬▬▬';

// Changing yin:
const oldYin = '▬▬▬ ○ ▬▬▬';

// Empty (same as solid, just faded):
const empty = '▬▬▬▬▬▬▬'; // with opacity: 0.15
```

### CSS Specifications
```scss
.line-visual {
  font-size: 2.2rem;        // 35.2px
  font-weight: 900;         // Extra bold
  font-family: 'Courier New', monospace;
  letter-spacing: 3px;      // Space between characters
  width: 220px;             // Fixed width
  color: #1a1a1a;           // Black for stable
  
  &.changing {
    color: #c41e3a;         // Red for changing
    text-shadow: 0 0 10px rgba(196, 30, 58, 0.3);
    animation: pulse 1.5s ease-in-out infinite;
  }
  
  &.empty {
    opacity: 0.15;          // Very faded
    color: #999;
  }
}
```

## Examples

### Example 1: All Stable Lines (No Transformation)
```
▬▬▬▬▬▬▬    Line 6 (Young Yang - BLACK)
▬▬▬▬▬▬▬    Line 5 (Young Yin - BLACK)
▬▬▬▬▬▬▬    Line 4 (Young Yang - BLACK)
▬▬▬▬▬▬▬    Line 3 (Young Yin - BLACK)
▬▬▬▬▬▬▬    Line 2 (Young Yang - BLACK)
▬▬▬▬▬▬▬    Line 1 (Young Yin - BLACK)
```
**Result:** All black, no changing indicators, no relating hexagram

### Example 2: Mixed with Changing Lines
```
▬▬▬▬▬▬▬        Line 6 (Young Yang - BLACK)
▬▬▬ × ▬▬▬      Line 5 (Old Yang - RED with ×)
▬▬▬▬▬▬▬        Line 4 (Young Yin - BLACK)
▬▬▬ ○ ▬▬▬      Line 3 (Old Yin - RED with ○)
▬▬▬▬▬▬▬        Line 2 (Young Yang - BLACK)
▬▬▬▬▬▬▬        Line 1 (Young Yin - BLACK)
```
**Result:** 2 red lines with indicators, relating hexagram will be generated

### Example 3: During Building (3 of 6 tosses)
```
▬▬▬▬▬▬▬        Line 6 (empty - GRAY faded)
▬▬▬▬▬▬▬        Line 5 (empty - GRAY faded)
▬▬▬▬▬▬▬        Line 4 (empty - GRAY faded)
▬▬▬ × ▬▬▬      Line 3 (Old Yang - RED with ×)
▬▬▬▬▬▬▬        Line 2 (Young Yin - BLACK)
▬▬▬▬▬▬▬        Line 1 (Young Yang - BLACK)
```
**Result:** 3 completed (solid opacity), 3 pending (faded opacity)

## Alignment & Consistency

### Perfect Alignment Achieved
✅ All lines exactly 7 characters: `▬▬▬▬▬▬▬`
✅ All lines 220px width
✅ All lines same font size (2.2rem)
✅ All lines same font weight (900)
✅ All lines same letter spacing (3px)
✅ Monospace font ensures perfect character alignment
✅ Centered indicators maintain balance

### Visual Consistency
- Empty lines: Same as filled, just faded (15% opacity)
- Changing indicators (× and ○) centered perfectly
- 3 characters on each side of indicator
- All lines stack perfectly in vertical alignment

## User Experience Benefits

1. **Visual Uniformity**
   - All lines look the same at base level
   - Professional, clean appearance
   - Easy to scan

2. **Clear Change Indicators**
   - Red color immediately signals transformation
   - × and ○ clearly mark the changing lines
   - Pulse animation draws attention

3. **Instant Recognition**
   - Black = Stable
   - Red with × = Yang transforming to Yin
   - Red with ○ = Yin transforming to Yang
   - Faded = Not yet determined

4. **Perfect Alignment**
   - All lines stack perfectly
   - No visual jitter or misalignment
   - Professional appearance

---

**Result:** All lines are solid, identical in length and width, with clear centered indicators for changing lines. Perfect visual consistency and alignment throughout the hexagram building process.
