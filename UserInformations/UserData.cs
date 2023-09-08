using System;
using System.Collections.Generic;

namespace UserDataNamespace
{
    public class UserData
    {
        public string Login { get; }
        public string Password { get; }
        public DateTime DateOfBirth { get; }
        public Dictionary<string, int> QuizResults { get;}

        public UserData(string Login, string Password, DateTime DateOfBirth)
        {
            this.Login = Login;
            this.Password = Password;
            this.DateOfBirth = DateOfBirth.Date;
            QuizResults = new Dictionary<string, int>();
        }
    }
}