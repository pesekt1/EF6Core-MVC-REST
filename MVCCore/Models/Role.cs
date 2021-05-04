using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MVCCore.Models
{
    [Table("Role")]
    public class Role
    {
        public Role(string name)
        {
            Name = name;
        }

        public Role()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}