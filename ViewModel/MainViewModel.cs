using Quiz.Command;
using QuizGame.View;
using System.Windows;
using System.Windows.Input;

namespace QuizGame.ViewModel
{
    public class MainViewModel
    {
        public ICommand RegistrationButton { get; private set; }
        public ICommand AuthorizationButton { get; }
        public ICommand ExitButton { get; }

        public MainViewModel()
        {
            AuthorizationButton = new DelegateCommand(SwtitchToAuthorizationPage, (_) => true);
            RegistrationButton = new DelegateCommand(SwitchToRegistrationPage, (_) => true);
            ExitButton = new DelegateCommand(ExitApplication, (_) => true);
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

            var authorizationViewModel = new AuthorizationViewModel();

            AuthorizationPage authorizationPage = new AuthorizationPage();
            authorizationPage.DataContext = authorizationViewModel;

            Application.Current.MainWindow.Content = authorizationPage;
        }

        public void ExitApplication(object parametr)
        {
            Application.Current.Shutdown();
        }
    }
}
