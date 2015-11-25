using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MancalaApplication
{
    public partial class MancalaBoard : Form
    {

        private Button[] arrayPockets;
        private bool player1Turn;
        private bool compPlay;
        private bool p1Win;
        private bool p2Win;
        private int compChoice;
        private int endPocket;
        private bool pvcEnabled;

        public MancalaBoard()
        {
            InitializeComponent();
        }

        MancalaLogic game1;

        private void MancalaBoard_Load(object sender, EventArgs e)
        {
            arrayPockets = new Button[12];

            IEnumerable<Button> query1 = this.Controls.OfType<Button>();

            arrayPockets[0] = button1;
            arrayPockets[1] = button2;
            arrayPockets[2] = button3;
            arrayPockets[3] = button4;
            arrayPockets[4] = button5;
            arrayPockets[5] = button6;
            arrayPockets[6] = button7;
            arrayPockets[7] = button8;
            arrayPockets[8] = button9;
            arrayPockets[9] = button10;
            arrayPockets[10] = button11;
            arrayPockets[11] = button12;

            game1 = new MancalaLogic(arrayPockets);
            InitialiseBoard();
        }

        private void InitialiseBoard()
        {
            player1Turn = true;
            pvpButton.Checked = true;
            pvcEnabled = true;
            p1Win = false;
            p2Win = false;
            labelPlayer1.Text = "Player 1";
            labelPlayer1.Font = new Font(labelPlayer1.Font, FontStyle.Bold);
            labelPlayer2.Text = "Player 2";
            labelPlayer2.Font = new Font(labelPlayer2.Font, FontStyle.Regular);

            for (int i = 0; i < arrayPockets.Length; i++)
            {
                foreach (Button btn in this.Controls.OfType<Button>())
                {
                    if (btn == arrayPockets[i])
                    {
                        if (i == 5 || i == 11)
                        {
                            btn.Text = "0";
                        }
                        else
                        {
                            btn.Text = "3";
                            if (i > 5 && i < 12)
                            {
                                btn.Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (pvcButton.Checked == false)
            {
                pvcEnabled = false;
            }
            Button selectedPocket = sender as Button;
            int pocket;
            int beads;
            pocket = game1.PocketPlace(selectedPocket);
            beads = Convert.ToInt16(selectedPocket.Text);
            game1.MoveBeads(pocket, beads);
            DoNext();
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            InitialiseBoard();
            pvcButton.Enabled = true;
        }

        private void CheckRules()
        {
            if (game1.Rule1 == true)
            {
                if (player1Turn == true)
                {
                    arrayPockets[11].Text = Convert.ToString(Convert.ToInt16(arrayPockets[11].Text) + Convert.ToInt16(arrayPockets[10 - game1.EndPocket].Text));
                }
                else if (player1Turn == false)
                {
                    arrayPockets[5].Text = Convert.ToString(Convert.ToInt16(arrayPockets[5].Text) + Convert.ToInt16(arrayPockets[10 - game1.EndPocket].Text));
                }
                arrayPockets[10 - game1.EndPocket].Text = "0";
                Debug.WriteLine(game1.EndPocket);
                Debug.WriteLine(10 - game1.EndPocket);
            }
        }

        private void DoNext()
        {
            CheckRules();
            ChangeTurn();
            UpdateBoard();
            CheckForP2Win();
            CheckForP1Win();
            CheckCompTurn();
        }

        private void CheckCompTurn()
        {
            if (pvcButton.Checked == true)
            {
                CompTurn();
            }
            if (pvcButton.Checked == false)
            {

            }
        }

        private void ChangeTurn()
        {
            if (player1Turn == true)
            {
                if (compPlay == false)
                {
                    for (int i = 0; i < (arrayPockets.Length / 2) - 1; i++)
                    {
                        foreach (Button btn in this.Controls.OfType<Button>())
                        {
                            if (btn == arrayPockets[i])
                            {
                                btn.Enabled = true;
                                btn.Cursor = Cursors.Hand;
                            }
                        }
                    }
                }
                for (int i = (arrayPockets.Length / 2); i < arrayPockets.Length - 1; i++)
                {
                    foreach (Button btn in this.Controls.OfType<Button>())
                    {
                        if (btn == arrayPockets[i])
                        {
                            btn.Enabled = false;
                        }
                    }
                }
                player1Turn = false;
            }
            else if (player1Turn == false)
            {
                for (int i = 0; i < ((arrayPockets.Length / 2) - 1); i++)
                {
                    foreach (Button btn in this.Controls.OfType<Button>())
                    {
                        if (btn == arrayPockets[i])
                        {
                            btn.Enabled = false;
                        }
                    }
                }
                for (int i = (arrayPockets.Length / 2); i < arrayPockets.Length - 1; i++)
                {
                    foreach (Button btn in this.Controls.OfType<Button>())
                    {
                        if (btn == arrayPockets[i])
                        {
                            btn.Enabled = true;
                            btn.Cursor = Cursors.Hand;
                        }
                    }
                }
                player1Turn = true;
            }

            labelPlayer1.Font = new Font(labelPlayer1.Font, labelPlayer1.Font.Style ^ FontStyle.Bold);
            labelPlayer2.Font = new Font(labelPlayer2.Font, labelPlayer2.Font.Style ^ FontStyle.Bold);
        }

        private void UpdateBoard()
        {
            for (int i = 0; i < arrayPockets.Length; i++)
            {
                foreach (Button btn in this.Controls.OfType<Button>())
                {
                    if (btn == arrayPockets[i])
                    {
                        if (btn.Text == "0")
                        {
                            btn.Enabled = false;
                        }
                    }
                }
            }
        }

        private void CheckForP2Win()
        {
            if (p1Win == false)
            {
                int win = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (arrayPockets[i].Text == "0")
                    {
                        win += 1;
                    }
                }
                if (win == 5)
                {
                    for (int i = 6; i < 11; i++)
                    {
                        arrayPockets[5].Text = Convert.ToString(Convert.ToInt16(arrayPockets[5].Text) + Convert.ToInt16(arrayPockets[i].Text));
                        arrayPockets[i].Text = "0";
                        arrayPockets[i].Enabled = false;
                    }
                    if (Convert.ToInt16(arrayPockets[5].Text) > Convert.ToInt16(arrayPockets[11].Text))
                    {
                        labelPlayer2.Text = "Player 2 Wins";
                        labelP2Win.Text = Convert.ToString(Convert.ToInt16(labelP2Win.Text) + 1);
                    }
                    else if (Convert.ToInt16(arrayPockets[5].Text) == Convert.ToInt16(arrayPockets[11].Text))
                    {
                        labelPlayer2.Text = "Draw";
                    }
                }
            }
        }

        private void CheckForP1Win()
        {
            if (p2Win == false)
            {
                int win = 0;
                for (int i = 6; i < 12; i++)
                {
                    if (arrayPockets[i].Text == "0")
                    {
                        win += 1;
                    }
                }
                if (win == 5)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        arrayPockets[11].Text = Convert.ToString(Convert.ToInt16(arrayPockets[11].Text) + Convert.ToInt16(arrayPockets[i].Text));
                        arrayPockets[i].Text = "0";
                        arrayPockets[i].Enabled = false;
                    }
                    if (Convert.ToInt16(arrayPockets[5].Text) < Convert.ToInt16(arrayPockets[11].Text))
                    {
                        labelPlayer1.Text = "Player 1 Wins";
                        labelP1Win.Text = Convert.ToString(Convert.ToInt16(labelP1Win.Text) + 1);
                    }
                    else if (Convert.ToInt16(arrayPockets[5].Text) == Convert.ToInt16(arrayPockets[11].Text))
                    {
                        labelPlayer1.Text = "Draw";
                    }
                }
            }
        }

        private void instructionsButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Take turns to move 'beads' around the board.\nThe box with your colour on is your 'store'.\nThis is your final score.\nIf you run out of beads on your side of the\nboard then you win and take your oponents\nremaining beads.");
        }

        private void pvcButton_CheckedChanged(object sender, EventArgs e)
        {
            if (pvcButton.Checked == true)
            {
                if (pvcEnabled == true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        arrayPockets[i].Enabled = false;
                        compPlay = true;
                    }
                }
                else
                {
                    pvpButton.Checked = true;
                }
            }
            else if (pvpButton.Checked == true)
            {
                compPlay = false;
            }
        }

        public void CompTurn()
        {
            Random rnd = new Random();
            compChoice = rnd.Next(5);
            Debug.WriteLine("compChoice: " + compChoice);
            int beads;
            Debug.WriteLine("array" + arrayPockets[compChoice]);
            beads = Convert.ToInt16(arrayPockets[compChoice].Text);
            game1.MoveBeads(compChoice, beads);
            DoNext();
        }



    }
}

