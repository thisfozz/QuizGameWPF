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
        UserModel userModel;

        private string _loginUser;

        public string LoginUser
        {
            get { return _loginUser; } //fix
        }
        public ICommand ExitAccountButton { get; }

        public MenuViewModel(string loginUser) //fix
        {
            ExitAccountButton = new DelegateCommand(ExitAccount, (_) => true);
            _loginUser = loginUser; //fix
        }

        private void ExitAccount(object parameter)
        {
            //authenticationUser.LogoutUser(); //fix

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
