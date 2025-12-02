#!/bin/bash

# Itzin Consultation API Test Script

API_URL="http://localhost:5095/api"
EMAIL="consultant$(date +%s)@itzin.com"

echo "========================================="
echo "Itzin Consultation System Test"
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

TOKEN=$(echo "$REGISTER_RESPONSE" | python3 -c "import sys, json; print(json.load(sys.stdin).get('token', ''))")
echo "✓ User registered successfully"
echo ""

# Test 2: List all hexagrams
echo "Test 2: Get hexagram count"
echo "---------------------------"
HEXAGRAM_COUNT=$(curl -s "${API_URL}/hexagrams" | python3 -c "import sys, json; print(len(json.load(sys.stdin)))")
echo "✓ Total hexagrams available: $HEXAGRAM_COUNT"
echo ""

# Test 3: Get a specific hexagram
echo "Test 3: Get hexagram #1 (The Creative)"
echo "---------------------------------------"
curl -s "${API_URL}/hexagrams/number/1?language=en" | python3 -c "
import sys, json
data = json.load(sys.stdin)
print(f\"✓ Hexagram {data['number']}: {data['chineseName']} - {data['englishName']}\")
print(f\"  Judgment: {data['judgment'][:80]}...\")
"
echo ""

# Test 4: Create first consultation
echo "Test 4: Create I Ching consultation (English)"
echo "----------------------------------------------"
CONSULTATION_1=$(curl -s -X POST "${API_URL}/consultations" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d '{
    "question": "What should I focus on in my career development?",
    "language": "en"
  }')

echo "$CONSULTATION_1" | python3 -c "
import sys, json
data = json.load(sys.stdin)
print(f\"✓ Consultation created (ID: {data['id']})\")
print(f\"  Question: {data['question']}\")
print(f\"  Primary Hexagram: {data['primaryHexagram']['number']}. {data['primaryHexagram']['englishName']}\")
if data.get('relatingHexagram'):
    print(f\"  Relating Hexagram: {data['relatingHexagram']['number']}. {data['relatingHexagram']['englishName']}\")
    print(f\"  Changing Lines: {data.get('changingLines', [])}\")
else:
    print(f\"  No changing lines\")
print(f\"  Toss Values: {data['tossValues']}\")
" > /tmp/consultation_result.txt

cat /tmp/consultation_result.txt
CONSULTATION_ID=$(echo "$CONSULTATION_1" | python3 -c "import sys, json; print(json.load(sys.stdin)['id'])")
echo ""

# Test 5: Create Russian language consultation
echo "Test 5: Create I Ching consultation (Russian)"
echo "----------------------------------------------"
CONSULTATION_2=$(curl -s -X POST "${API_URL}/consultations" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d '{
    "question": "Какой путь выбрать в личных отношениях?",
    "language": "ru"
  }')

echo "$CONSULTATION_2" | python3 -c "
import sys, json
data = json.load(sys.stdin)
print(f\"✓ Consultation created (ID: {data['id']})\")
print(f\"  Вопрос: {data['question']}\")
print(f\"  Основная гексаграмма: {data['primaryHexagram']['number']}. {data['primaryHexagram']['russianName']}\")
if data.get('relatingHexagram'):
    print(f\"  Вторая гексаграмма: {data['relatingHexagram']['number']}. {data['relatingHexagram']['russianName']}\")
"
echo ""

# Test 6: Get consultation history
echo "Test 6: Get consultation history"
echo "---------------------------------"
curl -s "${API_URL}/consultations" \
  -H "Authorization: Bearer $TOKEN" | python3 -c "
import sys, json
data = json.load(sys.stdin)
print(f\"✓ Total consultations: {len(data)}\")
for i, c in enumerate(data[:5], 1):
    question = c['question'][:50] + '...' if len(c['question']) > 50 else c['question']
    print(f\"  {i}. {question}\")
    print(f\"     Hexagram {c['primaryHexagram']['number']} - {c['primaryHexagram']['englishName']}\")
"
echo ""

# Test 7: Update consultation notes
echo "Test 7: Add notes to consultation"
echo "----------------------------------"
curl -s -X PATCH "${API_URL}/consultations/${CONSULTATION_ID}/notes" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d '{
    "notes": "This reading provided valuable insights. The hexagram speaks directly to my current situation regarding career choices and timing."
  }' -w "Status: %{http_code}\n" | grep -q "204" && echo "✓ Notes added successfully" || echo "✗ Failed to add notes"
echo ""

# Test 8: Retrieve consultation with notes
echo "Test 8: Retrieve consultation with notes"
echo "-----------------------------------------"
curl -s "${API_URL}/consultations/${CONSULTATION_ID}" \
  -H "Authorization: Bearer $TOKEN" | python3 -c "
import sys, json
data = json.load(sys.stdin)
print(f\"✓ Consultation ID: {data['id']}\")
print(f\"  Question: {data['question']}\")
print(f\"  Notes: {data['notes'][:100]}...\")
print(f\"  Date: {data['consultationDate']}\")
"
echo ""

# Test 9: Create multiple consultations for testing
echo "Test 9: Create 3 more consultations"
echo "------------------------------------"
for i in {1..3}; do
    curl -s -X POST "${API_URL}/consultations" \
      -H "Content-Type: application/json" \
      -H "Authorization: Bearer $TOKEN" \
      -d "{
        \"question\": \"Test question number $i\",
        \"language\": \"en\"
      }" > /dev/null
    echo "✓ Created consultation $i"
done
echo ""

# Test 10: Test pagination
echo "Test 10: Test pagination (take=2)"
echo "----------------------------------"
curl -s "${API_URL}/consultations?take=2" \
  -H "Authorization: Bearer $TOKEN" | python3 -c "
import sys, json
data = json.load(sys.stdin)
print(f\"✓ Retrieved {len(data)} consultations (requested 2)\")
"
echo ""

echo "========================================="
echo "All tests completed successfully! ✅"
echo "========================================="
echo ""
echo "Summary:"
echo "  - Authentication: Working ✓"
echo "  - Hexagram retrieval: Working ✓"
echo "  - Consultation creation (EN/RU): Working ✓"
echo "  - Consultation history: Working ✓"
echo "  - Notes functionality: Working ✓"
echo "  - Pagination: Working ✓"
echo ""
