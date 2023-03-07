using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsBE.Models;

namespace StudentsBE.Migrations
{
  public class HabilitiesConfiguration : IEntityTypeConfiguration<Hability>
  {
    public void Configure(EntityTypeBuilder<Hability> builder)
    {
      builder.HasData(
       new Hability { Id = 1, Name = "Out loud reading", SubjectId = 1},
       new Hability { Id = 2, Name = "Reading comprehension", SubjectId = 1 },
       new Hability { Id = 3, Name = "Readability", SubjectId = 1 },
       new Hability { Id = 4, Name = "Coherence", SubjectId = 1 },
       new Hability { Id = 5, Name = "Sum", SubjectId = 2 },
       new Hability { Id = 6, Name = "Subtraction", SubjectId = 2 },
       new Hability { Id = 7, Name = "Multiplication", SubjectId = 2 },
       new Hability { Id = 8, Name = "Division", SubjectId = 2 }
       );
    }
  }
}
