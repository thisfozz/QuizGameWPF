using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuizGame.Model
{
    public class UserModel : INotifyPropertyChanged
    {
        private string login;
        private string password;
        private DateTime dateOfBirth;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }
        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                dateOfBirth = value;
                OnPropertyChanged();
            }
        }
    }
}
