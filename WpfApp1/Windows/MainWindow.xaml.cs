using System.Windows;
using WpfApp1.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HighScore highScore;

        public MainWindow()
        {
            InitializeComponent();
            this.highScore = Utils.ReadHighScoreFromXML("scores.xml");
        }

        public MainWindow(User user)
        {
            InitializeComponent();
            tbName.Text = user.name;
            this.highScore = Utils.ReadHighScoreFromXML("scores.xml");
        }

        private void BtnStartGame_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(tbName.Text);
            if (Utils.CheckTextBox(tbName))
            {
                GameWindow gameWindow = new GameWindow(user, cbGameType.SelectedIndex + 3);
                gameWindow.Show();
                this.Close();
            }
            else
                lblMessage.Content = "Enter your name!";
        }

        private void BtnHighScores_Click(object sender, RoutedEventArgs e)
        {
            HighScoreWindow highScoreWindow = new HighScoreWindow(this.highScore);
            highScoreWindow.Show();
        }

        private void btnExitGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
