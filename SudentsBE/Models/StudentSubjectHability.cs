namespace StudentsBE.Models
{
    public class StudentSubjectHability
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int HabilityId { get; set; }

        public int Score { get; set; }

        public virtual Student Student { get; set; }

        public virtual Hability Hability { get; set; }
    }
}
