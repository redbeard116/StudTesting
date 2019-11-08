using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TestGenerator.Command;
using TestGenerator.Interface;
using TestGenerator.Model;

namespace TestGenerator.ViewModel
{
    public class CreateUserVM : INotifyPropertyChanged
    {

        public CreateUserVM(IRepo repo)
        {
            _repo = repo;
            GetRole();
        }

        private string _firstName;
        private string _secondName;
        private string _login;
        private string _password;
        private Role _selectRole;
        private readonly IRepo _repo;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string SecondName
        {
            get { return _secondName; }
            set
            {
                _secondName = value;
                OnPropertyChanged("SecondName");
            }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        public Role SelectRole
        {
            get { return _selectRole; }
            set
            {
                _selectRole = value;
                OnPropertyChanged("SelectRole");
            }
        }

        public RelayCommand CreateUserCmd => new RelayCommand(CreateUserCommand);
        public ObservableCollection<Role> Roles { get; set; } = new ObservableCollection<Role>();

        private void CreateUserCommand(object obj)
        {
            List<Account> account = new List<Account>();
            account.Add(new Account
            {
                FirstName = _firstName,
                SecondName = _secondName,
                Login = _login,
                Password = _password,
                RoleId = _selectRole.RoleId
            });
            _repo.CreateUserCommand(account[0]);
        }
        private void GetRole()
        {
            var roles = _repo.GetRole();
            foreach (var role in roles)
                Roles.Add(role);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string field)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(field));
        }
    }
}
