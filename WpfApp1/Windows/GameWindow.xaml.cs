using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private int size;
        private Game game;
        private Button[,] buttons;

        public GameWindow(User user, int size)
        {
            InitializeComponent();
            Application.Current.MainWindow = this;

            this.size = size;
            this.game = new Game(user, size);
            this.buttons = InitializeButtons();

            lblWelcome.Content = user.Welcome();
        }

        private Button[,] InitializeButtons()
        {
            mainGrid.Height = size * 100;
            Application.Current.MainWindow.Height = size * 100 + 220;
            Application.Current.MainWindow.Width = size * 100 + 100;

            Button[,] buttons = new Button[size, size];

            RowDefinition rowDefinition;
            ColumnDefinition columnDefinition;

            //https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.grid?view=net-5.0
            for (int i = 0; i < size; i++)
            {
                rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(100, GridUnitType.Star);
                mainGrid.RowDefinitions.Add(rowDefinition);

                columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(100, GridUnitType.Star);
                mainGrid.ColumnDefinitions.Add(columnDefinition);

                for (int j = 0; j < size; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Name = Utils.GetButtonName(i, j, size);
                    //buttons[i, j].Content = buttons[i, j].Name;
                    buttons[i, j].FontSize = 50;
                    buttons[i, j].Click += new RoutedEventHandler(ButtonClickHandler);
                    Grid.SetRow(buttons[i, j], i);
                    Grid.SetColumn(buttons[i, j], j);

                    mainGrid.Children.Add(buttons[i, j]);
                }
            }
            return buttons;
        }

        private async void ButtonClickHandler(object sender, RoutedEventArgs e)
        {
            Button srcButton = e.Source as Button;
            //daca nu este completat labelul cu WIN/LOS4E/DRAW
            if (string.IsNullOrEmpty((string)srcButton.Content) && game.GetStatus().Equals(""))
            {
                srcButton.Content = "X";

                game.PlayX(Utils.GetButtonPosition(srcButton.Name));
                game.CheckForWin();

                if (game.GetStatus().Equals(""))
                {
                    await Task.Delay(200);
                    int oSquare = game.PlayO();
                    buttons[oSquare / size, oSquare % size].Content = "O";
                    game.CheckForWin();
                }
                lblResult.Content = game.GetStatus();

                if (lblResult.Content.Equals("WIN") || lblResult.Content.Equals("LOSE"))
                    ColorSquares(game.GetWinningSquares());
                
            }
        }

        private void ColorSquares(List<string> list)
        {
            foreach (var item in list)
            {
                for (int i = 0; i < size * size; i++)
                {
                    if (item.Equals(buttons[i / size, i % size].Name) && lblResult.Content.Equals("WIN"))
                        buttons[i / size, i % size].Background = Brushes.Green;
                    if (item.Equals(buttons[i / size, i % size].Name) && lblResult.Content.Equals("LOSE"))
                        buttons[i / size, i % size].Background = Brushes.Red;
                }
            }
        }

        private void BtnReplay_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(game.GetUser());
            mainWindow.Show();
            this.Close();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
