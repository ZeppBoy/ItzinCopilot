#!/usr/bin/env python3
"""
Generate C# descriptions code for hexagrams 4-64
"""

import os
import re
from pathlib import Path

def read_section(text, section_header):
    """Extract a section from hexagram text"""
    # Find the section header
    idx = text.find(section_header + ":")
    if idx == -1:
        return ""
    
    # Find the content after the header
    start = idx + len(section_header) + 1
    
    # Find where the section ends (at the next header or section)
    next_header = len(text)
    for header in ["Название:", "Образный ряд:", "Внешний и внутренний миры:", 
                   "Скрытая возможность:", "Определение:", "Символ:", "Линии гексаграммы:",
                   "Source:"]:
        pos = text.find(header, start)
        if pos != -1 and pos < next_header:
            next_header = pos
    
    content = text[start:next_header].strip()
    # Remove dashes
    content = re.sub(r'-{5,}', '', content)
    # Clean whitespace
    content = re.sub(r'\s+', ' ', content).strip()
    return content[:400]  # Limit to 400 chars

def escape_for_csharp(text):
    """Prepare text for C# string"""
    if not text:
        return ""
    # Remove extra whitespace
    text = re.sub(r'\s+', ' ', text).strip()
    # Escape quotes and backslashes
    text = text.replace('\\', '\\\\').replace('"', '\\"')
    # Limit to 300 chars to be safe
    return text[:300]

def parse_hexagram(hex_num):
    """Parse hexagram file and extract key data"""
    filepath = Path(f'/Users/viktorshershnov/AI/Projects/ItzinCopilot/hexagrams_data/hexagram_{hex_num:02d}.txt')
    
    if not filepath.exists():
        return None
    
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()
    
    # Extract title
    title_match = re.search(r'Title: (.+)', content)
    title = title_match.group(1).strip() if title_match else f"Hexagram {hex_num}"
    
    # Extract short description
    short_match = re.search(r'Short Description: (.+?)(?=\n\n)', content, re.DOTALL)
    short = escape_for_csharp(short_match.group(1)) if short_match else ""
    
    # Extract sections
    name = escape_for_csharp(read_section(content, 'Название'))
    image_row = escape_for_csharp(read_section(content, 'Образный ряд'))
    inner_outer = escape_for_csharp(read_section(content, 'Внешний и внутренний миры'))
    definition = escape_for_csharp(read_section(content, 'Определение'))
    symbol = escape_for_csharp(read_section(content, 'Символ'))
    
    # Extract line descriptions
    lines_section = read_section(content, 'Линии гексаграммы')
    lines = lines_section.split('Вторая') if 'Вторая' in lines_section else ['']
    
    line1 = escape_for_csharp(lines[0][:150]) if lines else ""
    line2 = escape_for_csharp(lines[1][:150]) if len(lines) > 1 else ""
    line3 = escape_for_csharp(lines_section[len(lines_section)//5:2*len(lines_section)//5][:150]) if lines_section else ""
    line4 = escape_for_csharp(lines_section[2*len(lines_section)//5:3*len(lines_section)//5][:150]) if lines_section else ""
    line5 = escape_for_csharp(lines_section[3*len(lines_section)//5:4*len(lines_section)//5][:150]) if lines_section else ""
    line6 = escape_for_csharp(lines_section[4*len(lines_section)//5:][:150]) if lines_section else ""
    
    return {
        'id': hex_num,
        'short': short,
        'name': name,
        'image_row': image_row,
        'description': escape_for_csharp(short),  # Use short as description for now
        'inner_outer': inner_outer,
        'definition': definition,
        'symbol': symbol,
        'line1': line1,
        'line2': line2,
        'line3': line3,
        'line4': line4,
        'line5': line5,
        'line6': line6
    }

# Generate C# code for hexagrams 4-9
for i in range(4, 10):
    data = parse_hexagram(i)
    if data:
        print(f"""        descriptions.Add(CreateRuDescription(
            {i}, {i},
            "{data['short']}",
            "{data['name']}",
            "{data['image_row']}",
            "{data['description']}",
            "{data['inner_outer']}",
            "{data['definition']}",
            "{data['symbol']}",
            "{data['line1']}",
            "{data['line2']}",
            "{data['line3']}",
            "{data['line4']}",
            "{data['line5']}",
            "{data['line6']}",
            "{escape_for_csharp(data['image_row'][-100:])}"
        ));
""")

