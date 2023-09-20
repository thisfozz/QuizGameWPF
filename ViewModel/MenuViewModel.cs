using AuthenticationManagerNamespace;
using Quiz.Command;
using QuizGame.Model;
using QuizGame.View;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace QuizGame.ViewModel
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        AuthenticationUser _authenticationUser;

        public MenuViewModel(AuthenticationUser authenticationUser) //fix избавиться от передачи аргумента
        {
            ExitAccountButton = new DelegateCommand(ExitAccount, (_) => true);
            CreatePackButton = new DelegateCommand(CreatePack, (_) => true);

            _authenticationUser = authenticationUser;
            UpdateLoginUser();
        }

        private string _loginUser;
        public string LoginUser
        {
            get { return _loginUser; }
            private set
            {
                if (_loginUser != value)
                {
                    _loginUser = value;
                    OnPropertyChanged(nameof(LoginUser));
                }
            }
        }

        private void UpdateLoginUser()
        {
            LoginUser = _authenticationUser.GetCurrectUser().Login;
        }

        public ICommand ExitAccountButton { get; }
        public ICommand CreatePackButton { get; }

        private void CreatePack(object parametr)
        {
            CreatePackViewModel createPackViewModel = new CreatePackViewModel();

            CreatePackView createPackView = new CreatePackView();
            createPackView.DataContext = createPackViewModel;

            Application.Current.MainWindow.Content = createPackView;
        }

        private void ExitAccount(object parametr)
        {
            /*
            var mainViewModel = new MainViewModel();

            MainWindow mainwindow = new MainWindow();
            mainwindow.DataContext = mainViewModel;

            Application.Current.MainWindow.Content = mainwindow;
            */
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
