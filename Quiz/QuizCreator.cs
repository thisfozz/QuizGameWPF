using QuestionQuizNamespace;
using QuizSerializerNamespace;
using System;
using System.Collections.Generic;
using AnswerQuizNamespace;
using System.Threading;

namespace QuizCreatorNamespace
{
    public class QuizCreator
    {
        private static readonly QuizSerializer quizSerializer = new QuizSerializer();

        public static int CorrectAnswers { get; set; }
        public void CreateQuiz()
        {
            List<QuestionQuiz> quiz = new List<QuestionQuiz>();

            while (true)
            {
                Console.WriteLine("Введите вопрос (или введите 'save' для завершения и сохранения викторины):");
                string question = Console.ReadLine();

                if (question == "save") break;

                Console.WriteLine("Введите варианты ответов (или введите 'save' для завершения вопроса):");

                List<AnswerQuiz> answers = new List<AnswerQuiz>();

                while (true)
                {
                    Console.Write("Введите ответ: ");
                    string answerText = Console.ReadLine();

                    if (answerText == "save") break;

                    Console.WriteLine("Это правильный ответ? Введите 'да' или 'нет':");
                    string isCorrectInput = Console.ReadLine();
                    bool isCorrectAnswer = isCorrectInput.ToLower() == "да";

                    AnswerQuiz answer = new AnswerQuiz
                    {
                        Answer = answerText,
                        IsCorrectAnswer = isCorrectAnswer
                    };

                    answers.Add(answer);
                }

                QuestionQuiz questionQuiz = new QuestionQuiz
                {
                    Question = question,
                    Answers = answers
                };

                quiz.Add(questionQuiz);
                Console.Clear();
            }

            Console.WriteLine("Введите название файла для сохранения викторины:(без указания типа) ");
            string fileName = Console.ReadLine();
            string filePath = $"{fileName}.json";

            string jsonQuiz = quizSerializer.ToJsonQuiz(quiz);

            quizSerializer.SerializeQuiz(jsonQuiz, filePath);

            Console.WriteLine($"Викторина сохранена в файле: {filePath}");
        }
        public static bool isCheckFile(string nameQuiz)
        {
            string filePath = $"{nameQuiz}.json";
            List<QuestionQuiz> quizHistory = quizSerializer.DeserializeQuiz(filePath);

            if (quizHistory.Count == 0)
            {
                return false;
            }
            return true;
        }
        public static void StartQuiz(string nameQuiz)
        {
            string filePath = $"{nameQuiz}.json";
            QuizSerializer quizSerializer = new QuizSerializer();
            List<QuestionQuiz> quizHistory = quizSerializer.DeserializeQuiz(filePath);

            int totalQuestions = quizHistory.Count;
            int counterQuestions = 0;

            ConsoleKeyInfo keyInfo;

            foreach (QuestionQuiz questionQuiz in quizHistory)
            {
                int counterAnswers = 0;
                foreach (AnswerQuiz answer in questionQuiz.Answers)
                {
                    counterAnswers++;
                }

                int userAnswer = -1;
                bool isValidAnswer = false;

                while (!isValidAnswer)
                {
                    keyInfo = Console.ReadKey(true);
                    if (char.IsDigit(keyInfo.KeyChar))
                    {
                        userAnswer = int.Parse(keyInfo.KeyChar.ToString());
                        if (userAnswer >= 1 && userAnswer <= counterAnswers)
                        {
                            isValidAnswer = true;
                        }
                    }
                }
                AnswerQuiz selectedAnswer = questionQuiz.Answers[userAnswer - 1];
                if (selectedAnswer.IsCorrectAnswer)
                {
                    CorrectAnswers++;
                }
                Console.Clear();
            }
        }
    }
}