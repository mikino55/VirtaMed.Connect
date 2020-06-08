using System;
using System.Collections.Generic;
using VirtaMed.Data.Shared.DTO;

namespace VirtaMed.Data.Shared.DTO.Deployment
{
    public class SceneDto : MetaEntityDto
    {
        public List<ReportSectionDto> ReportSections { get; set; } = new List<ReportSectionDto>();
    }
}