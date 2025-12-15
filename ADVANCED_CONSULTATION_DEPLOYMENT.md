# ?? Advanced Consultation Feature - Deployment Checklist

## ? Pre-Deployment Verification

### Build Status
- [x] Backend builds successfully: `dotnet build` ?
- [x] Frontend compiles successfully: `npm run build` ?
- [x] No TypeScript errors ?
- [x] No C# compiler warnings ?

### Code Quality
- [x] All files properly formatted ?
- [x] No console.log statements in production code ?
- [x] Error handling implemented ?
- [x] Navigation properly configured ?

### Database
- [x] Schema includes all required columns ?
- [x] Foreign key relationships configured ?
- [x] No orphaned data ?
- [x] Backup created before deployment ?? **DO THIS**

### Documentation
- [x] Implementation documentation complete ?
- [x] Algorithm documentation complete ?
- [x] Testing guide created ?
- [x] Quick start guide created ?
- [x] Consultation history documentation ? **NEW**

---

## ?? Deployment Steps

### Step 1: Pre-Deployment Backup

```bash
# Backup database
cd C:\Users\mrrov\source\repos\ItzinCopilot\Itzin.Api
cp itzin.db itzin.db.backup.$(date +%Y%m%d_%H%M%S)

# Or on Windows PowerShell
Copy-Item itzin.db -Destination "itzin.db.backup.$(Get-Date -Format 'yyyyMMdd_HHmmss')"
```

### Step 2: Test in Development

```bash
# Terminal 1 - Backend
cd C:\Users\mrrov\source\repos\ItzinCopilot\Itzin.Api
dotnet run

# Terminal 2 - Frontend  
cd C:\Users\mrrov\source\repos\ItzinCopilot\Itzin.Web
npm start
```

**Run through test scenarios from `ADVANCED_CONSULTATION_QUICK_TEST.md`**

#### Advanced Consultation Tests
- [ ] Test advanced consultation with changing lines
- [ ] Test advanced consultation without changing lines
- [ ] Test basic consultation (unchecked)
- [ ] Test skip question flow
- [ ] Test hexagram navigation
- [ ] Verify database records
- [ ] Test on mobile viewport

#### Consultation History Tests ? **NEW**
- [ ] Access history from dashboard
- [ ] View list of consultations
- [ ] Click consultation to view details
- [ ] Verify basic consultation displays correctly
- [ ] Verify advanced consultation displays all hexagrams
- [ ] Test hexagram navigation from history
- [ ] Verify back button navigation
- [ ] Test with consultations that have notes
- [ ] Test empty state (new user with no consultations)

### Step 3: Build for Production

**Backend:**
```bash
cd C:\Users\mrrov\source\repos\ItzinCopilot\Itzin.Api
dotnet publish -c Release -o ./publish
```

**Frontend:**
```bash
cd C:\Users\mrrov\source\repos\ItzinCopilot\Itzin.Web
npm run build -- --configuration production
```

### Step 4: Configuration Updates

**Update `appsettings.Production.json`:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=/path/to/production/itzin.db"
  },
  "JwtSettings": {
    "SecretKey": "YOUR-PRODUCTION-SECRET-KEY-MIN-32-CHARS",
    "Issuer": "ItzinApi",
    "Audience": "ItzinClient",
    "ExpiryMinutes": "60"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

**Frontend environment:**
- Update API endpoint in production environment file
- Ensure CORS is properly configured
- Update any hardcoded URLs

### Step 5: Deploy Files

1. **Backend:**
   - Deploy contents of `Itzin.Api/publish/` to production server
   - Ensure `appsettings.Production.json` is in place
   - Configure IIS/Apache/Nginx
   - Set up systemd service (if Linux)

2. **Frontend:**
   - Deploy contents of `Itzin.Web/dist/` to web server
   - Configure web server (nginx, Apache, IIS)
   - Set up HTTPS
   - Configure redirects for SPA routing

3. **Database:**
   - Copy `itzin.db` to production location
   - Set appropriate file permissions
   - Verify connection string in appsettings

### Step 6: Post-Deployment Verification

```bash
# Test API endpoint
curl -X GET https://your-production-domain.com/api/health

# Test consultation creation (with valid JWT token)
curl -X POST https://your-production-domain.com/api/consultations \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{
    "question": "Production test",
    "tossResults": [7, 8, 9, 6, 7, 8],
    "language": "en",
    "isAdvanced": true
  }'
```

**Check:**
- [ ] API responds correctly
- [ ] Frontend loads and displays properly
- [ ] Can create new consultation
- [ ] Advanced consultation appears correctly
- [ ] All hexagrams display with details
- [ ] Navigation works
- [ ] Consultation history accessible ? **NEW**
- [ ] History displays all consultations ? **NEW**
- [ ] Can view consultation details from history ? **NEW**
- [ ] No console errors
- [ ] Database is being updated

---

## ?? Smoke Tests

### Critical Path Test

1. **User Registration/Login**
   - [ ] Can register new user
   - [ ] Can login
   - [ ] JWT token is issued

2. **Create Basic Consultation**
   - [ ] Can navigate to consultation page
   - [ ] Can uncheck advanced option
   - [ ] Can complete consultation
   - [ ] Results display correctly

3. **Create Advanced Consultation**
   - [ ] Checkbox is checked by default
   - [ ] Can complete consultation  
   - [ ] Advanced section appears
   - [ ] All hexagrams display correctly
   - [ ] Can click hexagrams for details

4. **Consultation History** ? **NEW**
   - [ ] Can access from dashboard
   - [ ] History list displays consultations
   - [ ] Can click consultation to view details
   - [ ] Detail page shows all hexagrams
   - [ ] Can navigate to hexagram details
   - [ ] Back navigation works correctly

5. **Navigation**
   - [ ] Can return to dashboard
   - [ ] Can view consultation history
   - [ ] Can view hexagram details
   - [ ] Back button works

---

## ?? Monitoring

### Key Metrics to Track

1. **Performance:**
   - API response time for consultation creation
   - API response time for history list
   - API response time for consultation detail
   - Frontend page load time
   - Database query performance

2. **Usage:**
   - % of consultations that are advanced
   - Most common hexagrams
   - History page visits
   - Average consultations per user
   - Error rates

3. **Errors:**
   - Failed consultation creations
   - Failed history loads
   - Database connection errors
   - Frontend JavaScript errors

### Logging

**Check logs regularly:**
```bash
# Backend logs
tail -f /path/to/logs/itzin-*.log

# Web server logs (nginx example)
tail -f /var/log/nginx/access.log
tail -f /var/log/nginx/error.log
```

**Monitor for:**
- ? Failed hexagram calculations
- ? Failed history queries
- ? Database connection failures
- ? NULL reference exceptions
- ? JWT validation failures
- ? Unauthorized access attempts

---

## ?? Rollback Plan

### If Issues Occur

1. **Stop Services:**
   ```bash
   # Stop backend
   systemctl stop itzin-api  # or kill process

   # Stop frontend (if applicable)
   systemctl stop nginx  # or restart with old config
   ```

2. **Restore Previous Version:**
   ```bash
   # Restore backend
   rm -rf /path/to/api/*
   cp -r /path/to/backup/previous-version/* /path/to/api/

   # Restore frontend
   rm -rf /var/www/html/*
   cp -r /path/to/backup/previous-frontend/* /var/www/html/

   # Restore database (if needed)
   cp itzin.db.backup.TIMESTAMP itzin.db
   ```

3. **Restart Services:**
   ```bash
   systemctl start itzin-api
   systemctl start nginx
   ```

4. **Verify:**
   - Check that application is accessible
   - Verify consultations work
   - Check logs for errors

---

## ?? Post-Deployment Tasks

### Immediate (Within 1 hour)
- [ ] Verify all smoke tests pass
- [ ] Check error logs
- [ ] Monitor API response times
- [ ] Verify database is being updated
- [ ] Test on multiple devices/browsers
- [ ] Verify history feature works ? **NEW**

### Within 24 hours
- [ ] Review analytics/metrics
- [ ] Check for any user-reported issues
- [ ] Verify backup systems are working
- [ ] Document any issues encountered
- [ ] Monitor history page usage ? **NEW**

### Within 1 week
- [ ] Analyze usage patterns
- [ ] Review performance metrics
- [ ] Gather user feedback
- [ ] Plan any needed hotfixes
- [ ] Analyze consultation vs history page views ? **NEW**

---

## ? Success Criteria

The deployment is successful if:

- [x] API is accessible and responding
- [x] Frontend loads without errors
- [x] Users can create consultations
- [x] Advanced consultation feature works
- [x] All hexagrams display correctly
- [x] Database is being updated
- [x] Consultation history is accessible ? **NEW**
- [x] History displays all consultations correctly ? **NEW**
- [x] Navigation between pages works smoothly ? **NEW**
- [x] No critical errors in logs
- [x] Performance is acceptable (<2s page load, <500ms API)

---

## ?? Security Checklist

- [ ] JWT secret key is strong and unique
- [ ] HTTPS is properly configured
- [ ] CORS is configured appropriately
- [ ] Database file permissions are correct
- [ ] API keys are not exposed in frontend
- [ ] Rate limiting is configured (if applicable)
- [ ] Error messages don't expose sensitive info
- [ ] Authorization checks prevent viewing other users' consultations ? **NEW**

---

## ?? Support

### If Issues Arise

1. **Check Documentation:**
   - `ADVANCED_CONSULTATION_COMPLETE.md`
   - `ADVANCED_CONSULTATION_IMPLEMENTATION.md`
   - `ADVANCED_CONSULTATION_QUICK_TEST.md`
   - `CONSULTATION_HISTORY_COMPLETE.md` ? **NEW**

2. **Check Logs:**
   - Backend: `Itzin.Api/logs/`
   - Web server: System logs
   - Browser console: DevTools

3. **Database Debug:**
   ```bash
   sqlite3 itzin.db "SELECT * FROM Consultations ORDER BY Id DESC LIMIT 5;"
   ```

4. **Rollback if Necessary:**
   - Follow rollback plan above
   - Investigate issue in development
   - Deploy fix when ready

---

## ?? Deployment Timeline

### Recommended Schedule

**Best time to deploy:**
- Off-peak hours (e.g., 2 AM - 6 AM local time)
- Weekdays (not Friday or day before holiday)
- When support staff is available

**Deployment phases:**
1. **T-1 hour:** Create backups
2. **T-30 min:** Build production artifacts
3. **T-15 min:** Final tests in development
4. **T-0:** Begin deployment
5. **T+15 min:** Complete deployment
6. **T+30 min:** Smoke tests complete
7. **T+1 hour:** Monitoring stable
8. **T+24 hours:** Post-deployment review

---

## ?? Deployment Complete!

When all items are checked:
- Feature is live in production
- Users can access advanced consultation
- Users can view consultation history ? **NEW**
- Monitoring is in place
- Support is ready

**Congratulations! The Advanced Consultation and History features are now live!** ??

---

## ?? Feature Summary

### Deployed Features:
1. **Advanced Consultation** ?
   - Anti-Hexagram calculation
   - Changing Hexagram calculation
   - Additional Changing Hexagrams
   - Default enabled with checkbox

2. **Consultation History** ? **NEW**
   - History list page
   - Consultation detail page
   - Full hexagram display
   - Advanced hexagram support
   - Navigation integration

### User Journey:
```
Login ? Dashboard ? New Consultation (Advanced) ? Results ? History ? Detail View ? Hexagram Detail
