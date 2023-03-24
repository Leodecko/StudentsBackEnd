using AutoMapper;
using StudentsBE.Models;
using StudentsBE.DTO;
using StudentsBE.DTO.Subject;
using StudentsBE.DTO.Skill;

namespace StudentsBE.Profiles
{
  public class AutoMapperProfiles : Profile
  {
        public AutoMapperProfiles()
        {
            CreateMap<Student, StudentDto>().ReverseMap();

            CreateMap<Subject, GetSubjectOutput>().ReverseMap();

            CreateMap<Hability, SkillOutput>().ReverseMap();

            CreateMap<SubjectCreateDto, Subject>()
                .ForMember(dto => dto.Name, opts => opts.MapFrom(record => record.SubjectName)).ReverseMap();

            CreateMap<Hability, SkillCreateDto>().ReverseMap()
                .ForMember(dto => dto.Name, opts => opts.MapFrom(record => record.Name)).ReverseMap();
           


        }
  }
}
