#!/usr/bin/env python3
"""
Generate C# seed data entries from hexagram text files
"""

import os
import re
from pathlib import Path

def extract_section(text, section_name):
    """Extract a section from hexagram text"""
    pattern = rf"{section_name}:\s*-+\s*(.*?)(?=\n[A-Z]|$)"
    match = re.search(pattern, text, re.DOTALL | re.IGNORECASE)
    if match:
        content = match.group(1).strip()
        # Clean up whitespace
        content = re.sub(r'\s+', ' ', content)
        return content[:500]  # Limit to 500 chars
    return ""

def escape_csharp_string(text):
    """Escape text for C# string literals"""
    if not text:
        return ""
    # Remove extra whitespace
    text = re.sub(r'\s+', ' ', text).strip()
    # Escape backslashes first
    text = text.replace('\\', '\\\\')
    # Escape quotes
    text = text.replace('"', '\\"')
    # Limit length
    return text[:450]

def parse_hexagram_file(filepath):
    """Parse a hexagram text file and extract key information"""
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()
    
    # Extract title
    title_match = re.search(r'Title: (.+)', content)
    title = title_match.group(1).strip() if title_match else ""
    
    # Extract number from filename
    filename = os.path.basename(filepath)
    hex_num = int(filename.split('_')[1].split('.')[0])
    
    # Extract short description
    short_match = re.search(r'Short Description: (.+?)(?=\n\n|Full Description)', content, re.DOTALL)
    short_desc = escape_csharp_string(short_match.group(1)) if short_match else ""
    
    # Extract full description
    full_match = re.search(r'Full Description:\s*(.+?)(?=SECTIONS:|$)', content, re.DOTALL)
    full_desc = escape_csharp_string(full_match.group(1)) if full_match else ""
    
    # Extract sections
    sections = {
        'Название': extract_section(content, 'Название'),
        'Образный ряд': extract_section(content, 'Образный ряд'),
        'Внешний и внутренний миры': extract_section(content, 'Внешний и внутренний миры'),
        'Определение': extract_section(content, 'Определение'),
        'Символ': extract_section(content, 'Символ'),
        'Линии гексаграммы': extract_section(content, 'Линии гексаграммы'),
    }
    
    return {
        'id': hex_num,
        'hexagramId': hex_num,
        'title': title,
        'short': escape_csharp_string(short_desc),
        'name': escape_csharp_string(sections['Название']),
        'imageRow': escape_csharp_string(sections['Образный ряд']),
        'description': escape_csharp_string(full_desc),
        'innerOuterWorlds': escape_csharp_string(sections['Внешний и внутренний миры']),
        'definition': escape_csharp_string(sections['Определение']),
        'symbol': escape_csharp_string(sections['Символ']),
        'lines': escape_csharp_string(sections['Линии гексаграммы']),
    }

def generate_csharp_code(hex_data):
    """Generate C# code for a single hexagram"""
    h = hex_data
    
    # Split lines content into individual lines (simplified)
    lines = h['lines'].split('Вторая') if 'Вторая' in h['lines'] else [''] * 6
    
    line1 = escape_csharp_string(lines[0][:150]) if lines else ""
    line2 = escape_csharp_string(lines[1][:150]) if len(lines) > 1 else ""
    line3 = escape_csharp_string(lines[2][:150] if len(lines) > 2 else "")
    line4 = escape_csharp_string(h['lines'][len(h['lines'])//4:len(h['lines'])//2][:150]) if h['lines'] else ""
    line5 = escape_csharp_string(h['lines'][len(h['lines'])//2:3*len(h['lines'])//4][:150]) if h['lines'] else ""
    line6 = escape_csharp_string(h['lines'][3*len(h['lines'])//4:][:150]) if h['lines'] else ""
    
    code = f'''        descriptions.Add(CreateRuDescription(
            {h['id']}, {h['hexagramId']},
            "{h['short']}",
            "{h['name']}",
            "{h['imageRow']}",
            "{h['description']}",
            "{h['innerOuterWorlds']}",
            "{h['definition']}",
            "{h['symbol']}",
            "{line1}",
            "{line2}",
            "{line3}",
            "{line4}",
            "{line5}",
            "{line6}",
            "{escape_csharp_string(h['lines'][-100:] if h['lines'] else '')}"
        ));
'''
    return code

# Main execution
hexagrams_dir = Path('/Users/viktorshershnov/AI/Projects/ItzinCopilot/hexagrams_data')

print("// Auto-generated from hexagram text files")
print("// for hexagrams 2-64\n")

for i in range(2, 65):
    filepath = hexagrams_dir / f'hexagram_{i:02d}.txt'
    
    if filepath.exists():
        try:
            hex_data = parse_hexagram_file(filepath)
            code = generate_csharp_code(hex_data)
            print(code)
        except Exception as e:
            print(f"// Error processing hexagram {i}: {e}")
    else:
        print(f"// File not found: {filepath}")

print(f"\n// Total: 63 hexagram entries generated (2-64)")

