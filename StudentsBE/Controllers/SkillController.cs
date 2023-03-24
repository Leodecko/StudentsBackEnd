using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsBE.Context;
using StudentsBE.DTO.Skill;
using StudentsBE.DTO.Subject;
using StudentsBE.Models;
using System.Data;

namespace StudentsBE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class SkillController : Controller
    {
        private readonly ContextDB _db;

        private readonly IMapper _mapper;

        public SkillController(ContextDB db, IMapper mapper)
        {
            _db = db;

            _mapper = mapper;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Create(int subjectId, SkillCreateDto skill)
        {
            try
            {
                var currentSubject = _db.Subjects.Where(x => x.Id == subjectId);

                if (currentSubject is null)
                {
                    return BadRequest($"the subject with the id {subjectId} was not found");
                }

                var newSkill = _mapper.Map<Hability>(skill);

                newSkill.SubjectId = subjectId;

                await _db.Habilities.AddAsync(newSkill);

                await _db.SaveChangesAsync();

                var students = _db.Students.ToList();

                List<StudentSubjectHability> studentHabilities = students.Select(student => new StudentSubjectHability() { HabilityId = newSkill.Id, StudentId = student.Id, Score = 0 }).ToList();

                await _db.StudentSubjectHabilities.AddRangeAsync(studentHabilities);

                await _db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
