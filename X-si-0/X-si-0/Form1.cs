using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using X_si_0.Algorithm;
using X_si_0.Game;

namespace X_si_0
{
    public partial class Form1 : Form
    {
        List<Button> buttons;

        Board board;

        private int _iterations = 1000;

        public Form1()
        {
            InitializeComponent();
            board = new Board();
        }

        private void placeSymbol(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonName = button.Name;
            
            buttons = new List<Button>() { btn00, btn01, btn02, btn10, btn11, btn12, btn20, btn21, btn22 };

            

            // Update screen user
            int coord_x = int.Parse(buttonName[3].ToString());
            int coord_y = int.Parse(buttonName[4].ToString());
            board.board[coord_x, coord_y] = 'X';
            button.Text = board.board[coord_x, coord_y].ToString();
             
            // Update screen AI
            if (board.is_win())
            {
                MessageBox.Show("Meciul s-a terminat a castigat omul!");
                resetBoard();
            }


    
             updateScreenBot();

            /*
            MessageBox.Show(board.board[0,0].ToString());
            MessageBox.Show(board.board[0,1].ToString());
            MessageBox.Show(board.board[0,2].ToString());

            MessageBox.Show(board.board[1, 0].ToString());
            MessageBox.Show(board.board[1, 1].ToString());
            MessageBox.Show(board.board[1, 2].ToString());

            MessageBox.Show(board.board[2, 0].ToString());
            MessageBox.Show(board.board[2, 1].ToString());
            MessageBox.Show(board.board[2, 2].ToString());
            */
            //board.board[coord_x, coord_y] = '0';
            //button.Text = board.board[coord_x, coord_y].ToString();


        }

        void updateScreenBot()
        {

            MTCS mtcs = new MTCS(_iterations);
            var best_move = mtcs.search(board);

            if(best_move.board != null)
            {
                board.board = best_move.board.board; //trimit tablei curente tabla cu modificari

                int index = 0;

                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        //string s = board.board[row, col];
                        Console.WriteLine(buttons[index].Text);
                        Console.WriteLine(board.board[row, col]);
                        if (board.board[row, col] == '0')
                        {
                            buttons[index].Text = "0";
                            buttons[index].Enabled = false;
                        }
                        index++;
                    }
                }

                // Daca s-a castigat un meci
                if (board.is_win())
                {
                    MessageBox.Show("Meciul s-a terminat a castigat calculatorul!");
                    resetBoard();
                }
            }
            else
            {
                MessageBox.Show("Meciul s-a terminat egal!");
                resetBoard();
            }
            
        }

        void resetBoard()
        {
            board.board = new char[3, 3] { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Text = "";
                buttons[i].Enabled = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void updateSimBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int.TryParse(nrSimTextBox.Text.ToString(), out _iterations);
            }
            catch(Exception ex)
            {

            }
        }

        /*
        void checkWinner()
        {
            // Verificare pe linii
            if(board[0, 0] == board[0, 1] && board[0, 1] == board[0, 2])
            {
                MessageBox.Show($"Castigatorul este {board[0, 0].ToString()}");
                winner = true;
            }

            if (board[1, 0] == board[1, 1] && board[1, 1] == board[1, 2])
            {
                MessageBox.Show($"Castigatorul este {board[1, 0].ToString()}");
                winner = true;
            }

            if (board[2, 0] == board[2, 1] && board[2, 1] == board[2, 2])
            {
                MessageBox.Show($"Castigatorul este {board[2, 0].ToString()}");
                winner = true;
            }

            // Verificare pe coloane
            if (board[0, 0] == board[1, 0] && board[1, 0] == board[2, 0])
            {
                MessageBox.Show($"Castigatorul este {board[0, 0].ToString()}");
                winner = true;
            }

            if (board[0, 1] == board[1, 1] && board[1, 1] == board[2, 1])
            {
                MessageBox.Show($"Castigatorul este {board[0, 1].ToString()}");
                winner = true;
            }

            if (board[0, 2] == board[1, 2] && board[1, 2] == board[2, 2])
            {
                MessageBox.Show($"Castigatorul este {board[0, 2].ToString()}");
                winner = true;
            }

            // Verificare diagonala principala
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                MessageBox.Show($"Castigatorul este {board[0, 0].ToString()}");
                winner = true;
            }

            // Verificare diagonala secundara
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                MessageBox.Show($"Castigatorul este {board[0, 2].ToString()}");
                winner = true;
            }

            // Resetare joc

            if(winner == true)
            {
                board = new char[3, 3] { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };
                
                for(int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].Text = "";
                    buttons[i].Enabled = true;
                }

                winner = false;
                playerTurn = true;
            }
        }

        */
    }
}
