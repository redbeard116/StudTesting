using TestGenerator.Model;

namespace TestGenerator.Model
{
    public class EditUser
    {
        public Account User { get; set; }
        public string FullName { get; set; }
        public Role Role { get; set; }
    }
}
