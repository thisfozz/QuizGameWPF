using AesEncryptionNamespace;
using FileManagerNamespace;
using System;
using System.Collections.Generic;
using UserDataNamespace;

namespace QuizGame.RegistrationAndAuthorization
{
    public class AuthenticationManager
    {
        private List<UserData> registeredUsers; // ?? static ??
        private UserData currentUser;

        public AuthenticationManager()
        {
            registeredUsers = LoadData.LoadUserData();
            currentUser = null;
        }
        public bool LoginUser(string login, string password)
        {
            registeredUsers = LoadData.LoadUserData();
            if (registeredUsers != null)
            {
                AesEncryption aesEncryption = new AesEncryption();
                UserData user = registeredUsers.Find(u => u.Login == login);

                if (user == null) return false;
                string decryptedPassword = aesEncryption.Decrypt(user.Password);
                if (password == decryptedPassword)
                {
                    currentUser = user;
                    return true;
                }
            }
            return false;
        }
        public bool RegisterUser(string loginUser, string passwordUser, DateTime birthdate)
        {
            if (registeredUsers.Exists(u => u.Login == loginUser)) return false;

            UserData newUser = new UserData(loginUser, passwordUser, birthdate);
            registeredUsers.Add(newUser);
            SaveRegisteredUsers();
            currentUser = newUser;
            return true;
        }
        public void UpdatePassword(string login, string password)
        {
            if (currentUser != null && currentUser.Login == login)
            {
                //currentUser.Password = password;
            }
        }
        private void SaveRegisteredUsers()
        {
            string jsonUserData = LoadData.ToJsonUserData(registeredUsers);
            LoadData.SerializeUserData(jsonUserData);
        }
        public void LogoutUser()
        {
            currentUser = null;
        }
        public string GetLoggedInUserLogin()
        {
            return currentUser?.Login;
        }
        public UserData GetCurrectUser()
        {
            return currentUser;
        }
        public List<UserData> GetAllRegisteredUser()
        {
            return registeredUsers;
        }
    }
}
