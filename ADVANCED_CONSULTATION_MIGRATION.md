# Add Advanced Consultation Migration

Run this command to create a migration for the new Advanced Consultation properties:

```bash
cd Itzin.Infrastructure
dotnet ef migrations add AddAdvancedConsultation --startup-project ../Itzin.Api
```

Then apply the migration:

```bash
dotnet ef database update --startup-project ../Itzin.Api
```

## Migration Details

This migration adds the following columns to the `Consultations` table:

1. **IsAdvanced** (bool, default: false) - Flag indicating if this is an advanced consultation
2. **AntiHexagramId** (int?, nullable) - ID of the anti-hexagram (inverse of primary)
3. **ChangingHexagramId** (int?, nullable) - ID of the changing hexagram
4. **AdditionalChangingHexagrams** (string?, nullable) - Comma-separated list of additional hexagram IDs

## Foreign Key Relationships

- AntiHexagramId ? Hexagrams(Id)
- ChangingHexagramId ? Hexagrams(Id)
