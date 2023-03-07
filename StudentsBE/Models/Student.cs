namespace StudentsBE.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email  { get; set; }

        public virtual ICollection<StudentSubjectHability> StudentSubjectHabilities { get; set; }
    }
}
