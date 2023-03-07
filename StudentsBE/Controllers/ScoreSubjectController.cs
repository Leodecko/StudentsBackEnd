using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsBE.Context;
using StudentsBE.DTO;

namespace StudentsBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScoreSubjectController
    {

        private readonly ContextDB _db;
        public ScoreSubjectController(ContextDB db)
        {
            _db = db;
        }

        public struct scoresDto
        {
            public int score { get; set; }

            public int id { get; set; }
        }

        [HttpPut]
        public ActionResult UpdateScores([FromBody]List<scoresDto> newScores)
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


    }
}
