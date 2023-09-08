using AesEncryptionNamespace;
using DataCorrectnessNamespace;
using System;
using System.Threading;
using AuthenticationManagerNamespace;

namespace RegistrationNamespace
{
    public class Registration : IDisposable
    {
        private readonly AesEncryption aesEncryption = new AesEncryption();
        private readonly AuthenticationManager authenticationManager = new AuthenticationManager();
        
        public event EventHandler RegistrationSuccess;

        //private void RegistrationInputData()
        //{
        //    int cursorPositionInput, cursorNotifyAndInput;
        //    string text = string.Empty;

        //    try
        //    {
        //        text = "Недопустимый формат логина";
        //        cursorPositionInput = 44;
        //        cursorNotifyAndInput = 6;
        //        Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
        //        string loginRegistrationUser = Console.ReadLine();
        //        while (!DataCorrectness.isCheckLogin(loginRegistrationUser))
        //        {
        //            //AuxiliaryLog.AuxiliaryLogErrorinput(text, cursorPositionInput, cursorNotifyAndInput);
        //            loginRegistrationUser = Console.ReadLine();
        //        }

        //        text = "Недопустимый формат пароля";
        //        cursorPositionInput = 45;
        //        cursorNotifyAndInput = 8;
        //        Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
        //        string passwordRegistrationUser = Console.ReadLine();
        //        while (!DataCorrectness.isCheckPassword(passwordRegistrationUser))
        //        {
        //            //AuxiliaryLog.AuxiliaryLogErrorinput(text, cursorPositionInput, cursorNotifyAndInput);
        //            passwordRegistrationUser = Console.ReadLine();
        //        }
        //        string passwordEncrypt = aesEncryption.Encrypt(passwordRegistrationUser);

        //        text = "Недопустимый формат даты";
        //        cursorPositionInput = 52;
        //        cursorNotifyAndInput = 10;
        //        Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
        //        string dateBirthRegistrationUser = Console.ReadLine();
        //        while (!DataCorrectness.IsCheckDate(dateBirthRegistrationUser))
        //        {
        //            //AuxiliaryLog.AuxiliaryLogErrorinput(text, cursorPositionInput, cursorNotifyAndInput);
        //            dateBirthRegistrationUser = Console.ReadLine();
        //        }
        //        DateTime correctData = DataCorrectness.ConvertToDate(dateBirthRegistrationUser);

        //        bool isRegistration = authenticationManager.RegisterUser(loginRegistrationUser, passwordEncrypt, correctData);

        //        ProcessRegistrationResult(isRegistration);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Невероятная ошибка при регистрации: {ex.Message}");
        //    }
        //}

        //private void ProcessRegistrationResult(bool isRegistration)
        //{
        //    if (isRegistration)
        //    {
        //        Console.Clear();
        //        Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
        //        Console.WriteLine("\t\t\t\t║                                                       ║");
        //        Console.WriteLine("\t\t\t\t║             Регистрация прошла успешно                ║");
        //        Console.WriteLine("\t\t\t\t║                                                       ║");
        //        Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");
        //        Thread.Sleep(2000);
        //        RegistrationSuccess?.Invoke(this, EventArgs.Empty);
        //    }
        //    else
        //    {
        //        Console.Clear();
        //        Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
        //        Console.WriteLine("\t\t\t\t║                                                       ║");
        //        Console.WriteLine("\t\t\t\t║   Пользователь с таким логином уже зарегистрирован    ║");
        //        Console.WriteLine("\t\t\t\t║                                                       ║");
        //        Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");

        //        Thread.Sleep(1500);
        //    }
        //}
        //private void RegistrationForm()
        //{
        //    Console.Clear();
        //    Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
        //    Console.WriteLine("\t\t\t\t║                                                       ║");
        //    Console.WriteLine("\t\t\t\t║                     Регистрация                       ║");
        //    Console.WriteLine("\t\t\t\t║                                                       ║");
        //    Console.WriteLine("\t\t\t\t╠═══════════════════════════════════════════════════════║");
        //    Console.WriteLine("\t\t\t\t║                                                       ║");
        //    Console.WriteLine("\t\t\t\t║    Логин:                                             ║");
        //    Console.WriteLine("\t\t\t\t║                                                       ║");
        //    Console.WriteLine("\t\t\t\t║    Пароль:                                            ║");
        //    Console.WriteLine("\t\t\t\t║                                                       ║");
        //    Console.WriteLine("\t\t\t\t║    Дата рождения:                                     ║");
        //    Console.WriteLine("\t\t\t\t║                                                       ║");
        //    Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");
        //    RegistrationInputData();
        //}

        //public void RegistrationUser()
        //{
        //    RegistrationForm();
        //}

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
