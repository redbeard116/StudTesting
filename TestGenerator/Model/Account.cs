using System.ComponentModel.DataAnnotations.Schema;

namespace TestGenerator.Model
{
    [Table("account", Schema = "public")]
    public class Account
    {
        [Column("account_id")]
        public int Id { get; set; }
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("second_name")]
        public string SecondName { get; set; }
        [Column("login")]
        public string Login { get; set; }
        [Column("password")]
        public string Password { get; set; }
    }
}
