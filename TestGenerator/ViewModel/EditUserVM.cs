using System.ComponentModel;
using TestGenerator.Model;
using TestGenerator.Interface;
using System.Collections.ObjectModel;
using System.Windows;
using TestGenerator.Command;
using System;

namespace TestGenerator.ViewModel
{
    public class EditUserVM : INotifyPropertyChanged
    {
        public EditUserVM(IRepo repo)
        {
            _repo = repo;
            GetAllUser();
            GetRole();
        }
        private IRepo _repo;
        private EditUser _selectUser;
        private Visibility _currentUser = Visibility.Collapsed;

        public Visibility CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged("CurrentUser");
            }
        }
        public EditUser SelectUser
        {
            get { return _selectUser; }
            set
            {
                _selectUser = value;
                OnPropertyChanged("SelectUser");
                CurrentUser = Visibility.Visible;
            }
        }
        public RelayCommand EditUserCmd => new RelayCommand(EditUserCommand);
        public ObservableCollection<EditUser> EditUsers { get; set; } = new ObservableCollection<EditUser>();
        public ObservableCollection<Role> Roles { get; set; } = new ObservableCollection<Role>();

        private void GetAllUser()
        {
            var users = _repo.GetAllUser();
            foreach (var user in users)
                EditUsers.Add(user);
        }
        private void GetRole()
        {
            var roles = _repo.GetRole();
            foreach (var role in roles)
                Roles.Add(role);
        }
        private void EditUserCommand(object obj)
        {
            _repo.EditUser(SelectUser);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string field)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(field));
        }
    }
}
