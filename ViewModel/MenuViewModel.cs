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
        AuthenticationUser authenticationUser;

        private string _loginUser;

        public string LoginUser
        {
            get { return _loginUser; }
            set 
            { 
                _loginUser = authenticationUser.GetCurrectUser().Login;
                // Я хочу получать информацию о Login из AuthenticationUser из данного метода или GetLoggedInUserLogin в том же классе,
                // где есть соответсвующий метод, но получаю назад null,
                // хотя в момент когда срабатывает Login из AuthorizationViewModel - поле currentUser заполняется и имеет соответсвующее поле
                // Но уже в MenuViewModel currentUser несуществует
            }
        }
        public ICommand ExitAccountButton { get; }
        public ICommand CreatePackButton { get; }

        public MenuViewModel(string loginUser) //fix избавиться от передачи аргумента
        {
            ExitAccountButton = new DelegateCommand(ExitAccount, (_) => true);
            CreatePackButton = new DelegateCommand(CreatePack, (_) => true);
            _loginUser = loginUser; //fix избавиться от этого
        }

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


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
