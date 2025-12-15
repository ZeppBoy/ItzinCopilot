# Quick Testing Guide - Consultation Enhancements

## ?? Feature 1: Changing Lines Highlighting

### Test 1: Basic Highlighting
1. Login to the app
2. Start a new consultation
3. Complete 6 coin tosses (ensure at least 1-2 changing lines)
4. On result page, click **"View Full Interpretation ?"** on **Primary Hexagram**
5. **Expected Results:**
   - Changing lines have **golden background** (#fff9e6)
   - Changing lines have **bold golden titles** (#d4af37)
   - Non-changing lines are **grayed out** (60% opacity)
   - Non-changing lines have **gray text** (#666)

### Test 2: No Highlighting for Other Hexagrams
1. From same consultation result
2. Click **"View Full Interpretation ?"** on **Relating Hexagram**
3. **Expected:** All lines display normally (no highlighting)

### Test 3: Direct Hexagram Browse
1. Navigate to **Hexagrams** menu
2. Click any hexagram
3. **Expected:** All lines display normally (no query param)

### Test 4: Language Toggle
1. View hexagram with highlighted lines
2. Click language toggle (EN/RU button)
3. **Expected:** Highlighting persists after language switch

---

## ??? Feature 2: Delete Consultation

### Test 1: Delete from History List
1. Navigate to **History** menu
2. Find any consultation card
3. Locate **??? icon** in top-right corner
4. Hover over icon
5. **Expected:** Icon gets red background, scales up slightly
6. Click the icon
7. **Expected:** Browser confirmation dialog appears
   - Message: "Are you sure you want to delete this consultation? This action cannot be undone."

### Test 2: Cancel Deletion
1. Click delete icon
2. In confirmation dialog, click **"Cancel"**
3. **Expected:** 
   - Dialog closes
   - Consultation remains in list
   - No API call made

### Test 3: Confirm Deletion
1. Click delete icon
2. In confirmation dialog, click **"OK"**
3. **Expected:**
   - Consultation card disappears from list immediately
   - No error messages appear
4. Refresh the page
5. **Expected:** 
   - Consultation is still gone
   - Deleted from database

### Test 4: Multiple Deletions
1. Delete first consultation
2. Immediately delete second consultation
3. **Expected:** Both removed successfully, no errors

---

## ?? Visual Reference

### Changing Line (Highlighted)
```
???????????????????????????????????????????
? Черта 2 (golden #d4af37, bold)         ?
?                                         ?
? Description text (dark #333, medium)   ?
?                                         ?
? Golden background (#fff9e6 gradient)   ?
? Golden border left (4px solid #d4af37) ?
? Subtle shadow                           ?
???????????????????????????????????????????
```

### Non-Changing Line (Grayed Out)
```
???????????????????????????????????????????
? Черта 1 (gray #666)                    ?
?                                         ?
? Description text (light gray #888)     ?
?                                         ?
? Gray background (#f0f0f0)              ?
? Gray border left (4px solid #999)      ?
? 60% opacity                             ?
???????????????????????????????????????????
```

### Delete Button States
- **Default:** ??? (opacity: 0.6)
- **Hover:** ??? (opacity: 1, red background, scale: 1.1)
- **Active:** ??? (scale: 0.95)

---

## ?? Edge Cases to Test

### Changing Lines Feature
- [ ] Consultation with 0 changing lines
- [ ] Consultation with 1 changing line
- [ ] Consultation with 6 changing lines (all)
- [ ] Consultation with lines 1, 3, 5 changing
- [ ] Consultation with lines 2, 4, 6 changing
- [ ] View hexagram, then back, then forward (browser history)
- [ ] Direct URL access: `/hexagrams/1?changingLines=2,5`
- [ ] Invalid query param: `/hexagrams/1?changingLines=invalid`

### Delete Feature
- [ ] Delete first consultation in list
- [ ] Delete last consultation in list
- [ ] Delete with 1 consultation remaining
- [ ] Delete when no consultations left (should show empty state)
- [ ] Try to delete twice (click very fast)
- [ ] Network error during deletion
- [ ] Unauthorized deletion (different user's consultation)

---

## ?? Known Behaviors

### Changing Lines
- Only works when navigating from consultation/history pages
- Does not apply to relating/anti/changing hexagrams (by design)
- Query param format: `changingLines=1,2,3` (comma-separated)
- Line numbers are 1-indexed (1-6)

### Delete
- **Permanent deletion** - no undo functionality
- User confirmation required (browser native dialog)
- Only owner can delete their consultations
- Card removed immediately from UI (optimistic update)
- If API fails, error message shown but card already removed (refresh to restore)

---

## ?? Quick Start Commands

### Start Backend
```bash
cd Itzin.Api
dotnet run
```
**URL:** http://localhost:5095

### Start Frontend
```bash
cd Itzin.Web
npm start
```
**URL:** http://localhost:4200

---

## ? Success Criteria

**Changing Lines:**
- ? Changing lines clearly stand out with golden highlight
- ? Non-changing lines are visually de-emphasized
- ? Easy to focus on relevant interpretations
- ? Works in both Russian and English

**Delete Button:**
- ? Trash icon visible on each history card
- ? Hover effect provides visual feedback
- ? Confirmation prevents accidental deletion
- ? Immediate UI update after deletion
- ? Persistent deletion (survives page refresh)

---

## ?? Full Test Report Template

```
Date: ___________
Tester: ___________

CHANGING LINES HIGHLIGHTING
[ ] Basic highlighting (with changing lines)
[ ] No highlighting (relating hexagram)
[ ] No highlighting (direct browse)
[ ] Language toggle persists highlighting
[ ] Edge case: 0 changing lines
[ ] Edge case: All 6 changing lines

DELETE BUTTON
[ ] Button appears on all history cards
[ ] Hover effect works
[ ] Confirmation dialog shows
[ ] Cancel works (no deletion)
[ ] Confirm works (deletion successful)
[ ] Refresh confirms deletion persisted
[ ] Multiple deletions work

ISSUES FOUND:
_________________________________
_________________________________

OVERALL RESULT: [ ] PASS  [ ] FAIL

NOTES:
_________________________________
_________________________________
```

---

**Testing Time Estimate:** 15-20 minutes for full test suite
**Priority:** High - Both features affect core UX
