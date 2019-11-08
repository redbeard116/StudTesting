using System.ComponentModel.DataAnnotations.Schema;

namespace TestGenerator.Model
{
    [Table("role", Schema = "public")]
    public class Role
    {
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("role")]
        public string RoleType { get; set; }
    }
}
