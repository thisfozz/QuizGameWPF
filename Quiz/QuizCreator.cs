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