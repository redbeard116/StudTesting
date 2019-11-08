using System.Collections.Generic;
using TestGenerator.Model;

namespace TestGenerator.Interface
{
    public interface IRepo
    {
        bool Autorization(string login, string password);
        Account GetUser(string login);
        Role GetRole(int id);
        List<Role> GetRole();
        void GenerateTest(string question, string answer);
        List<Question> GetTest(int questionCount);
        int CheckAnswer(List<Question> answers);
        void CreateUserCommand(Account account);
        void AddMark(StudentProgress progress);
        List<Progress> GetStudProg();
        List<EditUser> GetAllUser();
        void EditUser(EditUser user);
        void CreateTest(Test test);
        Test LoginTest(string login,string password);
        int CountQuestion();
        (bool, string,string) GetTrueAnswer(string questId, string answer);
        StudentProgress GetStudProg(int studetId,int testIs);
        bool GetTrueAnswer(Question quest);
        Test GetTests(int testId);
    }
}
