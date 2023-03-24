using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsBE.Context;
using StudentsBE.DTO;
using StudentsBE.DTO.Subject;
using StudentsBE.Models;

namespace StudentsBE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="admin")]
    public class SubjectController : Controller
    {

        private readonly ContextDB _db;

        private readonly IMapper _mapper;

        public SubjectController(ContextDB db, IMapper mapper)
        {
            _db = db;

            _mapper = mapper;
        }

        public struct scoresDto
        {
            public int score { get; set; }

            public int id { get; set; }
        }

        [HttpPut]
        public ActionResult UpdateScores([FromBody] List<scoresDto> newScores)
        {

            var idsSearch = newScores.Select(x => x.id).ToList();

            var subjectHabilities = _db.StudentSubjectHabilities.Where(x => idsSearch.Contains(x.Id)).ToList();

            subjectHabilities.ForEach(score =>
            {
                var currentScore = newScores.FirstOrDefault(x => x.id == score.Id).score;

                score.Score = currentScore;
            });

            _db.SaveChanges();

            return new OkResult();
        }

        [HttpGet("{studentId}")]
        public ActionResult GetSubjects(int studentId)
        {
            var habilitiesScore = _db.StudentSubjectHabilities.Where(x => x.StudentId == studentId);

            List<GetSubjectOutput> result = new List<GetSubjectOutput>();

            var subjects = _db.Subjects.Include(x => x.Habilities).ToList();

            foreach (var subject in subjects)
            {
                var currentSubjectResponse = new GetSubjectOutput();

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

            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubjectCreateDto subject)
        {
            try
            {
                var newSubject = _mapper.Map<Subject>(subject);

                await _db.AddAsync(newSubject);

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
