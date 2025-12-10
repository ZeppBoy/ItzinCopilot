#!/usr/bin/env python3
"""Generate C# seed data for hexagrams 4-64"""

import json
import re

# Load JSON
with open('hexagrams_data/hexagrams_combined.json', 'r', encoding='utf-8') as f:
    data = json.load(f)

def esc(s):
    """Escape for C#"""
    if not s: return ""
    s = re.sub(r'\s+', ' ', str(s)).strip()
    s = s.replace('"', '\\"')
    return s[:250]

# Generate code
result = []
for i in range(4, 65):
    h = data.get(str(i), {})
    if not h:
        continue
    
    sec = h.get('sections', {})
    short = esc(h.get('description_short', ''))
    name = esc(sec.get('Название', ''))
    image = esc(sec.get('Образный ряд', ''))
    full = esc(h.get('description_full', ''))
    inner = esc(sec.get('Внешний и внутренний миры', ''))
    defn = esc(sec.get('Определение', ''))
    symb = esc(sec.get('Символ', ''))
    lines = esc(sec.get('Линии гексаграммы', ''))
    
    # Split lines
    parts = lines.split(' ') if lines else []
    l1 = esc(' '.join(parts[0:10]))
    l2 = esc(' '.join(parts[10:20]))
    l3 = esc(' '.join(parts[20:30]))
    l4 = esc(' '.join(parts[30:40]))
    l5 = esc(' '.join(parts[40:50]))
    l6 = esc(' '.join(parts[50:]))
    
    code = f'''        descriptions.Add(CreateRuDescription(
            {i}, {i},
            "{short}",
            "{name}",
            "{image}",
            "{full}",
            "{inner}",
            "{defn}",
            "{symb}",
            "{l1}",
            "{l2}",
            "{l3}",
            "{l4}",
            "{l5}",
            "{l6}",
            "{esc(lines[-100:] if lines else '')}"
        ));
'''
    result.append(code)

# Print all
for code in result:
    print(code)

