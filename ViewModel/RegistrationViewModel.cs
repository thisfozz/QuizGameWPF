using AesEncryptionNamespace;
using AuthenticationManagerNamespace;
using Quiz.Command;
using QuizGame.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace QuizGame.ViewModel
{
    internal class RegistrationViewModel : INotifyPropertyChanged
    {
        private AesEncryption aesEncryption;
        private AuthenticationUser authenticationManager;

        private UserModel userModel;

        public ICommand RegisterButton { get; }

        public bool isRegistrationSuccessful { get; set; }
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

        public string registrationErrorMessage { get; set; }
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
        }

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
            isRegistrationSuccessful = authenticationManager.RegistrationVerification(UserModel.Login, UserModel.Password, UserModel.DateOfBirth);

            if (!isRegistrationSuccessful)
            {
                string encryptedPassword = aesEncryption.Encrypt(UserModel.Password);
                isRegistrationSuccessful = authenticationManager.RegisterUser(UserModel.Login, encryptedPassword, UserModel.DateOfBirth);

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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
