using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGenerator.DB;
using TestGenerator.Model;

namespace TestGenerator.Interface
{
    public class Repo : IRepo
    {
        private DBContex db = new DBContex();
        public bool Autorization(string login, string password)
        {
            return db.Accounts.Any(x => x.Login == login && x.Password == password);
        }

        public Account GetUser(string login)
        {
            return db.Accounts.FirstOrDefault(x => x.Login == login);
        }

        public Role GetRole(int id)
        {
            return db.Roles.FirstOrDefault(x => x.RoleId == id);
        }

        public List<Role> GetRole()
        {
            var roles = db.Roles;
            List<Role> listRoles = new List<Role>();
            foreach (var role in roles)
                listRoles.Add(role);
            return listRoles;
        }

        public void GenerateTest(string question, string answer)
        {
            Question test = new Question
            {
                Quest = question,
                Answer = answer
            };
            db.Questions.Add(test);
            db.SaveChanges();
        }

        public List<Question> GetTest(int questionCount)
        {
            var alltest = db.Questions;
            List<Question> tests = new List<Question>();
            Random rand = new Random();
            for (int i = 0; i < questionCount; i++)
            {
                var number = rand.Next(1, alltest.Count());
                var quest = db.Questions.FirstOrDefault(x => x.QuestionId == number);
                tests.Add(new Question {  QuestionId = quest.QuestionId,Quest = quest.Quest });
            }
            return tests;
        }

        public int CheckAnswer(List<Question> answers)
        {
            int number = 0;
            foreach (var answer in answers)
            {
                if (db.Questions.Any(x => x.Quest == answer.Quest && x.Answer == answer.Answer))
                    number += 10;
            }
            return number;
        }

        public void CreateUserCommand(Account account)
        {
            db.Accounts.Add(account);
            db.SaveChanges();
        }

        public void AddMark(StudentProgress progress)
        {
            var stud = db.StudProgress.FirstOrDefault(x => x.StudentId == progress.StudentId && x.TestId == progress.TestId);
            if (stud != null)
            {
                stud.Mark = progress.Mark;
                stud.Questions = progress.Questions;
                stud.Answers = progress.Answers;
            }
            else
            {
                db.StudProgress.Add(progress);
            }
            db.SaveChanges();
        }

        public List<Progress> GetStudProg()
        {
            var students = db.StudProgress.Join(
                db.Accounts,
                p => p.StudentId,
                c => c.Id,
                (p, c) => new
                {
                    Id = p.StudentId,
                    FirstName = c.FirstName,
                    SecondName = c.SecondName,
                    Mark = p.Mark,
                    TestId = p.TestId
                });
            List<Progress> studentProgress = new List<Progress>();
            int plase = 1;
            foreach (var student in students)
            {
                studentProgress.Add(new Progress
                {
                    Plase = plase,
                    StudId = student.Id,
                    Student = $"{student.FirstName} {student.SecondName}",
                    Mark = student.Mark,
                    TestId = student.TestId
                });
                plase++;
            }
            return studentProgress.OrderByDescending(w=>w.TestId).OrderByDescending(w=>w.Plase).ToList();
        }

        public List<EditUser> GetAllUser()
        {
            var users = db.Accounts.Join(
                db.Roles,
                p => p.Id,
                c => c.RoleId,
                (p, c) => new
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    SecondName = p.SecondName,
                    Login = p.Login,
                    Password = p.Password,
                    RoleId = p.RoleId,
                    RoleType = c.RoleType
                });
            List<EditUser> editUsers = new List<EditUser>();
            foreach (var user in users)
            {
                editUsers.Add(new EditUser
                {
                    User = new Account
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        SecondName = user.SecondName,
                        Login = user.Login,
                        Password = user.Password,
                        RoleId = user.RoleId
                    },
                    FullName = $"{user.FirstName} {user.SecondName}",
                    Role = new Role
                    {
                        RoleId = user.RoleId,
                        RoleType = user.RoleType
                    }
                });
            }
            return editUsers;
        }

        public void EditUser(EditUser user)
        {
            var currentUser = db.Accounts.Find(user.User.Id);
            if (currentUser != null)
            {
                currentUser.FirstName = user.User.FirstName;
                currentUser.SecondName = user.User.SecondName;
                currentUser.Login = user.User.Login;
                currentUser.Password = user.User.Password;
                currentUser.RoleId = user.Role.RoleId;
                db.SaveChanges();
            }
        }

        public void CreateTest(Test test)
        {
            db.Tests.Add(test);
            db.SaveChanges();
        }

        public Test LoginTest(string login, string password)
        {
            return db.Tests.FirstOrDefault(x => x.Login == login && x.Password == password);
        }

        public int CountQuestion()
        {
            return db.Questions.Count();
        }

        public (bool,string,string) GetTrueAnswer(string questId,string answer)
        {
            var id = Int32.Parse(questId);
            var quest = db.Questions.Find(id);
            if (quest.Answer == answer)
            {
                return (true,quest.Quest,answer);
            }
            else
            {
                return (false, quest.Quest,answer);
            }
        }

        public StudentProgress GetStudProg(int studetId, int testId)
        {
            return db.StudProgress.FirstOrDefault(w=>w.StudentId == studetId && w.TestId == testId);
        }

        public bool GetTrueAnswer(Question quest)
        {
            var que = db.Questions.FirstOrDefault(w=>w.QuestionId == quest.QuestionId);
            if (que.Answer == quest.Answer)
                return true;
            else
                return false;
        }

        public Test GetTests(int testId)
        {
            return db.Tests.FirstOrDefault(x=>x.TestId == testId);
        }
    }
}
