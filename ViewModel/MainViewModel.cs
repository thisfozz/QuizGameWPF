using Quiz.Command;
using QuizGame.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuizGame.ViewModel
{
    public class MainViewModel
    {
        public ICommand RegistrationButton { get; }
        public ICommand AuthorizationButton { get; }

        public MainViewModel()
        {
            RegistrationButton = new DelegateCommand(SwitchToRegistrationPage, _ => true);
            AuthorizationButton = new DelegateCommand(SwtitchToAuthorizationPage, _ => true);
        }

        private void SwitchToRegistrationPage(object parameter)
        {
            RegistrationPage registrationPageViewModel = new RegistrationPage();

            RegistrationPage registrationPage = new RegistrationPage();
            registrationPage.DataContext = registrationPageViewModel;

            Application.Current.MainWindow.Content = registrationPage;
        }

        private void SwtitchToAuthorizationPage(object parameter)
        {
            AuthorizationPageViewModel authorizationPageViewModel = new AuthorizationPageViewModel();

            AuthorizationPageViewModel authorizationPage = new AuthorizationPageViewModel();
            authorizationPage.DataContext = authorizationPageViewModel;

            Application.Current.MainWindow.Content = authorizationPage;
        }
    }
}
