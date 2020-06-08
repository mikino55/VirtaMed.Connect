using System;
using System.Collections.Generic;
using VirtaMed.Data.Shared.DTO;

namespace VirtaMed.Data.Shared.DTO.Deployment
{
    public class SceneSectionDto : MetaEntityDto
    {
        public List<SceneDto> Scenes { get; set; } = new List<SceneDto>();

    }
}