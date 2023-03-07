namespace StudentsBE.DTO
{
  public class GetSubjectOutput
  {
    public string Name { get; set; }
    public int Id { get; set; }
    public List<SkillOutput> Skills { get; set; }
  }

  public class SkillOutput
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
  }
}
