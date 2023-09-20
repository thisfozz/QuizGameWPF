using AnswerQuizNamespace;
using QuestionQuizNamespace;
using Quiz.Command;
using QuizSerializerNamespace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace QuizGame.ViewModel
{
    public class CreatePackViewModel : INotifyPropertyChanged
    {
        private static readonly QuizSerializer quizSerializer = new QuizSerializer();

        private ObservableCollection<QuestionQuiz> quiz = new ObservableCollection<QuestionQuiz>();

        public ObservableCollection<QuestionQuiz> Quiz
        {
            get { return quiz; }
            set
            {
                quiz = value;
                OnPropertyChanged(nameof(Quiz));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private string _questionText;
        public string QuestionText
        {
            get => _questionText;
            set
            {
                _questionText = value;
                OnPropertyChanged(nameof(QuestionText));
            }
        }

        private string _answer1Text;
        public string Answer1Text
        {
            get => _answer1Text;
            set
            {
                _answer1Text = value;
                OnPropertyChanged(nameof(Answer1Text));
            }
        }

        private string _answer2Text;
        public string Answer2Text
        {
            get => _answer2Text;
            set
            {
                _answer2Text = value;
                OnPropertyChanged(nameof(Answer2Text));
            }
        }

        private string _answer3Text;
        public string Answer3Text
        {
            get => _answer3Text;
            set
            {
                _answer3Text = value;
                OnPropertyChanged(nameof(Answer3Text));
            }
        }

        private string _answer4Text;
        public string Answer4Text
        {
            get => _answer4Text;
            set
            {
                _answer4Text = value;
                OnPropertyChanged(nameof(Answer4Text));
            }
        }


        private bool _isCorrentAnswer1;
        public bool IsCorrentAnswer1
        {
            get => _isCorrentAnswer1;
            set
            {
                _isCorrentAnswer1 = value;
                OnPropertyChanged(nameof(IsCorrentAnswer1));
            }
        }

        private bool _isCorrentAnswer2;
        public bool IsCorrentAnswer2
        {
            get => _isCorrentAnswer2;
            set
            {
                _isCorrentAnswer2 = value;
                OnPropertyChanged(nameof(IsCorrentAnswer2));
            }
        }

        private bool _isCorrentAnswer3;
        public bool IsCorrentAnswer3
        {
            get => _isCorrentAnswer3;
            set
            {
                _isCorrentAnswer2 = value;
                OnPropertyChanged(nameof(IsCorrentAnswer3));
            }
        }
        private bool _isCorrentAnswer4;
        public bool IsCorrentAnswer4
        {
            get => _isCorrentAnswer4;
            set
            {
                _isCorrentAnswer2 = value;
                OnPropertyChanged(nameof(IsCorrentAnswer4));
            }
        }

        public ICommand AddQuestionCommand { get; }
        public ICommand SaveQuestionCommand { get; }
        public ICommand SerializeQuizCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public CreatePackViewModel()
        {
            AddQuestionCommand = new DelegateCommand(Add, (_) => true);
            DeleteCommand = new DelegateCommand(DeleteQuestion, (_) => true);
            EditCommand = new DelegateCommand(EditQuestion, (_) => true);
            SaveQuestionCommand = new DelegateCommand(Save, (_) => true);
            SerializeQuizCommand = new DelegateCommand(SerializeQuiz, (_) => true);
        }

        private void Add(object parametr) //ок
        {
            var questionQuiz = new QuestionQuiz
            {
                Question = QuestionText,
                Answers = new List<AnswerQuiz>
                {
                    new AnswerQuiz
                    {
                        Answer = Answer1Text,
                        IsCorrectAnswer = IsCorrentAnswer1
                    },
                    new AnswerQuiz
                    {
                        Answer = Answer2Text,
                        IsCorrectAnswer = IsCorrentAnswer2
                    },
                    new AnswerQuiz
                    {
                        Answer = Answer3Text,
                        IsCorrectAnswer = IsCorrentAnswer3
                    },
                    new AnswerQuiz
                    {
                        Answer = Answer4Text,
                        IsCorrectAnswer = IsCorrentAnswer4
                    },
                }
            };
            Quiz.Add(questionQuiz);

            QuestionText = string.Empty;

            Answer1Text = string.Empty;
            Answer2Text = string.Empty;
            Answer3Text = string.Empty;
            Answer4Text = string.Empty;

            IsCorrentAnswer1 = false;
            IsCorrentAnswer2 = false;
            IsCorrentAnswer3 = false;
            IsCorrentAnswer4 = false;

            OnPropertyChanged(nameof(Quiz));
        }

        private void Save(object parametr) // не ок
        {
            var question = (QuestionQuiz)parametr; // понятно, что тут будет null конечно
            
            question.Question = QuestionText;

            question.Answers[0].Answer = Answer1Text;
            question.Answers[1].Answer = Answer2Text;
            question.Answers[2].Answer = Answer3Text;
            question.Answers[3].Answer = Answer4Text;

            question.Answers[0].IsCorrectAnswer = IsCorrentAnswer1;
            question.Answers[1].IsCorrectAnswer = IsCorrentAnswer2;
            question.Answers[2].IsCorrectAnswer = IsCorrentAnswer3;
            question.Answers[3].IsCorrectAnswer = IsCorrentAnswer4;

            QuestionText = string.Empty;

            Answer1Text = string.Empty;
            Answer2Text = string.Empty;
            Answer3Text = string.Empty;
            Answer4Text = string.Empty;

            IsCorrentAnswer1 = false;
            IsCorrentAnswer2 = false;
            IsCorrentAnswer3 = false;
            IsCorrentAnswer4 = false;

            OnPropertyChanged(nameof(Quiz));
        }

        private void DeleteQuestion(object parametr) //ок
        {
            var question = (QuestionQuiz)parametr;
            Quiz.Remove(question);
        }

        private void EditQuestion(object parametr) //ок
        {
            QuestionQuiz questionQuiz = (QuestionQuiz)parametr;

            QuestionText = questionQuiz.Question;

            Answer1Text = questionQuiz.Answers[0].Answer;
            Answer2Text = questionQuiz.Answers[1].Answer;
            Answer3Text = questionQuiz.Answers[2].Answer;
            Answer4Text = questionQuiz.Answers[3].Answer;

            IsCorrentAnswer1 = questionQuiz.Answers[0].IsCorrectAnswer;
            IsCorrentAnswer2 = questionQuiz.Answers[1].IsCorrectAnswer;
            IsCorrentAnswer3 = questionQuiz.Answers[2].IsCorrectAnswer;
            IsCorrentAnswer4 = questionQuiz.Answers[3].IsCorrectAnswer;
        }

        private void SerializeQuiz(object parametr)
        {
            string JSONQuizz = quizSerializer.ToJsonQuiz(Quiz); //ок
            //Дописать сохранение в файл с нужнным названием в любое место, которое выберет пользователь
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
