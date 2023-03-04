using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsBE.Models;

namespace StudentsBE.Migrations
{
  public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
  {
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
       builder.HasData(
        new Subject { Id = 1, Name = "Spanish"},
        new Subject { Id = 2, Name = "Math"}
        );
    }
  }
}
