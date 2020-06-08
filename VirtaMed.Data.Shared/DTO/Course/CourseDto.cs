using System;
using System.Collections.Generic;
using System.Text;
using VirtaMed.Data.Shared.DTO.Deployment;
using VirtaMed.Data.Shared.Enums;

namespace VirtaMed.Data.Shared.DTO.Course
{
    public class CourseDto : MetaEntityDto
    {
        public string DescriptionLanguageKey { get; set; }

        public List<CourseSectionDto> Sections { get; set; }

        public List<LanguageDictionaryDto> Dictionaries { get; set; } = new List<LanguageDictionaryDto>();
    }

    public class CourseSectionDto : MetaEntityDto
    {
        public CourseSectionType SectionType { get; set; }

        public List<CourseEntryDto> Entries { get; set; }
    }

    public class CourseEntryDto : MetaEntityDto
    {
        public Guid SceneIdentifier { get; set; }

        public double StarCriteria1 { get; set; }

        public double StarCriteria2 { get; set; }

        public double StarCriteria3 { get; set; }
    }
}
