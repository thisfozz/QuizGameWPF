using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuizGame.RegistrationAndAuthorization
{
    public class DataCorrectness
    {
        public static bool isCheckLogin(string loginUser)
        {
            string pattern = @"^[a-zA-Z0-9]+$";

            if (string.IsNullOrEmpty(loginUser)) return false;
            if (loginUser.Length < 3 || loginUser.Length > 15) return false;
            if (HasRusCharacters(loginUser)) return false;
            if (!Regex.IsMatch(loginUser, pattern)) return false;

            return true;
        }
        public static bool isCheckPassword(string passwordUser)
        {
            if (string.IsNullOrEmpty(passwordUser)) return false;
            if (passwordUser.Length < 8) return false;
            if (HasRusCharacters(passwordUser)) return false;

            return true;
        }
        public static bool IsCheckDate(DateTime? date)
        {
            if (date == null) return false;

            return true;
        }
        public static DateTime ConvertToDate(string date)
        {
            DateTime convertedDate;
            if (DateTime.TryParseExact(date, "dd.MM.yyyy", null, DateTimeStyles.None, out convertedDate)) return convertedDate;

            return DateTime.MinValue;
        }
        private static bool HasRusCharacters(string input)
        {
            string pattern = @"[\p{IsCyrillic}]";

            if (Regex.IsMatch(input, pattern)) return true;

            return false;
        }
    }
}
