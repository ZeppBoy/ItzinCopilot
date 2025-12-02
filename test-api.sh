#!/bin/bash

# Itzin API Test Script
# This script tests the authentication endpoints

API_URL="http://localhost:5095/api"
EMAIL="test$(date +%s)@itzin.com"

echo "========================================="
echo "Itzin API Test Suite"
echo "========================================="
echo ""

# Test 1: Register a new user
echo "Test 1: Register a new user"
echo "----------------------------"
REGISTER_RESPONSE=$(curl -s -X POST "${API_URL}/auth/register" \
  -H "Content-Type: application/json" \
  -d "{
    \"email\": \"${EMAIL}\",
    \"password\": \"TestPassword123!\",
    \"firstName\": \"John\",
    \"lastName\": \"Doe\",
    \"preferredLanguage\": \"en\"
  }")

echo "$REGISTER_RESPONSE" | python3 -m json.tool
TOKEN=$(echo "$REGISTER_RESPONSE" | python3 -c "import sys, json; print(json.load(sys.stdin).get('token', ''))")
echo ""

# Test 2: Login with correct credentials
echo "Test 2: Login with correct credentials"
echo "---------------------------------------"
LOGIN_RESPONSE=$(curl -s -X POST "${API_URL}/auth/login" \
  -H "Content-Type: application/json" \
  -d "{
    \"email\": \"${EMAIL}\",
    \"password\": \"TestPassword123!\"
  }")

echo "$LOGIN_RESPONSE" | python3 -m json.tool
echo ""

# Test 3: Login with wrong password
echo "Test 3: Login with wrong password (should fail)"
echo "------------------------------------------------"
curl -s -X POST "${API_URL}/auth/login" \
  -H "Content-Type: application/json" \
  -d "{
    \"email\": \"${EMAIL}\",
    \"password\": \"WrongPassword123!\"
  }" -w "\nHTTP Status: %{http_code}\n"
echo ""

# Test 4: Register with duplicate email
echo "Test 4: Register with duplicate email (should fail)"
echo "----------------------------------------------------"
curl -s -X POST "${API_URL}/auth/register" \
  -H "Content-Type: application/json" \
  -d "{
    \"email\": \"${EMAIL}\",
    \"password\": \"AnotherPassword123!\",
    \"firstName\": \"Jane\",
    \"lastName\": \"Doe\",
    \"preferredLanguage\": \"ru\"
  }" -w "\nHTTP Status: %{http_code}\n"
echo ""

echo "========================================="
echo "All tests completed!"
echo "========================================="
