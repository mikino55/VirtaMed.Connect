using System;
using System.Collections.Generic;
using System.Text;
using VirtaMed.Data.Shared.Enums;

namespace VirtaMed.Data.Entity
{
    public class CourseEntity : MetaEntityBase
    {
        public string DescriptionLanguageKey { get; set; }

        public List<CourseSectionEntity> Sections { get; set; }

        public List<LanguageDictionary> Dictionaries { get; set; } = new List<LanguageDictionary>();
    }

    public class CourseSectionEntity : MetaEntityBase
    {
        public CourseSectionType SectionType { get; set; }

        public List<CourseEntryEntity> Entries { get; set; }
    }

    public class CourseEntryEntity
    {
        public Guid SceneIdentifier { get; set; }

        public double StarCriteria1 { get; set; }

        public double StarCriteria2 { get; set; }

        public double StarCriteria3 { get; set; }
    }
}
