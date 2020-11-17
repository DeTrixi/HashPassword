using System.ComponentModel.DataAnnotations.Schema;

namespace HashPassword.Models
{
    [Table("Users")]
    public class Users
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public  string FirstName{ get; set; }
        public string LastName { get; set; }


    }
}