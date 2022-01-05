using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_si_0.Game
{
    class Board
    {
        public char[,] board;
        public Board()
        {
            board = new char[3, 3] { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };
        }
        public bool is_win()
        {
            bool winner = false;

            // Verificare pe linii
            if (board[0, 0] == board[0, 1] && board[0, 1] == board[0, 2])
            {
                winner = true;
            }

            if (board[1, 0] == board[1, 1] && board[1, 1] == board[1, 2])
            {
                winner = true;
            }

            if (board[2, 0] == board[2, 1] && board[2, 1] == board[2, 2])
            {
                winner = true;
            }

            // Verificare pe coloane
            if (board[0, 0] == board[1, 0] && board[1, 0] == board[2, 0])
            {
                winner = true;
            }

            if (board[0, 1] == board[1, 1] && board[1, 1] == board[2, 1])
            {
                winner = true;
            }

            if (board[0, 2] == board[1, 2] && board[1, 2] == board[2, 2])
            {
                winner = true;
            }

            // Verificare diagonala principala
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                winner = true;
            }

            // Verificare diagonala secundara
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                winner = true;
            }

            return winner;
        }

        public bool is_draw()
        {
            if(is_win() == true)
            {
                return false;
            }    

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    // Daca exista un element care e diferit de X sau 0 inseamna ca jocul nu e gata
                    // Deci nu se poate determina daca e egal sau nu
                    if(board[i, j] != 'X' || board[i, j] != '0')
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // Metoda asta genereaza pozitiile valide pe care calculatorul le poate face
        public List<Board> generate_states()
        {
            List<Board> actions = new List<Board>();

            for(int row = 0; row < 3; row++)
            {
                for(int col = 0; col < 3; col++)
                {
                    // Inseamna ca pozitia respectiva este goala
                    if(board[row, col] != 'X' && board[row, col] != '0')
                    {
                        actions.Add(make_move(row, col));
                    }
                }
            }

            return actions;
        }

        //Aici e posibil sa ma insel
        Board make_move(int row, int col)
        {
            Board tempBoard = new Board();
            tempBoard.board = board.DeepClone();
            tempBoard.board[row, col] = '0';

            return tempBoard;
        }
    }
}
