using System.Collections.Generic;

namespace WpfApp1
{
    public class Game
    {
        private Board board;
        private User user;
        private string status;

        public Game(User user, int dim)
        {
            this.user = CheckPlayer(user);
            this.board = new Board(dim);
            this.status = string.Empty;
        }

        //returns User with persisted score or new User
        private User CheckPlayer(User user)
        {
            List<User> userList = Utils.ReadHighScoreFromXML("scores.xml").users;
            if (userList == null)
                return user;
            else
            {
                foreach (var item in userList)
                {
                    if (item.name.Equals(user.name))
                        return item;
                }
                return user;
            }
        }

        public User GetUser()
        {
            return this.user;
        }

        public int GetBoardSize()
        {
            return this.board.GetSize();
        }

        public string GetStatus()
        {
            return this.status;
        }

        public void PlayX(int xSquare)
        {
            board.SetSquare(xSquare, 'X');
        }

        public int PlayO()
        {
            int oSquare = board.GetRandomEmptyField();
            board.SetSquare(oSquare, 'O');
            return oSquare;
        }

        public void CheckForWin()
        {
            switch (board.IsWin())
            {
                case 'X':
                    user.score += 10;
                    this.status = "WIN";
                    Utils.WriteResultsToXML(user, "scores.xml");
                    break;
                case 'O':
                    user.score -= 10;
                    this.status = "LOSE";
                    Utils.WriteResultsToXML(user, "scores.xml");
                    break;
                case 'd':
                    this.status = "DRAW";
                    break;
                default:
                    break;
            }
        }

        public List<string> GetWinningSquares()
        {
            return this.board.GetLastCheckedSquareNames();
        }
    }
}
