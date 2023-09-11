﻿using AuthenticationManagerNamespace;
using Quiz.Command;
using QuizGame.Model;
using QuizGame.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;

namespace QuizGame.ViewModel
{
    internal class AuthorizationViewModel : INotifyPropertyChanged
    {
        private AuthenticationUser authenticationManager;

        public AuthorizationViewModel()
        {
            authenticationManager = new AuthenticationUser();

            UserModel = new UserModel();

            AuthorizationButton = new DelegateCommand(Login,
            (_) =>
            {
                return
                !string.IsNullOrEmpty(UserModel?.Login)
                && !string.IsNullOrEmpty(UserModel?.Password);
            });
            PropertyChanged += (_, _) => { ((DelegateCommand)AuthorizationButton).RaiseCanExecuteChanged(); };

        }

        public ICommand AuthorizationButton { get; }

        public bool isAuthorizationSuccessful { get; private set; }
        public bool IsAuthorizationSuccessful
        {
            get { return isAuthorizationSuccessful; }
            set
            {
                if (value != isAuthorizationSuccessful)
                {
                    isAuthorizationSuccessful = value;
                    OnPropertyChanged();
                }
            }
        }

        public string authorizationErrorMessage { get; private set; }
        public string AuthorizationErrorMessage
        {
            get { return authorizationErrorMessage; }
            set
            {
                if (value != authorizationErrorMessage)
                {
                    authorizationErrorMessage = value;
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
                    ((DelegateCommand)AuthorizationButton).RaiseCanExecuteChanged();
                };
                OnPropertyChanged();
            }
        }

        private void Login(object obj)
        {
            isAuthorizationSuccessful = authenticationManager.LoginUser(UserModel.Login, UserModel.Password);

            if (isAuthorizationSuccessful)
            {
                MainViewModel mainViewModel = new MainViewModel();

                MainMenuPage mainMenuPage = new MainMenuPage();
                mainMenuPage.DataContext = mainViewModel;

                Application.Current.MainWindow.Content = mainMenuPage;
            }
            else
            {
                AuthorizationErrorMessage = "Неверный логин или пароль";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}