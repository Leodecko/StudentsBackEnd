using System.ComponentModel.DataAnnotations;

namespace StudentsBE.DTO
{
    public class StudentDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
  }
}
