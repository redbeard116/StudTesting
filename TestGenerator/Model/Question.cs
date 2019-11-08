using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestGenerator.Model
{
    [Table("question", Schema = "public")]
    public class Question
    {
        [Column("test_id")]
        public int QuestionId { get; set; }
        [Column("question")]
        public string Quest { get; set; }
        [Column("answer")]
        public string Answer { get; set; }
    }
}
