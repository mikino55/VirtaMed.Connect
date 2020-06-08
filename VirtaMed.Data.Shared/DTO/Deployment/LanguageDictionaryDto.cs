using System.Collections.Generic;

namespace VirtaMed.Data.Shared.DTO.Deployment
{
    public class LanguageDictionaryDto
    {
        public string Culture { get; set; }

        public List<LanguageEntryDto> Entries { get; set; } = new List<LanguageEntryDto>();
    }
}