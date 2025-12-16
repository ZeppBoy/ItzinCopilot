#!/usr/bin/env python3
import re

# Read the backup file
with open('/Users/viktorshershnov/AI/Projects/ItzinCopilot/Itzin.Infrastructure/Data/Seed/HexagramRuDescriptionSeedData.cs.backup', 'r', encoding='utf-8') as f:
    content = f.read()

# Pattern to match CreateRuDescription calls
# We need to add two empty strings after the 5th string parameter
pattern = r'descriptions\.Add\(CreateRuDescription\((\d+), (\d+), (".*?"), (".*?"), (".*?"), (".*?"), (".*?"), (".*?"), (".*?")'

def replace_func(match):
    hex_num = int(match.group(1))
    if hex_num >= 19:
        # Add two empty strings after the 5th string (parameter 7)
        return f'descriptions.Add(CreateRuDescription({match.group(1)}, {match.group(2)}, {match.group(3)}, {match.group(4)}, {match.group(5)}, {match.group(6)}, {match.group(7)}, "", "", {match.group(8)}, {match.group(9)}'
    return match.group(0)

# Replace
content = re.sub(pattern, replace_func, content)

# Write back
with open('/Users/viktorshershnov/AI/Projects/ItzinCopilot/Itzin.Infrastructure/Data/Seed/HexagramRuDescriptionSeedData.cs', 'w', encoding='utf-8') as f:
    f.write(content)

print("Fixed all hexagrams 19-64")

