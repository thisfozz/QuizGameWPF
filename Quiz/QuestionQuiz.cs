using AnswerQuizNamespace;
using System.Collections.Generic;

namespace QuestionQuizNamespace
{
    public class QuestionQuiz
    {
        public string Question { get; set; }
        public List<AnswerQuiz> Answers { get; set; }
    }
}