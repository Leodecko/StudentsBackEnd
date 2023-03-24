using System.ComponentModel.DataAnnotations;

namespace StudentsBE.DTO
{
    public class UserCredentials
    {
        [EmailAddress]
        public string Email { get; set;}

        public string Password { get; set;}
    }
}
