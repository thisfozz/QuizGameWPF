using AesEncryptionNamespace;
using System;
using System.Threading;

namespace QuizGame.RegistrationAndAuthorization
{
    public class Registration
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
    }
}
