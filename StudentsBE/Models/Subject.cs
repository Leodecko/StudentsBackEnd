namespace StudentsBE.Models
{
  public class Subject
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual List<Hability> Habilities { get; set; }
  }
}
