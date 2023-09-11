using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UserDataNamespace;
using Formatting = Newtonsoft.Json.Formatting;

namespace FileManagerNamespace
{
    public class LoadData
    {
        private const string FilePath = "UserData.json";

        public static List<UserData> LoadUserData()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string jsonData = File.ReadAllText(FilePath);
                    var deserializedData = JsonConvert.DeserializeObject<List<UserData>>(jsonData);
                    if (deserializedData != null)
                        return deserializedData;
                    else
                    {
                        //Console.WriteLine("Ошибка при загрузке данных: Некорректный формат файла.");
                    }
                }
                else
                {
                    //Console.WriteLine("Ошибка при загрузке данных: Файл не найден.");
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            }

            return new List<UserData>();
        }
        public static string ToJsonUserData(List<UserData> users)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
                return jsonData;
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Ошибка при преобразовании в JSON: {ex.Message}");
                return string.Empty;
            }
        }
        public static void SerializeUserData(string jsonUserData)
        {
            try
            {
                if (!string.IsNullOrEmpty(jsonUserData))
                {
                    File.WriteAllText(FilePath, jsonUserData);
                }
                else
                {
                    //Console.WriteLine("Ошибка при сохранении данных: Пустые данные.");
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        public static void SaveUserDataForUser(UserData user)
        {
            try
            {
                if (user == null)
                {
                    //Console.WriteLine("Ошибка при сохранении данных пользователя: Пользователь не задан.");
                    return;
                }

                List<UserData> users = LoadUserData();
                if (users == null)
                {
                    //Console.WriteLine("Ошибка при сохранении данных пользователя: Некорректный формат данных.");
                    return;
                }

                int index = users.FindIndex(u => u.Login == user.Login);
                if (index >= 0)
                {
                    users[index] = user;
                    string jsonUserData = ToJsonUserData(users);
                    SerializeUserData(jsonUserData);
                }
                else
                {
                    //Console.WriteLine("Ошибка при сохранении данных пользователя: Пользователь не найден.");
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Ошибка при сохранении данных пользователя: {ex.Message}");
            }
        }
    }
}