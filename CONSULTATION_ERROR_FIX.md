# Fix for "Failed to create consultation" Error

## 🔍 Root Causes Identified and Fixed

### Issue 1: Missing GetHexagramByIdAsync Method ✅ FIXED
**Problem**: The `HexagramsController.GetById(int id)` method was calling `GetHexagramByNumberAsync(id)` which doesn't exist in the service interface.

**Solution Applied**:
1. Added `GetHexagramByIdAsync(int id)` to `IHexagramService` interface
2. Implemented the method in `HexagramService` class
3. Updated `HexagramsController.GetById` to use the correct method

**Files Changed**:
- `Itzin.Core/Interfaces/IHexagramService.cs`
- `Itzin.Infrastructure/Services/HexagramService.cs`
- `Itzin.Api/Controllers/HexagramsController.cs`

### Issue 2: Missing Language Property in Frontend Request ✅ FIXED
**Problem**: The frontend's `CreateConsultationRequest` interface was missing the `language` property that the backend API expects.

**Solution Applied**:
1. Added `language?: string;` to `CreateConsultationRequest` interface
2. Updated `consultation-flow.ts` to send `language: 'en'` in the request

**Files Changed**:
- `Itzin.Web/src/app/core/models/consultation.model.ts`
- `Itzin.Web/src/app/features/consultation/consultation-flow/consultation-flow.ts`

### Issue 3: Backend Not Running ⚠️ NEEDS ACTION
**Problem**: The .NET backend API is not running on port 5000.

**Solution**: You need to start the backend server manually.

## 🚀 How to Test the Fix

### Step 1: Start the Backend

Open a terminal and run:

```bash
cd /Users/viktorshershnov/AI/Projects/ItzinCopilot/Itzin.Api
dotnet run
```

**Wait for this message**:
```
Now listening on: http://localhost:5000
Application started.
```

### Step 2: Verify Backend is Running

In another terminal:
```bash
# Test hexagram endpoint
curl http://localhost:5000/api/hexagrams/1

# Should return JSON with hexagram data
```

Or open in browser: http://localhost:5000/swagger

### Step 3: Test Frontend

1. Open browser: http://localhost:4200
2. Login/Register
3. Start a new consultation
4. Complete 6 coin tosses
5. The consultation should now be created successfully!

## 🐛 Debugging the Error

If you still see "Failed to create consultation", check the following:

### 1. Check Browser Console (F12)
Look for the actual error message:
```javascript
console.error('Error creating consultation:', error);
```

Common errors:
- **401 Unauthorized**: Token expired, need to login again
- **400 Bad Request**: Invalid data sent to API
- **500 Internal Server Error**: Backend bug
- **Network Error**: Backend not running

### 2. Check Backend Logs

```bash
# View latest logs
tail -f /Users/viktorshershnov/AI/Projects/ItzinCopilot/Itzin.Api/logs/itzin-*.log
```

Look for:
- Exception stack traces
- Validation errors
- Database errors

### 3. Check Network Tab in DevTools

1. Open DevTools (F12) → Network tab
2. Complete a consultation
3. Look for POST request to `/api/consultations`
4. Check:
   - Request payload (should have tossResults, question, language)
   - Response status code
   - Response body

### 4. Test the Backend Endpoint Directly

```bash
# Get auth token first (login via UI and copy from localStorage)
TOKEN="your_jwt_token_here"

# Test consultation creation
curl -X POST http://localhost:5000/api/consultations \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d '{
    "question": "Test question",
    "tossResults": [7, 8, 9, 6, 7, 8],
    "language": "en"
  }'
```

## 📊 Request/Response Structure

### Frontend Request
```typescript
{
  question?: string;        // Optional
  tossResults: number[];    // Required: array of 6 numbers (6-9)
  language?: string;        // Optional: "en" or "ru"
}
```

### Backend Response
```json
{
  "id": 1,
  "question": "Test question",
  "consultationDate": "2025-12-06T20:00:00Z",
  "primaryHexagram": {
    "id": 1,
    "number": 1,
    "chineseName": "乾",
    "englishName": "The Creative",
    "judgment": "...",
    "image": "...",
    "lines": ["...", "...", "...", "...", "...", "..."]
  },
  "relatingHexagram": null,
  "changingLines": [],
  "tossValues": [7, 8, 9, 6, 7, 8],
  "notes": null
}
```

## ✅ Validation Requirements

The backend validates:
- **tossResults**: Must be exactly 6 numbers
- **Each toss value**: Must be 6, 7, 8, or 9
  - 6 = Old Yin (changing)
  - 7 = Young Yang (static)
  - 8 = Young Yin (static)
  - 9 = Old Yang (changing)
- **question**: Optional, max 500 characters
- **language**: Optional, max 10 characters

## 🔧 Common Issues and Solutions

### Issue: "Hexagram not found for binary"
**Cause**: The binary calculation is producing an invalid result.
**Solution**: Check that all 64 hexagrams are seeded in the database.

```bash
# Check hexagram count
sqlite3 /Users/viktorshershnov/AI/Projects/ItzinCopilot/Itzin.Api/itzin.db "SELECT COUNT(*) FROM Hexagrams;"
# Should return: 64
```

### Issue: "Must have exactly 6 toss values"
**Cause**: Frontend is sending wrong number of tosses.
**Solution**: Verify the coin toss component completed all 6 tosses.

### Issue: "Invalid toss value"
**Cause**: Frontend is sending values outside 6-9 range.
**Solution**: Check the coin toss calculation logic in `CoinToss` component.

### Issue: Network Error / Backend not responding
**Cause**: Backend is not running or using wrong port.
**Solution**: 
```bash
# Check if backend is running
lsof -i :5000

# If not running, start it
cd /Users/viktorshershnov/AI/Projects/ItzinCopilot/Itzin.Api
dotnet run
```

## 📝 Summary of Changes

### Backend Changes (C#)
1. ✅ Added `GetHexagramByIdAsync` method to service
2. ✅ Fixed controller to use correct method

### Frontend Changes (TypeScript)
1. ✅ Added `language` property to request model
2. ✅ Updated consultation flow to send language
3. ✅ Fixed data model to match API response (from previous fix)

### Configuration
- ✅ Backend compiles without errors
- ✅ Frontend compiles without errors
- ⚠️ Backend needs to be started manually

## 🎯 Next Steps

1. **Start the backend** using `dotnet run` in Itzin.Api directory
2. **Test a consultation** end-to-end
3. **Check browser console** for any remaining errors
4. **Verify the "View Full Interpretation" button** works (from previous fix)

---

**Status**: All code fixes are complete. Backend needs to be started to test.

