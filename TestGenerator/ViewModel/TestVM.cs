using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using TestGenerator.Command;
using TestGenerator.Interface;
using TestGenerator.Model;

namespace TestGenerator.ViewModel
{
    public class TestVM : INotifyPropertyChanged
    {
        public TestVM(IRepo repo, int id, Test test)
        {
            UserId = id;
            _repo = repo;
            _test = test;
            GetTest();
        }

        private readonly IRepo _repo;
        private int UserId;
        private Test _test;

        public ObservableCollection<Question> Questions { get; set; } = new ObservableCollection<Model.Question>();

        public RelayCommand CheckAnswerCmd => new RelayCommand(CheckAnswer);

        private void GetTest()
        {
            var tests = _repo.GetTest(_test.QuestionCount);
            foreach (var test in tests)
            {
                Questions.Add(test);
            }

        }
        private void CheckAnswer(object obj)
        {
            List<Question> listAnswers = new List<Question>();
            string questions = string.Empty;
            string answers = string.Empty;
            string koeff = string.Empty;
            foreach (var quest in Questions)
            {
                listAnswers.Add(quest);
                questions += quest.QuestionId + ",";
                answers += quest.Answer + ",";
                var ans = _repo.GetTrueAnswer(quest);
                koeff += ans + ",";
            }
            var mark = _repo.CheckAnswer(listAnswers);
            MessageBox.Show($"Вы заработали {mark} баллов", "Оценка", MessageBoxButton.OK, MessageBoxImage.Information);

            var studProgress = new StudentProgress { StudentId = UserId, Mark = mark, TestId = _test.TestId, Questions = questions,Answers = answers,Koeff = koeff };

            _repo.AddMark(studProgress);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string field)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(field));
        }
    }
}
