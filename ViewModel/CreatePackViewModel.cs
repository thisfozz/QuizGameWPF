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

        public ICommand AddQuestion { get; private set; }
        public ICommand DeleteCommand { get; }


        public CreatePackViewModel()
        {
            AddQuestion = new DelegateCommand(Add, (_) => true);
            DeleteCommand = new DelegateCommand(DeleteQuestion, (_) => true);
        }

        private void Add(object parametr)
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

        private void DeleteQuestion(object parametr)
        {
            var question = (QuestionQuiz)parametr;
            Quiz.Remove(question);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
