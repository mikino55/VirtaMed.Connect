using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VirtaMed.Data.Entity;
using VirtaMed.Data.Shared.DTO.Course;
using VirtaMed.Data.Shared.DTO.Deployment;

namespace VirtaMed.Services.Mapping
{
    public interface IDataMapper
    {
        TDestination Map<TDestination>(object source);
    }

    public class DataMapper : IDataMapper
    {
        private readonly IMapper mapper;

        public DataMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => ConfigureEntityToDto(cfg));
            this.mapper = config.CreateMapper();
        }

        public TDestination Map<TDestination>(object source)
        {
            return this.mapper.Map<TDestination>(source);
        }

        private void ConfigureEntityToDto(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<SimulatorDto, Simulator>()
                 .ForMember(d => d.Identifier, o => o.MapFrom(s => s.Identifier))
                 .ForMember(d => d.NameLanguageKey, o => o.MapFrom(s => s.NameLanguageKey))
                 .ForMember(d => d.SceneSections, o => o.MapFrom(s => s.Sections));

            cfg.CreateMap<SceneSectionDto, SceneSection>()
                 .ForMember(d => d.SceneIdentifiers, o => o.Ignore());

            cfg.CreateMap<SceneDto, Scene>()
                 .ForMember(d => d.ReportSections, o => o.MapFrom(s => s.ReportSections));

            cfg.CreateMap<ReportSectionDto, ReportSection>()
                 .ForMember(d => d.ReportItems, o => o.MapFrom(s => s.ReportItems));

            cfg.CreateMap<ReportItemDto, ReportItem>()
                .ForMember(d => d.MaxScore, o => o.MapFrom(s => s.MaxScore));


            cfg.CreateMap<LocalizationDto, SimulatorLocalization>()
                .ForMember(d => d.EntityType, o => o.MapFrom(s => EntityType.Simulator))
                .ForMember(d => d.Dictionaries, o => o.MapFrom(s => s.Dictionaries))
                .ForMember(d => d.SimulatorIdentifier, o => o.MapFrom(s => s.SimulatorIdentifier))
                .ForMember(d => d.SimulatorVersion, o => o.MapFrom(s => s.SimulatorVersion));

            cfg.CreateMap<LanguageDictionaryDto, LanguageDictionary>()
                .ForMember(d => d.CultureName, o => o.MapFrom(s => s.Culture))
                .ForMember(d => d.Entries, o => o.MapFrom(s => s.Entries));

            cfg.CreateMap<LanguageEntryDto, LanguageEntry>()
               .ForMember(d => d.LanguageKey, o => o.MapFrom(s => s.LanguageKey))
               .ForMember(d => d.LanguageValue, o => o.MapFrom(s => s.LanguageValue));


            cfg.CreateMap<CourseDto, CourseEntity>()
                .ForMember(d => d.DescriptionLanguageKey, o => o.MapFrom(s => s.DescriptionLanguageKey))
                .ForMember(d => d.Sections, o => o.MapFrom(s => s.Sections))
                .ForMember(d => d.Dictionaries, o => o.MapFrom(s => s.Dictionaries));

            cfg.CreateMap<CourseSectionDto, CourseSectionEntity>()
                .ForMember(d => d.SectionType, o => o.MapFrom(s => s.SectionType))
                .ForMember(d => d.Entries, o => o.MapFrom(s => s.Entries));

            cfg.CreateMap<CourseEntryDto, CourseEntryEntity>()
                .ForMember(d => d.SceneIdentifier, o => o.MapFrom(s => s.SceneIdentifier))
                .ForMember(d => d.StarCriteria1, o => o.MapFrom(s => s.StarCriteria1))
                .ForMember(d => d.StarCriteria2, o => o.MapFrom(s => s.StarCriteria2))
                .ForMember(d => d.StarCriteria3, o => o.MapFrom(s => s.StarCriteria3));
        }

    }
}
