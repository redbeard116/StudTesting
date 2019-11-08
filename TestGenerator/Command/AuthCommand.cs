using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestGenerator.Interface;
using TestGenerator.View;
using TestGenerator.ViewModel;

namespace TestGenerator.Command
{
    public class AuthCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        private readonly IRepo _repo;
        private readonly MainVM _viewModel;
        public AuthCommand(IRepo repo, MainVM viewModel)
        {
            _repo = repo;
            _viewModel = viewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        
        public void Execute(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;

            var result = _repo.Autorization(_viewModel.Login, password);
            if (result != false)
            {
                _viewModel.GetCurrentUser();
                Account view = new Account();
                view.DataContext = _viewModel;
                view.Show();
                if (view.IsActive == true)
                {
                    App.reg.Close();
                }
            }
            else { _viewModel.Error = "Неправильный логин или пароль!"; }
        }
    }
}
