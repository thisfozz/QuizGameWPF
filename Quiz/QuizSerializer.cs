using QuestionQuizNamespace;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections.ObjectModel;

namespace QuizSerializerNamespace
{
    public class QuizSerializer
    {
        public string ToJsonQuiz(ObservableCollection<QuestionQuiz> quiz)
        {
            try
            {
                string json = JsonConvert.SerializeObject(quiz, Formatting.Indented);
                return json;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
        public List<QuestionQuiz> DeserializeQuiz(string nameQuiz)
        {
            try
            {
                if (File.Exists(nameQuiz))
                {
                    string jsonData = File.ReadAllText(nameQuiz);
                    var deserializedData = JsonConvert.DeserializeObject<List<QuestionQuiz>>(jsonData);
                    return deserializedData ?? new List<QuestionQuiz>();
                }
                else
                {
                    Console.WriteLine($"Ошибка при загрузке данных: Файл {nameQuiz} не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            }

            return new List<QuestionQuiz>();
        }

        public void SerializeQuiz(string jsonQuiz, string nameQuiz)
        {
            if (!string.IsNullOrEmpty(jsonQuiz))
            {
                File.WriteAllText(nameQuiz, jsonQuiz);
            }
        }
    }
}