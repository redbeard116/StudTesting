using System.ComponentModel.DataAnnotations.Schema;

namespace TestGenerator.Model
{
    [Table("student_progress", Schema = "public")]
    public class StudentProgress
    {
        [Column("progress_id")]
        public int Id { get; set; }
        [Column("student_id")]
        public int StudentId { get; set; }
        [Column("mark")]
        public int Mark { get; set; }
        [Column("test_id")]
        public int TestId { get; set; }
        [Column("questions")]
        public string Questions { get; set; }
        [Column("answers")]
        public string Answers { get; set; }
        [Column("koff")]
        public string Koeff { get; set; }
    }
}
