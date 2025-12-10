# How to Use the Hexagrams Data 📚

## Quick Access

### View a Specific Hexagram
```bash
# View hexagram 1 (Qian - Creativity)
cat hexagrams_data/hexagram_01.txt

# View hexagram 32 (Heng - Duration)
cat hexagrams_data/hexagram_32.txt

# View hexagram 64 (Wei Ji - Before Completion)
cat hexagrams_data/hexagram_64.txt
```

### Search for a Hexagram
```bash
# Find hexagrams mentioning "creativity"
grep -i "творчество" hexagrams_data/*.txt

# Find all hexagrams with "dragon" references
grep -i "дракон" hexagrams_data/*.txt

# Get line count for all hexagrams
wc -l hexagrams_data/*.txt
```

### Use JSON Data
```bash
# View hexagram 1 in JSON format
cat hexagrams_data/hexagrams_combined.json | jq '.["1"]'

# Extract only the short descriptions
cat hexagrams_data/hexagrams_combined.json | jq '.[] | {number: .number, title: .title, short: .description_short}'

# Get all section titles for hexagram 5
cat hexagrams_data/hexagrams_combined.json | jq '.["5"].sections | keys'
```

## Data Structure Reference

Each hexagram in the JSON contains:

```json
{
  "number": 1,
  "title": "1, Цянь",
  "description_short": "Творчество",
  "description_full": "Сила, творческая энергия...",
  "sections": {
    "Название": "Text about the name/meaning",
    "Образный ряд": "Image series interpretation",
    "Внешний и внутренний миры": "External/internal world meanings",
    "Определение": "Definition",
    "Символ": "Symbol meaning",
    "Линии гексаграммы": "Line interpretations"
  },
  "full_text": "Complete raw text of all content",
  "url": "https://64hex.ru/karcher/1.htm"
}
```

## Integration with ItzinCopilot Application

### 1. Import into Database
```sql
-- Example SQL to import hexagrams
INSERT INTO HexagramRuDescriptions (HexagramId, Name, ShortDescription, FullDescription, Sections)
SELECT 
    JSON_VALUE(data, '$.number') as HexagramId,
    JSON_VALUE(data, '$.title') as Name,
    JSON_VALUE(data, '$.description_short') as ShortDescription,
    JSON_VALUE(data, '$.description_full') as FullDescription,
    JSON_VALUE(data, '$.sections') as Sections
FROM (imported JSON data)
```

### 2. Update C# Models
Add these properties to your `HexagramRuDescription` entity:
```csharp
public int HexagramId { get; set; }
public string Name { get; set; }
public string ShortDescription { get; set; }
public string FullDescription { get; set; }
public Dictionary<string, string> Sections { get; set; }
```

### 3. API Endpoint Example
```csharp
[HttpGet("/{id}/ru-description")]
public async Task<IActionResult> GetRussianDescription(int id)
{
    var description = await _context.HexagramRuDescriptions
        .FirstOrDefaultAsync(h => h.HexagramId == id);
    
    return Ok(description);
}
```

### 4. Frontend Display
```typescript
// Angular component
export class HexagramDetailComponent implements OnInit {
  hexagramData: any;
  
  ngOnInit() {
    this.loadHexagramData();
  }
  
  loadHexagramData() {
    this.apiService.getHexagramDescription(this.hexagramId)
      .subscribe(data => {
        this.hexagramData = data;
      });
  }
}
```

## File Organization

```
hexagrams_data/
├── hexagram_01.txt          # Individual hexagrams (readable text)
├── hexagram_02.txt
├── ...
├── hexagram_64.txt
├── hexagrams_combined.txt    # All hexagrams in one file
└── hexagrams_combined.json   # Structured JSON format
```

## Command Examples

### Extract Specific Information
```bash
# Get all hexagram numbers and titles
grep "^HEXAGRAM" hexagrams_data/hexagrams_combined.txt

# Count total hexagrams
ls -1 hexagrams_data/hexagram_*.txt | wc -l

# Get file sizes
du -sh hexagrams_data/*

# Find hexagrams by a keyword
grep -l "творчество" hexagrams_data/*.txt
```

### Convert to Other Formats
```bash
# Extract to CSV (simple format)
echo "Number,Title,Short,Full" > hexagrams.csv
jq -r '.[] | [.number, .title, .description_short, .description_full] | @csv' \
  hexagrams_data/hexagrams_combined.json >> hexagrams.csv

# Extract to Markdown
for i in {1..64}; do
  cat "hexagrams_data/hexagram_$(printf '%02d' $i).txt" >> hexagrams_all.md
  echo "" >> hexagrams_all.md
done
```

## Performance Notes

- **Individual files**: ~7-8 KB each, fast to load individually
- **Combined JSON**: ~400 KB, good for bulk operations
- **Combined text**: ~500 KB, good for viewing/searching
- **Load time**: Minimal for single hexagrams, ~100ms for all hexagrams

## Backup & Maintenance

```bash
# Create backup
cp -r hexagrams_data/ hexagrams_data_backup/

# Check file integrity
find hexagrams_data/ -name "*.txt" -o -name "*.json" | wc -l

# Verify all 64 hexagrams are present
[[ $(ls hexagrams_data/hexagram_*.txt | wc -l) -eq 64 ]] && echo "✅ All 64 files present"
```

## Related Files

- **Script**: `scrape_hexagrams.py` - Original scraping script
- **Summary**: `HEXAGRAMS_EXTRACTION_SUMMARY.md` - Extraction details
- **Source**: https://64hex.ru/karcher/ - Original website

## Next Steps

1. ✅ Data extracted and organized
2. ⬜ Load into database (use your favorite tool)
3. ⬜ Create API endpoints
4. ⬜ Update frontend to display detailed descriptions
5. ⬜ Link consultations to hexagram details

---

**Last Updated**: December 10, 2025  
**Total Hexagrams**: 64  
**Language**: Russian (Стивен Карчер / Stephen Karcher)

