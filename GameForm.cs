using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace saper
{
    public partial class GameForm : Form
    {
        minerButton[][] buttons;
        int size;
        int difficulty;
        public GameForm(int size_i, int difficulty_i)
        {
            InitializeComponent();
            size = size_i;
            difficulty = difficulty_i;
            generateField();
        }

        void generateField()
        {
            Random rand = new Random();

            // Creating the buttons
            buttons = new minerButton[size][];
            for (int i = 0; i < size; ++i)
                buttons[i] = new minerButton[size];

            // Setting the buttons' properties and behaviour
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j )
                {
                    buttons[i][j] = new minerButton();
                    buttons[i][j].Size = new Size(25, 25);
                    buttons[i][j].isBomb = false;
                    if (rand.Next(difficulty) == 0)
                        buttons[i][j].isBomb = true;
                    buttons[i][j].Location = new Point(23 * i, 23 * j);
                    buttons[i][j].coord = new int[2] {i, j};

                    // To check which mouse button was pressed
                    buttons[i][j].MouseUp += new MouseEventHandler(button_MouseUp);
                    this.Controls.Add(buttons[i][j]);
                }
            }

            // Count bombs for each cell
            for (int x = 0; x < size; ++x)
                for (int y = 0; y < size; ++y)
                    for (int i = y - 1; i < y + 2; ++i)
                        for (int j = x - 1; j < x + 2; ++j)
                            if (i > -1 && i < size && j > -1 && j < size && buttons[i][j].isBomb)
                                buttons[y][x].amountOfBombsAround++;
        }

        void button_MouseUp(object sender, MouseEventArgs e)
        {
            minerButton button = sender as minerButton;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (button.isBomb == true)
                    {
                        button.Image = Properties.Resources.bomb;
                        for (int i = 0; i < size; ++i)
                            for (int j = 0; j < size; ++j)
                                if (buttons[i][j].isBomb)
                                    buttons[i][j].Image = Properties.Resources.bomb;
                        MessageBox.Show("Game over!");
                        this.Close();
                        SizeForm newGame = new SizeForm();
                        newGame.Show();
                    }
                    else
                    {
                        button.Enabled = false;
                        button.Image = null;
                        if (button.amountOfBombsAround > 0)
                            button.Text = Convert.ToString(button.amountOfBombsAround);
                        else
                            openNeighbours(button.coord[0], button.coord[1]);
                    }
                break;

                case MouseButtons.Right:
                    switch (button.state)
                    {
                        case 0:
                            button.Image = Properties.Resources.flag;
                            button.state = 1;
                            break;
                        case 1:
                            button.Image = Properties.Resources.question;
                            button.state = 2;
                            break;
                        case 2:
                            button.Image = null;
                            button.state = 0;
                            break;
                    }
                    break;
            }
        }

        void openNeighbours(int x, int y)
        {
            for (int i = x - 1; i < x + 2; ++i)
                for (int j = y - 1; j < y + 2; ++j)
                    if (i > -1 && i < size && j > -1 && j < size && !(buttons[i][j].isBomb) && buttons[i][j].Enabled)
                    {
                        buttons[i][j].Image = null;
                        buttons[i][j].Enabled = false;
                        if (buttons[i][j].amountOfBombsAround > 0)
                            buttons[i][j].Text = Convert.ToString(buttons[i][j].amountOfBombsAround);
                        else
                            openNeighbours(buttons[i][j].coord[0], buttons[i][j].coord[1]);
                    }
        }
    }

    public class minerButton : Button
    {
        public Boolean isBomb { get; set; }
        public Int32 amountOfBombsAround { get; set; }
        public int state { get; set; }//0 = no state, 1 = flag, 2 = question mark
        public int[] coord { get; set; }
    }
}