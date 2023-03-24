using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsBE.Context;
using StudentsBE.DTO;
using StudentsBE.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.Security.Principal;

namespace StudentsBE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]

    public class StudentController : Controller
    {
        private readonly ContextDB _dbContext;
        private readonly IMapper _mapper;

        public StudentController(ContextDB dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StudentDto>> GetStudents()
        {

            try
            {
                var students = _dbContext.Students.ToList();
                var studentsDto = _mapper.Map<IEnumerable<StudentDto>>(students);
                return Ok(studentsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public ActionResult<StudentDto> GetStudent(int id)
        {
            try
            {

                var student = _dbContext.Students.FirstOrDefault(s => s.Id == id);
                var studentDto = _mapper.Map<StudentDto>(student);
                return Ok(studentDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        // POST: StudentController/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] StudentDto studentDto)
        {
            try
            {
                var student = _mapper.Map<Student>(studentDto);

                _dbContext.Add(student);

                _dbContext.SaveChanges();

                var subjects = _dbContext.Subjects.Include(x => x.Habilities).ToList();

                int studentId = student.Id;

                foreach (var subject in subjects)


                {
                    subject.Habilities.ForEach(skill =>
                    {
                        _dbContext.StudentSubjectHabilities.Add(new StudentSubjectHability()
                        {
                            Score = 0,
                            StudentId = studentId,
                            HabilityId = skill.Id
                        });

                    });
                }

                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, StudentDto studentDto)
        {
            try
            {
                var student = _dbContext.Students.FirstOrDefault(s => s.Id == id);
                _mapper.Map(studentDto, student);
                _dbContext.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {

                var student = _dbContext.Students.FirstOrDefault(s => s.Id == id);
                _dbContext.Students.Remove(student);
                _dbContext.SaveChanges();
                var studentDto = _mapper.Map<StudentDto>(student);
                return Ok(studentDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }




    }

}
