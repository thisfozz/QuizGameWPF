using AesEncryptionNamespace;
using AuthenticationManagerNamespace;
using DataCorrectnessNamespace;
using FileManagerNamespace;
using Quiz.Command;
using QuizGame.Model;
using QuizGame.View;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using UserDataNamespace;

namespace QuizGame.ViewModel
{
    internal class RegistrationViewModel : INotifyPropertyChanged
    {
        private AesEncryption aesEncryption;
        private AuthenticationUser authenticationManager;

        public RegistrationViewModel()
        {
            authenticationManager = new AuthenticationUser();
            aesEncryption = new AesEncryption();
            UserModel = new UserModel();

            RegisterButton = new DelegateCommand(Register,
            (_) =>
            {
                return
                !string.IsNullOrEmpty(UserModel?.Login)
                && !string.IsNullOrEmpty(UserModel?.Password)
                && UserModel?.DateOfBirth != null;
            });
            PropertyChanged += (_, _) => { ((DelegateCommand)RegisterButton).RaiseCanExecuteChanged(); };

            AutorizationButton = new DelegateCommand(Autorization, (_) => true);
        }

        public ICommand RegisterButton { get; }

        public bool isRegistrationSuccessful { get; private set; }
        public bool IsRegistrationSuccessful
        {
            get { return isRegistrationSuccessful; }
            set
            {
                if (value != isRegistrationSuccessful)
                {
                    isRegistrationSuccessful = value;
                    OnPropertyChanged();
                }
            }
        }

        public string registrationErrorMessage { get; private set; }
        public string RegistrationErrorMessage
        {
            get { return registrationErrorMessage; }
            set
            {
                if (value != registrationErrorMessage)
                {
                    registrationErrorMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        private UserModel userModel;
        public UserModel UserModel
        {
            get { return userModel; }
            set
            {
                if (userModel == value) return;
                userModel = value;
                userModel.PropertyChanged += (_, _) =>
                {
                    ((DelegateCommand)RegisterButton).RaiseCanExecuteChanged();
                };
                OnPropertyChanged();
            }
        }

        private void Register(object parametr)
        {
            DataCorrectness dataCorrectness = new DataCorrectness();
            DateTime dateCorrect = DateTime.Now;

            if (dataCorrectness.ConvertToDate(UserModel.DateOfBirth, out dateCorrect)){
                isRegistrationSuccessful = authenticationManager.RegistrationVerification(UserModel.Login, UserModel.Password, dateCorrect);
            }

            if (!isRegistrationSuccessful)
            {
                string encryptedPassword = aesEncryption.Encrypt(UserModel.Password);
                isRegistrationSuccessful = authenticationManager.RegisterUser(UserModel.Login, encryptedPassword, dateCorrect);

                if (isRegistrationSuccessful)
                {
                    IsRegistrationSuccessful = true;
                    RegistrationErrorMessage = null;

                    MainViewModel mainViewModel = new MainViewModel();

                    MainMenuPage mainMenuPage = new MainMenuPage();
                    mainMenuPage.DataContext = mainViewModel;

                    Application.Current.MainWindow.Content = mainMenuPage;
                }
                else
                {
                    IsRegistrationSuccessful = false;
                    RegistrationErrorMessage = "Ошибка при регистрации. Попробуйте другой логин.";
                }
            }
            else
            {
                IsRegistrationSuccessful = false;
                RegistrationErrorMessage = "Ошибка при регистрации. Проверьте правильность введенных данных.";
            }
        }

        public ICommand AutorizationButton { get; }

        private void Autorization(object parametr)
        {
            AuthorizationViewModel authorizationPageViewModel = new AuthorizationViewModel();

            AuthorizationPage authorizationPage = new AuthorizationPage();
            authorizationPage.DataContext = authorizationPageViewModel;

            Application.Current.MainWindow.Content = authorizationPage;
        }

        private void UpdatePassword(object parametr)
        {
            /*         
            UserData currentUser = authenticationManager.GetCurrectUser();
            if (currentUser != null)
            {
                Ввод пароля
                 DataCorrectness.isCheckPassword(newPassword)
                UserModel.Password = новый пароль
                LoadData.SaveUserDataForUser(currentUser);
                Возврат к окну настроек
            }
            */
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
