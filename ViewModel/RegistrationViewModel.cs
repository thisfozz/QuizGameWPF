using AesEncryptionNamespace;
using AuthenticationManagerNamespace;
using DataCorrectnessNamespace;
using Quiz.Command;
using QuizGame.Model;
using QuizGame.RegistrationAndAuthorization;
using RegistrationNamespace;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace QuizGame.ViewModel
{
    internal class RegistrationViewModel : INotifyPropertyChanged
    {
        private UserModel userModel;
        private AesEncryption aesEncryption;
        private DataCorrectnessNamespace.DataCorrectness dataCorrectness;
        private AuthenticationManagerNamespace.AuthenticationManager authenticationManager;
        private QuizGame.RegistrationAndAuthorization.Registration registration;

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

        public RegistrationViewModel()
        {
            dataCorrectness = new DataCorrectnessNamespace.DataCorrectness();
            authenticationManager = new AuthenticationManagerNamespace.AuthenticationManager();
            aesEncryption = new AesEncryption();
            userModel = new UserModel();

            RegisterButton = new DelegateCommand(Register, CanRegister);
            PropertyChanged += (_, _) => { ((DelegateCommand)RegisterButton).RaiseCanExecuteChanged(); };
        }

        public UserModel UserModel
        {
            get { return userModel; }
            set
            {
                userModel = value;
                OnPropertyChanged();
            }
        }

        private void Register(object parametr)
        {
            bool isRegistrationSuccessful = registration.RegisterUser(userModel.Login, userModel.Password, userModel.DateOfBirth);

            if (isRegistrationSuccessful)
            {
                string encryptedPassword = aesEncryption.Encrypt(userModel.Password);
                isRegistrationSuccessful = authenticationManager.RegisterUser(userModel.Login, encryptedPassword, userModel.DateOfBirth);

                if (isRegistrationSuccessful)
                {
                    IsRegistrationSuccessful = true;
                    RegistrationErrorMessage = null;
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

        private bool CanRegister(object parametr)
        {
            return !string.IsNullOrWhiteSpace(UserModel.Login)
                && !string.IsNullOrWhiteSpace(UserModel.Password)
                && UserModel.DateOfBirth != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
