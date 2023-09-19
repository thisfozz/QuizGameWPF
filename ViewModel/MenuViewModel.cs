using AuthenticationManagerNamespace;
using Quiz.Command;
using QuizGame.Model;
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
                // Но уже в этом классе MenuViewModel currentUser несуществует
            }
        }
        public ICommand ExitAccountButton { get; }

        public MenuViewModel(string loginUser) //fix избавиться от передачи аргумента
        {
            ExitAccountButton = new DelegateCommand(ExitAccount, (_) => true);
            _loginUser = loginUser; //fix избавиться от этого
        }

        private void ExitAccount(object parameter)
        {
            authenticationUser.LogoutUser(); //fix также не работает т.к недоступен currentUser

            var mainWindowViewModel = new MainViewModel();

            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = mainWindowViewModel;

            Application.Current.MainWindow.Content = mainWindow;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
