using System.Collections.Generic;
using VirtaMed.Data.Shared.DTO;

namespace VirtaMed.Data.Shared.DTO.Deployment
{
    public class ReportSectionDto : MetaEntityDto
    {
        public List<ReportItemDto> ReportItems { get; set; } = new List<ReportItemDto>();
    }
}