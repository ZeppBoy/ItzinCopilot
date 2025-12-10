#!/usr/bin/env python3
"""Generate C# code for all hexagrams 11-64"""

import json
import re

with open('hexagrams_data/hexagrams_combined.json', 'r', encoding='utf-8') as f:
    hexagrams = json.load(f)

def clean(text):
    """Clean text for C# strings"""
    if not text:
        return ""
    text = re.sub(r'\s+', ' ', str(text)).strip()
    text = text.replace('"', '\\"')
    return text[:250]

# Generate code for hexagrams 11-64
for i in range(11, 65):
    h = hexagrams.get(str(i), {})
    if not h:
        continue
    
    sec = h.get('sections', {})
    
    short = clean(h.get('description_short', ''))
    name = clean(sec.get('Название', ''))
    image = clean(sec.get('Образный ряд', ''))
    full = clean(h.get('description_full', ''))
    inner = clean(sec.get('Внешний и внутренний миры', ''))
    defn = clean(sec.get('Определение', ''))
    symb = clean(sec.get('Символ', ''))
    lines = clean(sec.get('Линии гексаграммы', ''))
    
    # Split lines into 6 parts
    parts = lines.split() if lines else []
    l1 = clean(' '.join(parts[0:30])) if len(parts) > 0 else ""
    l2 = clean(' '.join(parts[30:60])) if len(parts) > 30 else ""
    l3 = clean(' '.join(parts[60:90])) if len(parts) > 60 else ""
    l4 = clean(' '.join(parts[90:120])) if len(parts) > 90 else ""
    l5 = clean(' '.join(parts[120:150])) if len(parts) > 120 else ""
    l6 = clean(' '.join(parts[150:180])) if len(parts) > 150 else ""
    
    print(f'''        descriptions.Add(CreateRuDescription({i}, {i}, "{short}", "{name}", "{image}", "{full}", "{inner}", "{defn}", "{symb}", "{l1}", "{l2}", "{l3}", "{l4}", "{l5}", "{l6}", "{clean(lines[-100:])}"));
''')

