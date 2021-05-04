using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MVCCore.Models
{
    [Table("user_roles")]
    public class UserRoles
    {
        public UserRoles()
        {
        }

        public UserRoles(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }
        
        [JsonIgnore]
        public virtual User User { get; set; }

    }
}