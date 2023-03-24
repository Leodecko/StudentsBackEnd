using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsBE.Context;
using StudentsBE.DTO;
using StudentsBE.Models;

namespace StudentsBE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class ScoreStudentController : Controller
    {
        private readonly ContextDB _dbContext;

        public ScoreStudentController(ContextDB dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{studentId}")]
        public ActionResult GetSkills(int studentId)
        {
            var habilitiesScore = _dbContext.StudentSubjectHabilities.Where(x => x.StudentId == studentId);

            List<GetSubjectOutput> result = new List<GetSubjectOutput>();

            List<SkillOutput> resultSkills = new List<SkillOutput>();

            var currentSubjectResponse = new GetSubjectOutput();

            var subjects = _dbContext.Subjects.Include(x => x.Habilities).ToList();

            foreach (var subject in subjects)
            { 

                currentSubjectResponse.Skills = new List<SkillOutput>();

                currentSubjectResponse.Name = subject.Name;

                currentSubjectResponse.Id = subject.Id;

                subject.Habilities.ForEach(hability =>
                {
                    var skillScore = habilitiesScore.FirstOrDefault(x => x.HabilityId == hability.Id);

                    var skill = new SkillOutput()
                    {
                        Score = skillScore.Score,
                        Id = skillScore.Id,
                        Name = hability.Name,
                    };

                    currentSubjectResponse.Skills.Add(skill);

                });

                result.Add(currentSubjectResponse);

                
            }
            resultSkills = currentSubjectResponse.Skills;
            return new OkObjectResult(resultSkills);
        }
    }
}
