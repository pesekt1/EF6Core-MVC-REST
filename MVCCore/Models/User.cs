using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCore.Models
{
    [Table("User")]
    public class User
    {
        // public User()
        // {
        //     UserRoles = new HashSet<UserRoles>();
        // }

        public User(string email, string password, string userName)
        {
            Email = email;
            Password = password;
            UserName = userName;
        }

        public User()
        {
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}