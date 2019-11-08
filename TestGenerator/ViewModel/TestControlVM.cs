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
    public class TestControlVM : INotifyPropertyChanged
    {

        public TestControlVM(IRepo repo, int roleId,View.TestControlV view)
        {
            _repo = repo;
            _view = view;
            if (roleId == 1)
            {
                StackVisible = Visibility.Visible;
                Content = "Создать";
                OpenCommandCmd = new RelayCommand(CreateTest);
            }
            if (roleId == 2)
            {
                StackVisible = Visibility.Collapsed;
                Content = "Начать тест";
                OpenCommandCmd = new RelayCommand(LoginTest);
            }
            int count = _repo.CountQuestion();
            for (int i = 1; i <= count; i++)
                CountQuestion.Add(i);
        }

        private IRepo _repo;
        private int _selectCount;
        private Visibility _stackVisible = Visibility.Visible;
        private Test Test;
        private View.TestControlV _view;

        public string TestName { get; set; }
        public ObservableCollection<int> CountQuestion { get; set; } = new ObservableCollection<int>();
        public string Login { get; set; }
        public string Password { get; set; }
        public int SelectCount
        {
            get { return _selectCount; }
            set
            {
                _selectCount = value;
                OnPropertyChanged("SelectCount");
            }
        }
        public Visibility StackVisible
        {
            get { return _stackVisible; }
            set
            {
                _stackVisible = value;
                OnPropertyChanged("StackVisible");
            }
        }
        public string Content { get; set; }

        public RelayCommand OpenCommandCmd { get; set; }

        private void CreateTest(object obj)
        {
            Test test = new Test
            {
                TestName = $"{TestName}{DateTime.UtcNow}",
                Login = Login,
                Password = Password,
                QuestionCount = SelectCount
            };
            _repo.CreateTest(test);
            _view.Close();
        }
        private void LoginTest(object obj)
        {
            Test =  _repo.LoginTest(Login,Password);
            _view.Close();
        }

        public Test GetTest()
        {
            return Test;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string field)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(field));
        }
    }
}
