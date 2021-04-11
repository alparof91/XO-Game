using System;
using System.Collections.Generic;

namespace WpfApp1
{
    class Board
    {
        private int size;
        private char[,] squares;
        private List<string> lastCheckedSquareNames;

        public Board(int size)
        {
            this.size = size;
            this.squares = new char[this.size, this.size];
            this.lastCheckedSquareNames = new List<string>();
            InitializeBoard(this.size, '_');
        }

        private void InitializeBoard(int size, char c)
        {
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    this.squares[i, j] = c;
                }
            }
        }

        private void PrintBoard()
        {
            for (int i = 0; i < this.size; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < this.size; j++)
                {
                    Console.Write(squares[i, j]);
                }
            }
            Console.WriteLine();
        }

        public int GetSize()
        {
            return this.size;
        }

        public void SetSquare(int squareNr, char value)
        {
            this.squares[squareNr / size, squareNr % size] = value;
            PrintBoard();
        }

        public char GetSquare(int i, int j)
        {
            return this.squares[i, j];
        }

        public char GetSquare(int i)
        {
            return this.squares[i / size, i % size];
        }

        public List<string> GetLastCheckedSquareNames()
        {
            return this.lastCheckedSquareNames;
        }

        public int GetRandomEmptyField()
        {
            Random random = new Random();
            int nr = random.Next(0, size * size);
            while (!squares[nr / size, nr % size].Equals('_'))
                nr = random.Next(0, size * size);
            return nr;
        }

        private bool CheckForXO(List<char> list)
        {
            foreach (var item in list)
            {
                if (item != list[0] || list[0].Equals('_'))
                    return false;
            }
            return true;
        }

        private List<char> GetLineFromSquare(int i, int j)
        {
            lastCheckedSquareNames.Clear();
            List<char> list = new List<char>();
            for (int n = 0; n < 3; n++)
            {
                list.Add(squares[i, j + n]);
                lastCheckedSquareNames.Add(Utils.GetButtonName(i, j + n, this.size));
            }
            return list;
        }

        private List<char> GetColumnFromSquare(int i, int j)
        {
            lastCheckedSquareNames.Clear();
            List<char> list = new List<char>();
            for (int n = 0; n < 3; n++)
            {
                list.Add(squares[i + n, j]);
                lastCheckedSquareNames.Add(Utils.GetButtonName(i + n, j, this.size));
            }
            return list;
        }

        private List<char> GetFirstDiagFromSquare(int i, int j)
        {
            lastCheckedSquareNames.Clear();
            List<char> list = new List<char>();
            for (int n = 0; n < 3; n++)
            {
                list.Add(squares[i + n, j + n]);
                lastCheckedSquareNames.Add(Utils.GetButtonName(i + n, j + n, this.size));
            }
            return list;
        }

        private List<char> GetSecondDiagFromSquare(int i, int j)
        {
            lastCheckedSquareNames.Clear();
            List<char> list = new List<char>();
            for (int n = 0; n < 3; n++)
            {
                list.Add(squares[i + n, j - n]);
                lastCheckedSquareNames.Add(Utils.GetButtonName(i + n, j - n, this.size));
            }
            return list;
        }

        public char IsWin()
        {
            int usedSquares = 0;

            //trecem prin matrice si verificam la pozitiile umplute daca e castigatoare
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (squares[i, j] != '_')
                    {
                        usedSquares++;

                        //check lines
                        //(j < size - 2) to stay between the line limits
                        if (j < size - 2 && CheckForXO(GetLineFromSquare(i, j)))
                            return squares[i, j];

                        //check columns
                        //(i < size - 2) to stay between the column limits
                        if (i < size - 2 && CheckForXO(GetColumnFromSquare(i, j)))
                            return squares[i, j];

                        //check \ diagonals
                        //(i < size - 2 && j < size - 2) to stay between the line and column limits
                        if (i < size - 2 && j < size - 2 && CheckForXO(GetFirstDiagFromSquare(i, j)))
                            return squares[i, j];

                        //check / diagonals
                        //(i < size - 2 && j > 1) to stay between the line and column limits
                        if (i < size - 2 && j > 1 && CheckForXO(GetSecondDiagFromSquare(i, j)))
                            return squares[i, j];
                    }
                }
            }
            if (usedSquares == size * size - 1)
                return 'd';
            else
                return '_';
        }
    }
}
