#!/bin/bash

# Script to start the Itzin API backend
# Created: December 6, 2025

echo "========================================"
echo "Starting Itzin API Backend"
echo "========================================"
echo ""

# Navigate to API directory
cd "$(dirname "$0")/Itzin.Api" || exit 1

# Check if port 5000 is in use
if lsof -Pi :5000 -sTCP:LISTEN -t >/dev/null ; then
    echo "⚠️  Port 5000 is already in use!"
    echo ""
    echo "Killing existing process..."
    lsof -ti:5000 | xargs kill -9 2>/dev/null
    sleep 2
    echo "✅ Port cleared"
    echo ""
fi

# Start the backend
echo "🚀 Starting backend on http://localhost:5000"
echo "📚 Swagger UI will be available at http://localhost:5000/swagger"
echo ""
echo "Press Ctrl+C to stop the server"
echo "========================================"
echo ""

dotnet run --launch-profile http

