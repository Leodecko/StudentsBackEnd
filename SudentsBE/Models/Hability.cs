namespace StudentsBE.Models
{
  public class Hability
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual Subject Subject { get; set; }

    public int SubjectId { get; set; }

    public virtual ICollection<StudentSubjectHability> StudentSubjectHabilities { get; set; }
  }
}
