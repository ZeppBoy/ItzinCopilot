# ?? Advanced Consultation - Quick Test Guide

## ? 5-Minute Test

### 1. Start Applications (2 terminals)

**Terminal 1 - Backend:**
```bash
cd C:\Users\mrrov\source\repos\ItzinCopilot\Itzin.Api
dotnet run
```
Wait for: `Now listening on: http://localhost:5095`

**Terminal 2 - Frontend:**
```bash
cd C:\Users\mrrov\source\repos\ItzinCopilot\Itzin.Web
npm start
```
Wait for: `Compiled successfully!`

### 2. Test Advanced Consultation (3 minutes)

1. **Navigate** to `http://localhost:4200` (or whichever port Angular is running on)
2. **Login** with your test account
3. **Click** "New Consultation" or navigate to consultation page
4. **Verify** checkbox "Advanced Consultation" is CHECKED by default ?
5. **Enter** a test question (e.g., "What should I focus on today?")
6. **Complete** 6 coin tosses
7. **View** results

### 3. What to Look For ?

#### Primary Section (Always Shown)
- ? Primary Hexagram card with:
  - Chinese symbol
  - Chinese name, Pinyin, English/Russian names
  - Judgment text
  - Hexagram lines visualization

- ? Relating Hexagram (if changing lines exist)

#### Advanced Section (Should Appear)
- ? Section header: "Advanced Consultation" with gold border
- ? **Anti-Hexagram (Inverse)**
  - Explanation text
  - Full hexagram details
  - Clickable card

- ? **Changing Hexagram Pattern** (if changing lines exist)
  - Explanation text
  - Full hexagram details
  - Clickable card

- ? **Progressive Transformations** (if changing lines exist)
  - Multiple cards with "Transformation 1, 2, 3..." labels
  - Gold accent styling
  - Full hexagram details for each
  - All cards clickable

### 4. Quick Database Check

```bash
cd C:\Users\mrrov\source\repos\ItzinCopilot\Itzin.Api
sqlite3 itzin.db "SELECT Id, IsAdvanced, AntiHexagramId, ChangingHexagramId FROM Consultations ORDER BY Id DESC LIMIT 1;"
```

**Expected:** `123|1|12|45` (or similar - numbers will vary)

---

## ?? Additional Quick Tests

### Test 1: Basic Consultation (Not Advanced)
1. Start new consultation
2. **UNCHECK** "Advanced Consultation"
3. Complete consultation
4. **Expected:** No "Advanced Consultation" section should appear

### Test 2: Skip Question
1. Start new consultation
2. Keep "Advanced Consultation" checked
3. Click "Skip Question"
4. Complete tosses
5. **Expected:** Advanced results should still appear

### Test 3: Navigation
1. Complete advanced consultation
2. Click on any hexagram card (Primary, Relating, Anti, Changing, or Additional)
3. **Expected:** Should navigate to hexagram detail page
4. Use browser back button
5. **Expected:** Should return to consultation results

---

## ?? Success Indicators

? **Feature Working If:**
- Checkbox is checked by default
- Advanced section appears with gold border
- Anti-Hexagram is always shown (when advanced is enabled)
- Changing hexagrams appear when changing lines exist
- All hexagram cards show Chinese symbols, names, and details
- Clicking hexagram cards navigates to detail pages
- Database records show `IsAdvanced = 1` and populated hexagram IDs

? **Problem If:**
- Checkbox not checked by default
- No advanced section appears
- Missing hexagram details (empty fields)
- Cards not clickable
- Navigation broken
- Database shows NULL values for hexagram IDs

---

## ?? Troubleshooting

### Frontend not showing advanced section
1. Open browser dev tools (F12)
2. Check console for errors
3. Check Network tab - verify API response includes `isAdvanced: true`
4. Verify `consultation.isAdvanced` is true in component

### Missing hexagram details
1. Check API response - should include full hexagram objects
2. Verify database hexagram IDs are valid
3. Check controller mapping - should fetch full hexagram data

### Build errors
```bash
# Backend
cd C:\Users\mrrov\source\repos\ItzinCopilot\Itzin.Api
dotnet build

# Frontend
cd C:\Users\mrrov\source\repos\ItzinCopilot\Itzin.Web
npm run build
```

### Database issues
```bash
cd C:\Users\mrrov\source\repos\ItzinCopilot\Itzin.Api
sqlite3 itzin.db "PRAGMA table_info(Consultations);"
```
Verify columns 8-11 exist: `IsAdvanced`, `AntiHexagramId`, `ChangingHexagramId`, `AdditionalChangingHexagrams`

---

## ?? Test Data Examples

### Example Toss Results with Changing Lines
```
[9, 6, 7, 8, 9, 7]
```
- Line 1: 9 (old yang - changing)
- Line 2: 6 (old yin - changing)  
- Line 3: 7 (young yang - stable)
- Line 4: 8 (young yin - stable)
- Line 5: 9 (old yang - changing)
- Line 6: 7 (young yang - stable)

**Result:** 3 changing lines ? Should see Changing Hexagram and 3 Additional Changing Hexagrams

### Example Toss Results without Changing Lines
```
[7, 8, 7, 8, 7, 8]
```
All stable lines ? Only Primary hexagram, no Relating, but Anti-Hexagram should still appear

---

## ?? Pro Tips

1. **Use Browser DevTools:** 
   - Network tab to inspect API responses
   - Console to check for JavaScript errors
   - Elements tab to verify DOM structure

2. **Test Multiple Scenarios:**
   - Consultation with 0 changing lines
   - Consultation with 1 changing line
   - Consultation with 6 changing lines (all changing)

3. **Check Responsive Design:**
   - Test on mobile viewport (F12 ? Toggle device toolbar)
   - Verify cards stack properly
   - Check that all text is readable

4. **Verify Calculations:**
   - Note the primary hexagram binary
   - Calculate expected anti-hexagram (flip all bits)
   - Verify displayed anti-hexagram matches

---

## ? Test Complete Checklist

- [ ] Backend starts without errors
- [ ] Frontend builds and runs without errors
- [ ] Checkbox is checked by default
- [ ] Advanced section appears with gold border
- [ ] Anti-Hexagram displays correctly
- [ ] Changing Hexagram appears (when applicable)
- [ ] Additional hexagrams appear (when applicable)
- [ ] All Chinese symbols render correctly
- [ ] All hexagram details populate (names, judgment)
- [ ] Cards are clickable and navigate properly
- [ ] Database records show correct values
- [ ] Can disable advanced by unchecking
- [ ] Mobile view works correctly

---

## ?? Done!

If all checkboxes above are checked, the Advanced Consultation feature is working correctly!

**Time to completion:** ~5 minutes  
**Confidence level:** High if all tests pass  
**Next step:** Use the feature for real consultations!

---

**Document:** Quick Test Guide  
**Date:** December 15, 2025  
**Status:** Ready for Testing
