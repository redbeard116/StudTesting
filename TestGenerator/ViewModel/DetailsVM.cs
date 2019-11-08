using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using TestGenerator.Command;
using TestGenerator.Interface;
using TestGenerator.Model;

namespace TestGenerator.ViewModel
{
    public class DetailsVM : INotifyPropertyChanged
    {
        private string _student;

        public DetailsVM(string student, StudentProgress prog, IRepo repo)
        {
            Student = student;
            _repo = repo;
            GetAnswers(prog);
        }
        private IRepo _repo;
        public ObservableCollection<DetaisTest> Questions { get; set; } = new ObservableCollection<DetaisTest>();
        public string Student
        {
            get => _student;
            set
            {
                _student = value;
                OnPropertyChanged("Student");
            }
        }

        private void GetAnswers(StudentProgress prog)
        {
            var quests = new List<string>();
            var answs = new List<string>();
            foreach (var quest in prog.Questions.Split(','))
            {
                if (!string.IsNullOrEmpty(quest))
                    quests.Add(quest);
            }
            foreach (var answ in prog.Answers.Split(','))
            {
                if (!string.IsNullOrEmpty(answ))
                    answs.Add(answ);
            }

            for (int i = 0; i < quests.Count; i++)
            {
                var (trueA, quest, answer) = _repo.GetTrueAnswer(quests[i], answs[i]);

                Questions.Add(new DetaisTest { Quest = quest, Answer = answer, Color = trueA });
            }
        }

        public RelayCommand CanselCmd => new RelayCommand(Cansel);

        private void Cansel(object obj)
        {
            if (obj is Window view)
            {
                view.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string field)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(field));
        }
    }
}
