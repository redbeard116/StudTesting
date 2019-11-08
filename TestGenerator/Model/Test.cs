using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGenerator.Model
{
    [Table("test", Schema = "public")]
    public class Test
    {
        [Column("id")]
        public int TestId { get; set; }
        [Column("test_name")]
        public string TestName { get; set; }
        [Column("login")]
        public string Login { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("question_count")]
        public int QuestionCount { get; set; }
        [Column("koff")]
        public string Koeff { get; set; }
    }
}
