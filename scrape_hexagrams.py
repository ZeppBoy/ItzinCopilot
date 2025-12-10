#!/usr/bin/env python3
"""
Script to scrape all 64 hexagram descriptions from https://64hex.ru/karcher/
Saves each hexagram to a separate file and also creates a combined file.
"""

import requests
import os
import json
from pathlib import Path
from bs4 import BeautifulSoup
import time

# Create output directory
output_dir = Path("hexagrams_data")
output_dir.mkdir(exist_ok=True)

# URL pattern
base_url = "https://64hex.ru/karcher/{}.htm"

# Dictionary to store all hexagrams
all_hexagrams = {}

print("🔄 Starting to scrape hexagrams from 64hex.ru...")
print(f"📁 Output directory: {output_dir.absolute()}\n")

# Scrape all 64 hexagrams
for i in range(1, 65):
    try:
        url = base_url.format(i)
        print(f"📥 Fetching hexagram {i}/64...", end=" ", flush=True)
        
        headers = {
            'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36'
        }
        response = requests.get(url, headers=headers, timeout=10)
        response.encoding = 'utf-8'
        
        if response.status_code == 200:
            soup = BeautifulSoup(response.content, 'html.parser')
            
            # Extract title
            title_tag = soup.find('h1')
            title = title_tag.text.strip() if title_tag else f"Hexagram {i}"
            
            # Extract description (desc1 and desc2 classes)
            desc1 = ""
            desc2 = ""
            desc1_tag = soup.find('div', class_='desc1')
            desc2_tag = soup.find('div', class_='desc2')
            
            if desc1_tag:
                desc1 = desc1_tag.text.strip()
            if desc2_tag:
                desc2 = desc2_tag.text.strip()
            
            # Extract all content from centercol (the main content area)
            centercol = soup.find('td', id='centercol')
            full_content = ""
            if centercol:
                full_content = centercol.get_text(separator='\n', strip=True)
            
            # Extract h2 sections
            sections = {}
            if centercol:
                h2_tags = centercol.find_all('h2')
                for h2 in h2_tags:
                    section_title = h2.text.strip()
                    # Get all content until next h2
                    content_lines = []
                    sibling = h2.next_sibling
                    while sibling:
                        if hasattr(sibling, 'name') and sibling.name == 'h2':
                            break
                        if isinstance(sibling, str):
                            text = sibling.strip()
                            if text:
                                content_lines.append(text)
                        elif hasattr(sibling, 'get_text'):
                            text = sibling.get_text(strip=True)
                            if text:
                                content_lines.append(text)
                        sibling = sibling.next_sibling
                    sections[section_title] = '\n'.join(content_lines)
            
            # Store in dictionary
            hexagram_data = {
                "number": i,
                "title": title,
                "description_short": desc1,
                "description_full": desc2,
                "sections": sections,
                "full_text": full_content,
                "url": url
            }
            
            all_hexagrams[str(i)] = hexagram_data
            
            # Save individual file
            individual_file = output_dir / f"hexagram_{i:02d}.txt"
            with open(individual_file, 'w', encoding='utf-8') as f:
                f.write(f"{'='*60}\n")
                f.write(f"HEXAGRAM {i}\n")
                f.write(f"{'='*60}\n\n")
                f.write(f"Title: {title}\n\n")
                if desc1:
                    f.write(f"Short Description: {desc1}\n\n")
                if desc2:
                    f.write(f"Full Description:\n{desc2}\n\n")
                if sections:
                    f.write("SECTIONS:\n")
                    for section_title, section_content in sections.items():
                        f.write(f"\n{section_title}:\n")
                        f.write(f"{'-'*40}\n")
                        f.write(f"{section_content}\n")
                f.write(f"\nSource: {url}\n")
            
            print(f"✅ Saved")
            
            # Be respectful to the server
            time.sleep(0.5)
        else:
            print(f"❌ Failed (Status: {response.status_code})")
    
    except requests.exceptions.RequestException as e:
        print(f"❌ Error: {str(e)}")
        time.sleep(1)
    except Exception as e:
        print(f"❌ Parse error: {str(e)}")

# Save combined JSON file
json_file = output_dir / "hexagrams_combined.json"
with open(json_file, 'w', encoding='utf-8') as f:
    json.dump(all_hexagrams, f, ensure_ascii=False, indent=2)

# Save combined text file
text_file = output_dir / "hexagrams_combined.txt"
with open(text_file, 'w', encoding='utf-8') as f:
    f.write("ALL 64 HEXAGRAMS FROM 64HEX.RU (STEPHEN KARCHER)\n")
    f.write("=" * 70 + "\n\n")
    
    for i in range(1, 65):
        if str(i) in all_hexagrams:
            hx = all_hexagrams[str(i)]
            f.write(f"\n{'='*70}\n")
            f.write(f"HEXAGRAM {hx['number']}: {hx['title']}\n")
            f.write(f"{'='*70}\n\n")
            
            if hx['description_short']:
                f.write(f"SHORT: {hx['description_short']}\n\n")
            
            if hx['description_full']:
                f.write(f"FULL DESCRIPTION:\n{hx['description_full']}\n\n")
            
            if hx['sections']:
                f.write("SECTIONS:\n")
                f.write("-" * 70 + "\n")
                for section_title, section_content in hx['sections'].items():
                    f.write(f"\n{section_title}:\n")
                    f.write(section_content + "\n")
            
            f.write(f"\n📍 URL: {hx['url']}\n")

print(f"\n{'='*60}")
print(f"✨ Scraping completed!")
print(f"{'='*60}")
print(f"📊 Total hexagrams extracted: {len(all_hexagrams)}/64")
print(f"\n📁 Output files:")
print(f"  • Individual files: {output_dir}/hexagram_01.txt to hexagram_64.txt")
print(f"  • Combined text: {output_dir}/hexagrams_combined.txt")
print(f"  • Combined JSON: {output_dir}/hexagrams_combined.json")
print(f"\n📍 Output directory: {output_dir.absolute()}")

