using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DataCorrectnessNamespace
{
    public class DataCorrectness
    {
        public bool isCheckLogin(string loginUser)
        {
            string pattern = @"^[a-zA-Z0-9]+$";

            if (string.IsNullOrEmpty(loginUser)) return false;
            if (loginUser.Length < 3 || loginUser.Length > 15) return false;
            if (HasRusCharacters(loginUser)) return false;
            if (!Regex.IsMatch(loginUser, pattern)) return false;

            return true;
        }
        public bool isCheckPassword(string passwordUser)
        {
            if (string.IsNullOrEmpty(passwordUser)) return false;
            if (passwordUser.Length < 8) return false;
            if (HasRusCharacters(passwordUser)) return false;

            return true;
        }
        public bool IsCheckDate(DateTime? date)
        {
            if (date == null) return false;
            
            return true;
        }
        private bool HasRusCharacters(string input)
        {
            string pattern = @"[\p{IsCyrillic}]";

            if (Regex.IsMatch(input, pattern)) return true;

            return false;
        }
    }
}