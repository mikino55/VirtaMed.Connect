using System;
using System.Collections.Generic;

namespace VirtaMed.Data.Shared.DTO.Deployment
{
    public class SimulatorDto
    {
        public string SimulatorFamilyNameLanguageKey { get; set; }

        public Guid SimulatorFamilyIdentifier { get; set; }

        public string CompanyName { get; set; }

        public string NameLanguageKey { get; set; }

        public Guid Identifier { get; set; }

        public string Version { get; set; }

        public List<SceneSectionDto> Sections { get; set; } = new List<SceneSectionDto>();

        public LocalizationDto Localization { get; set; }
    }
}
