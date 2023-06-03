using System;
using AutoMapper;
using VailInstructorWikiApi.DTOs;
using VailInstructorWikiApi.Models;

namespace VailInstructorWikiApi
{

    public class AutoMapperConfig
    {
        public static IMapper Configure()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Area, AreaDto>();
                config.CreateMap<AreaDto, Area>();

                config.CreateMap<Drill, DrillDto>();
                config.CreateMap<DrillDto, Drill>();

                config.CreateMap<Level, LevelDto>();
                config.CreateMap<LevelDto, Level>();

                config.CreateMap<RunDiscipline, RunDisciplineDto>();
                config.CreateMap<RunDisciplineDto, RunDiscipline>();

                config.CreateMap<Run, RunDto>();
                config.CreateMap<RunDto, Run>();

                config.CreateMap<Skill, SkillDto>();
                config.CreateMap<SkillDto, Skill>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            return mapper;
        }
    }
}

