# Hexagram Display - Visual Reference

## What You'll See

### Primary Hexagram Display

```
╔═══════════════════════════════════════════════════════════════╗
║  Primary Hexagram                                              ║
║                                                                ║
║  ┌──────────────────────────────────────────────────────────┐ ║
║  │                                                         42 │ ║
║  │    ䷩                                                      │ ║
║  │                                                            │ ║
║  │    ═══════════  (Line 6)                                  │ ║
║  │    ═══════════  (Line 5)                                  │ ║
║  │    ═══ • ═══    (Line 4 - changing)                       │ ║
║  │    ═══════════  (Line 3)                                  │ ║
║  │    ═══ ○ ═══    (Line 2 - changing)                       │ ║
║  │    ═══   ═══    (Line 1)                                  │ ║
║  │                                                            │ ║
║  │    Hexagram 42                                             │ ║
║  │    益                                                      │ ║
║  │    Yì                                                      │ ║
║  │    Increase                                                │ ║
║  │    Приумножение                                           │ ║
║  │                                                            │ ║
║  │    Judgment: Increase. It furthers one to undertake...    │ ║
║  │                                                            │ ║
║  │    [View Full Interpretation →]                           │ ║
║  └────────────────────────────────────────────────────────────┘ ║
╚═══════════════════════════════════════════════════════════════╝
```

### Layout Structure

**Desktop View (>640px):**
```
┌─────────────────────────────────────────────────────┐
│  ┌──────────┐  ┌─────────────────────────────────┐ │
│  │   [42]   │  │  Hexagram 42                    │ │
│  │    ䷩     │  │  益 (Chinese)                   │ │
│  │          │  │  Yì (Pinyin)                    │ │
│  │  ══════  │  │  Increase (English)             │ │
│  │  ══════  │  │  Приумножение (Russian)         │ │
│  │  ══ • ══ │  │                                  │ │
│  │  ══════  │  │  Judgment: It furthers one...   │ │
│  │  ══ ○ ══ │  │                                  │ │
│  │  ══  ══  │  └─────────────────────────────────┘ │
│  └──────────┘                                       │
│                                                     │
│  [     View Full Interpretation →     ]            │
└─────────────────────────────────────────────────────┘
```

**Mobile View (<640px):**
```
┌───────────────────────────┐
│      ┌──────────┐         │
│      │   [42]   │         │
│      │    ䷩     │         │
│      │          │         │
│      │  ══════  │         │
│      │  ══════  │         │
│      │  ══ • ══ │         │
│      │  ══════  │         │
│      │  ══ ○ ══ │         │
│      │  ══  ══  │         │
│      └──────────┘         │
│                           │
│   Hexagram 42             │
│   益                      │
│   Yì                      │
│   Increase                │
│   Приумножение            │
│                           │
│   Judgment: It furthers.. │
│                           │
│  [View Full →]            │
└───────────────────────────┘
```

## Complete Consultation Result Page

```
═══════════════════════════════════════════════════
              Your Hexagram
═══════════════════════════════════════════════════

┌─────────────────────────────────────────────────┐
│  Your Question                                   │
│                                                  │
│  "Should I accept the new job offer?"            │
└─────────────────────────────────────────────────┘

───────────────────────────────────────────────────
  Primary Hexagram
───────────────────────────────────────────────────

[HEXAGRAM VISUAL DISPLAY - See above]

───────────────────────────────────────────────────
  Changing Lines
───────────────────────────────────────────────────

Lines 2, 4 are changing

───────────────────────────────────────────────────
  Relating Hexagram
───────────────────────────────────────────────────

[TRANSFORMED HEXAGRAM VISUAL DISPLAY]
(Lines 2 and 4 are flipped, showing the future state)

───────────────────────────────────────────────────

Click on the hexagrams above to view their full
interpretations and understand the guidance they offer.

┌─────────────────┐  ┌──────────────────┐
│ New Consultation │  │ Back to Dashboard │
└─────────────────┘  └──────────────────┘
```

## Line Types Visual Reference

### Yang Lines (Solid)
```
Young Yang (7):  ═══════════
Old Yang (9):    ═══ ● ═══  (changing - has marker)
```

### Yin Lines (Broken)
```
Young Yin (8):   ═══   ═══
Old Yin (6):     ═══ ○ ═══  (changing - has marker)
```

## Color Scheme

- **Primary Red:** #c41e3a (used for Chinese names, titles, borders on hover)
- **Dark Text:** #333 (English names, main text)
- **Gray Text:** #666 (descriptions, pinyin)
- **Light Gray:** #999 (labels, secondary text)
- **Background:** White with subtle gradients
- **Borders:** #e0e0e0 (light gray)

## Interactive Elements

### Hover States
- **Hexagram Card:** Border changes to red, slight lift effect
- **View Button:** Background darkens (#a01829)

### Click Behavior
- **Card Click:** Navigate to hexagram detail page
- **Button Click:** Navigate to hexagram detail page (event propagation stopped)

## Information Hierarchy

**Size Order (largest to smallest):**
1. Chinese Name - 2.5rem
2. English Name - 1.5rem
3. Russian Name - 1.2rem
4. Pinyin - 1.1rem
5. Description - 0.95rem
6. Labels - 0.9rem

## Spacing & Layout

- Card padding: 30px
- Gap between visual and info: 30px (20px on mobile)
- Line gap: 8px
- Section margins: 15-30px

## Accessibility Features

- Alt text on line images
- Semantic HTML structure
- High contrast text colors
- Large touch targets (buttons)
- Clear visual hierarchy

## Example Complete Display

For a consultation that generates Hexagram 1 (The Creative) with lines 3 and 5 changing, transforming into Hexagram 14 (Possession in Great Measure):

**Primary Hexagram (1):**
- Number: 1
- Symbol: ䷀
- Lines: All yang, with lines 3 and 5 marked as changing
- Chinese: 乾
- Pinyin: Qián
- English: The Creative
- Russian: Творчество
- Judgment: The Creative works sublime success...

**Relating Hexagram (14):**
- Number: 14
- Symbol: ䷍
- Lines: Lines 3 and 5 transformed to yin, others remain yang
- Chinese: 大有
- Pinyin: Dà Yǒu
- English: Possession in Great Measure
- Russian: Обладание великим
- Judgment: Possession in Great Measure. Supreme success.

---

**This is the complete visual and functional specification of the implemented hexagram display feature.**

