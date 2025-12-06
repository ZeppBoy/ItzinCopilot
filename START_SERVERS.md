# 🚀 How to Start Both Servers

## Quick Start (Recommended)

### Option 1: Use the Start Script

Open a terminal and run:
```bash
cd /Users/viktorshershnov/AI/Projects/ItzinCopilot
./start-backend.sh
```

This script will:
- Check if port 5000 is in use
- Kill any process using that port
- Start the backend API
- Display the Swagger URL

### Option 2: Manual Start

**Terminal 1 - Backend API:**
```bash
cd /Users/viktorshershnov/AI/Projects/ItzinCopilot/Itzin.Api

# Kill any process on port 5000 first
lsof -ti:5000 | xargs kill -9 2>/dev/null

# Start the backend
dotnet run
```

Wait for this message:
```
Now listening on: http://localhost:5000
```

**Terminal 2 - Frontend (Already Running):**
The Angular dev server should already be running on port 4200.
If not, run:
```bash
cd /Users/viktorshershnov/AI/Projects/ItzinCopilot/Itzin.Web
ng serve
```

## 🔧 Troubleshooting Port 5000 Error

If you get "address already in use" error:

### Find what's using port 5000:
```bash
lsof -i:5000
```

### Kill the process:
```bash
# Method 1: Kill by port
lsof -ti:5000 | xargs kill -9

# Method 2: Kill all Itzin.Api processes
pkill -f Itzin.Api

# Method 3: Kill all dotnet processes (nuclear option)
pkill -9 dotnet
```

Then wait 2-3 seconds and try starting again.

## ✅ Verify Servers are Running

### Check Backend:
```bash
curl http://localhost:5000/swagger/index.html
```
Should return HTML content (200 OK)

Or open in browser: http://localhost:5000/swagger

### Check Frontend:
Open browser: http://localhost:4200

## 🧪 Test the Button Fix

1. Open http://localhost:4200
2. Login/Register
3. Go to "New Consultation"
4. Complete 6 coin tosses
5. On result page, open DevTools (F12)
6. Click "View Full Interpretation →" button
7. Should see in console:
   ```
   Button clicked! Event: ... ID: 1
   viewHexagram called with id: 1
   ```
8. Should navigate to `/hexagrams/{id}` page

## 📊 Server URLs

- **Frontend**: http://localhost:4200
- **Backend API**: http://localhost:5000
- **Swagger Docs**: http://localhost:5000/swagger
- **API Health**: http://localhost:5000/api/hexagrams/1

## 🛑 Stopping Servers

### Stop Backend:
- If running in terminal: Press `Ctrl+C`
- Or kill process: `lsof -ti:5000 | xargs kill`

### Stop Frontend:
- If running in terminal: Press `Ctrl+C`
- Or kill process: `lsof -ti:4200 | xargs kill`

## 💡 Alternative Port Configuration

If port 5000 is persistently blocked, you can change it:

Edit `Itzin.Api/Properties/launchSettings.json`:
```json
"applicationUrl": "http://localhost:5001"
```

Then update `Itzin.Web/src/environments/environment.ts`:
```typescript
apiUrl: 'http://localhost:5001/api'
```

---

**Status**: Frontend is already running. You just need to start the backend!

