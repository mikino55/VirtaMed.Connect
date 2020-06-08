using System;
using System.Collections.Generic;
using System.Text;

namespace VirtaMed.Data.Shared.DTO.Deployment
{
    public class LocalizationDto
    {
        public Guid SimulatorIdentifier { get; set; }

        public string SimulatorVersion { get; set; }

        public List<LanguageDictionaryDto> Dictionaries { get; set; } = new List<LanguageDictionaryDto>();
    }
}
