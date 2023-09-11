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
            var registrationPageViewModel = new RegistrationViewModel();

            RegistrationPage registrationPage = new RegistrationPage();
            registrationPage.DataContext = registrationPageViewModel;

            Application.Current.MainWindow.Content = registrationPage;
        }

        private void SwtitchToAuthorizationPage(object parameter)
        {
            AuthorizationPageViewModel authorizationPageViewModel = new AuthorizationPageViewModel();

            //var authorizationPage = new AuthorizationPage();
            //authorizationPage.DataContext = authorizationPageViewModel;

            //Application.Current.MainWindow.Content = authorizationPage;
        }
    }
}
