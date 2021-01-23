using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dinosaur_Game
{
    public partial class Form1 : Form
    {
        bool jumping = false;
        int jumpSpeed = 10;
        int force = 12;
        int score = 0;
        int obstacleSpeed = 10;
        readonly Random rand = new Random();


        public Form1()
        {
            InitializeComponent();

            resetGame();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        private void GameEvent(object sender, EventArgs e)
        {
            Trex.Top += jumpSpeed;

            txtScore.Text = "Score: " + score;

            if (jumping && force < 0)
            {
                jumping = false;
            }

            if (jumping)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;

                    if (x.Left + x.Width < -120)
                    {
                        x.Left = this.ClientSize.Width + rand.Next(200, 800);
                        score++;
                    }

                    if (Trex.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop();
                        Trex.Image = Properties.Resources.dead;
                        txtScore.Text += "  Press R to restart";
                    }
                }
            }
            if (Trex.Top >= 380 && !jumping)
            {
                // then we do the following
                force = 12; // set the force to 8
                Trex.Top = Floor.Top - Trex.Height; // also we place the player on top of the picture box
                jumpSpeed = 0; // set the jump speed to 0
            }

            // if score is equals or greater than 10
            if (score >= 10)
            {
                // the obstacle speed change to 15
                obstacleSpeed = 15;
            }
        }
        public void resetGame()
        {
            // This is the reset function
            force = 12; // set the force to 8
            Trex.Top = Floor.Top - Trex.Height; // also we place the player on top of the picture box
            jumpSpeed = 0; // set the jump speed to 0
            jumping = false; // change jumping to false
            score = 0; // set score to 0
            obstacleSpeed = 10; // set obstacle speed back to 10
            txtScore.Text = "Score: " + score; // change the score text to just show the score
            Trex.Image = Properties.Resources.running; // change the t rex image to running

            foreach (Control x in this.Controls)
            {
                // is X is a picture box and it has a tag of obstacle
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    // generate a random number in the position integer between 600 and 1000
                    int position = rand.Next(600, 1000);

                    // change the obstacles left position to a random location begining of the game
                    x.Left = 640 + (x.Left + position + x.Width * 3);
                }
            }
            gameTimer.Start(); // start the timer
        }
        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !jumping)
            {
                jumping = true;
            }
        }
        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                resetGame();
            }
            if (jumping)
            {
                jumping = false;
            }
        }
    }
}
