using QuizGame.ViewModel;
using System.Windows;

namespace QuizGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            Width = 1600;
            Height = 800;
        }
    }
}
