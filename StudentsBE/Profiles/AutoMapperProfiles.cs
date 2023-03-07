using AutoMapper;
using StudentsBE.Models;
using StudentsBE.DTO;

namespace StudentsBE.Profiles
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<Student, StudentDto>();
      CreateMap<StudentDto, Student>();
    }
  }
}
