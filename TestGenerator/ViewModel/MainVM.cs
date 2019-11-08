using System.ComponentModel;
using System.Windows;
using TestGenerator.Command;
using TestGenerator.Interface;
using TestGenerator.Model;
using TestGenerator.View;

namespace TestGenerator.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public MainVM(IRepo repo)
        {
            _repo = repo;
            AutorizationCmd = new AuthCommand(repo, this);
        }

        private readonly IRepo _repo;
        private string _error;
        private int RoleId;
        private int UserId;
        private TestVM _loadTest;
        private GeneratorVM _loadGenTest;
        private CreateUserVM _loadCreateUser;
        private StudProgVM _loadStudProg;
        private EditUserVM _loadEditUser;
        private Visibility _currentUserCommandVisible = Visibility.Visible;
        private Visibility _openViewVisible = Visibility.Collapsed;
        private Visibility _visibleButton = Visibility.Visible;
        private Visibility _visibleTest = Visibility.Visible;
        private Model.Test _test;

        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                OnPropertyChanged("Error");
            }
        }
        public string Login
        {
            get; set;
        }
        public string Role
        {
            get; set;
        }
        public string FirstName
        {
            get; set;
        }
        public string SecondName
        {
            get; set;
        }
        public string Image
        {
            get;
            set;
        }
        public string Content
        {
            get; set;
        }
        public string ContentCurrentUser
        {
            get;set;
        }
        public TestVM LoadTest
        {
            get { return _loadTest; }
            set
            {
                _loadTest = value;
                OnPropertyChanged("LoadTest");
            }
        }
        public GeneratorVM LoadGenTest
        {
            get { return _loadGenTest; }
            set
            {
                _loadGenTest = value;
                OnPropertyChanged("LoadGenTest");
                CurrentUserCommandVisible = Visibility.Hidden;
            }
        }
        public CreateUserVM LoadCreateUser
        {
            get { return _loadCreateUser; }
            set
            {
                _loadCreateUser = value;
                OnPropertyChanged("LoadCreateUser");
                CurrentUserCommandVisible = Visibility.Hidden;
            }
        }
        public StudProgVM LoadStudProg
        {
            get { return _loadStudProg; }
            set
            {
                _loadStudProg = value;
                OnPropertyChanged("LoadStudProg");
            }
        }
        public EditUserVM LoadEditUser
        {
            get { return _loadEditUser; }
            set
            {
                _loadEditUser = value;
                OnPropertyChanged("LoadEditUser");
            }
        }
        public Visibility CurrentUserCommandVisible
        {
            get { return _currentUserCommandVisible; }
            set
            {
                _currentUserCommandVisible = value;
                OnPropertyChanged("CurrentUserCommandVisible");
            }
        }
        public Visibility OpenViewVisible
        {
            get { return _openViewVisible; }
            set
            {
                _openViewVisible = value;
                OnPropertyChanged("OpenViewVisible");
            }
        }
        public string ContentButton
        {
            get; set;
        }
        public Visibility VisibleButton
        {
            get { return _visibleButton; }
            set
            {
                _visibleButton = value;
                OnPropertyChanged("VisibleButton");
            }
        }
        public Visibility VisibleTest
        {
            get { return _visibleTest; }
            set
            {
                _visibleTest = value;
                OnPropertyChanged("VisibleTest");
            }
        }

        public AuthCommand AutorizationCmd { get; }
        public RelayCommand OpenCommandCmd { get; set; }
        public RelayCommand OpenCurrentUserCmd { get; set; }
        public RelayCommand CreateTestCmd => new RelayCommand(CreateTest);

        private void CreateTest(object obj)
        {
            var view = new TestControlV();
            var testControlVM = new TestControlVM(_repo,RoleId,view);
            view.DataContext = testControlVM;
            view.ShowDialog();
            _test = testControlVM.GetTest();
            if (_test != null)
                OpenTestView();
        }

        public void GetCurrentUser()
        {
            var user = _repo.GetUser(Login);
            UserId = user.Id;
            FirstName = user.FirstName;
            SecondName = user.SecondName;
            RoleId = user.RoleId;
            SettingProfile(user.RoleId);
        }

        private void OpenGenTestView(object obj)
        {
            LoadGenTest = new GeneratorVM(_repo);
            OpenViewCheck();
        }
        private void OpenTestView()
        {
            if (_test != null)
            {
                LoadTest = new TestVM(_repo, UserId, _test);
                OpenViewCheck();
            }
        }
        private void OpenCreateUserView(object obj)
        {
            LoadCreateUser = new CreateUserVM(_repo);
            OpenViewCheck();
        }
        private void OpenEditUserView(object obj)
        {
            LoadEditUser = new EditUserVM(_repo);
            OpenCurrntViewCheck();
        }

        private void OpenStudProgress(object obj)
        {
            LoadStudProg = new StudProgVM(_repo,RoleId);
            OpenCurrntViewCheck();
        }
        private void SettingProfile(int id)
        {
            Role = _repo.GetRole(id).RoleType;
            if (id == 1)
            {
                Image = "/Image/teacher.png";
                Content = "Добавить вопросы";
                OpenCommandCmd = new RelayCommand(OpenGenTestView);
                ContentCurrentUser = "Успеваемость";
                OpenCurrentUserCmd = new RelayCommand(OpenStudProgress);
                ContentButton = "Создать тест";
            }
            if (id == 2)
            {
                Image = "/Image/student.png";
                Content = "Начать тест";
                ContentCurrentUser = "Успеваемость";
                OpenCurrentUserCmd = new RelayCommand(OpenStudProgress);
                ContentButton = "Начать тест";
                VisibleTest = Visibility.Collapsed;
            }
            if (id == 3)
            {
                Image = "/Image/student.png";
                Content = "Создать пользователя";
                ContentCurrentUser = "Все пользователи";
                OpenCommandCmd = new RelayCommand(OpenCreateUserView);
                OpenCurrentUserCmd = new RelayCommand(OpenEditUserView);
                VisibleButton = Visibility.Collapsed;
            }
        }
        private void OpenViewCheck()
        {
            CurrentUserCommandVisible = Visibility.Collapsed;
            if (OpenViewVisible == Visibility.Visible)
                OpenViewVisible = Visibility.Collapsed;
            else
                OpenViewVisible = Visibility.Visible;
        }
        private void OpenCurrntViewCheck()
        {
            OpenViewVisible = Visibility.Collapsed;
            if (CurrentUserCommandVisible == Visibility.Visible)
                CurrentUserCommandVisible = Visibility.Collapsed;
            else
                CurrentUserCommandVisible = Visibility.Visible;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string field)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(field));
        }
    }
}