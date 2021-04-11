using System.Collections.Generic;
using System.Windows;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class HighScoreWindow : Window
    {
        HighScore highScore;

        public HighScoreWindow(HighScore highScore)
        {
            InitializeComponent();
            this.highScore = highScore;
            lbHighScores.ItemsSource = highScore.users;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            this.highScore.users.Clear();
            Utils.WriteToXML(this.highScore, "scores.xml");
            HighScoreWindow highScoreWindow = new HighScoreWindow(highScore);
            highScoreWindow.Show();
            this.Close();
        }
    }
}
