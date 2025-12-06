#!/bin/bash

echo "========================================"
echo "Git Commit and Push"
echo "========================================"
echo ""

cd "$(dirname "$0")"

echo "📁 Current directory: $(pwd)"
echo ""

# Check git status
echo "1️⃣  Git Status:"
git status --short
echo ""

# Stage all changes
echo "2️⃣  Staging all changes..."
git add -A
echo "✅ Files staged"
echo ""

# Show what will be committed
echo "3️⃣  Files to commit:"
git diff --cached --name-only
echo ""

# Commit with message
echo "4️⃣  Committing changes..."
git commit -m "Fix: View Full Interpretation button, consultation creation, and back navigation

- Fixed 'View Full Interpretation' button not working
  - Updated consultation model to match API response structure
  - Fixed property access in templates (primaryHexagram.id instead of primaryHexagramId)
  - Added proper event handling with stopPropagation
  - Updated history-list component for compatibility

- Fixed consultation creation errors
  - Added GetHexagramByIdAsync method to service interface and implementation
  - Fixed HexagramsController to use correct method
  - Added language property to CreateConsultationRequest
  - Updated consultation flow to send language parameter

- Fixed back button navigation from hexagram detail
  - Changed goBack() to use Location.back() instead of hardcoded route
  - Now properly returns to previous page in browser history

- Added documentation and helper scripts
  - CONSULTATION_ERROR_FIX.md
  - BACK_BUTTON_FIX.md
  - START_SERVERS.md
  - start-backend.sh
  - debug-consultation.sh"

COMMIT_EXIT=$?
echo ""

if [ $COMMIT_EXIT -eq 0 ]; then
    echo "✅ Commit successful"
    echo ""
    
    # Show current branch
    echo "5️⃣  Current branch:"
    BRANCH=$(git branch --show-current)
    echo "   $BRANCH"
    echo ""
    
    # Show remote
    echo "6️⃣  Remote repository:"
    git remote -v
    echo ""
    
    # Push to GitHub
    echo "7️⃣  Pushing to GitHub..."
    git push origin "$BRANCH"
    
    PUSH_EXIT=$?
    echo ""
    
    if [ $PUSH_EXIT -eq 0 ]; then
        echo "✅ Successfully pushed to GitHub!"
        echo ""
        echo "🎉 All changes committed and pushed!"
    else
        echo "⚠️  Push failed. Error code: $PUSH_EXIT"
        echo ""
        echo "Possible issues:"
        echo "  - No remote configured"
        echo "  - Authentication required"
        echo "  - Network connection issues"
        echo ""
        echo "Try manually:"
        echo "  git push origin $BRANCH"
    fi
else
    echo "⚠️  Commit failed or nothing to commit"
    echo ""
    
    if [ $COMMIT_EXIT -eq 1 ]; then
        echo "ℹ️  This usually means there are no changes to commit"
        echo ""
        echo "Current status:"
        git status
    fi
fi

echo ""
echo "========================================"
echo "Latest commit:"
git log -1 --oneline
echo "========================================"

