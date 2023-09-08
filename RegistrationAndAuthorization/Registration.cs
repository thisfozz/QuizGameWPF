using AesEncryptionNamespace;
using System;
using System.Threading;

namespace QuizGame.RegistrationAndAuthorization
{
    public class Registration : IDisposable
    {
        private AuthenticationManager authenticationManager;
        private AesEncryptionNamespace.AesEncryption aesEncryption;

        public event EventHandler RegistrationSuccess;

        public Registration(AuthenticationManager authManager, AesEncryptionNamespace.AesEncryption aesEncryptor)
        {
            authenticationManager = authManager;
            aesEncryption = aesEncryptor;
        }

        public bool RegisterUser(string login, string password, DateTime dateOfBirth)
        {
            int cursorPositionInput, cursorNotifyAndInput;
            string text = string.Empty;

            bool loginisCorrect = DataCorrectness.isCheckLogin(login);
            bool passwordisCorrect = DataCorrectness.isCheckLogin(password);
            bool dateOfBirthCorrect = DataCorrectness.IsCheckDate(dateOfBirth);

            if (loginisCorrect && passwordisCorrect && dateOfBirthCorrect)
            {
                string passwordEncrypt = aesEncryption.Encrypt(password);

                bool isRegistration = authenticationManager.RegisterUser(login, passwordEncrypt, dateOfBirth);

                return isRegistration;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
