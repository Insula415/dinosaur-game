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
                
                force = 12; 
                Trex.Top = Floor.Top - Trex.Height; 
                jumpSpeed = 0;
            }

            
            if (score >= 10)
            {
                obstacleSpeed = 15;
            }
        }
        public void resetGame()
        {
           
            force = 12; 
            Trex.Top = Floor.Top - Trex.Height; 
            jumpSpeed = 0; 
            jumping = false;
            score = 0;
            obstacleSpeed = 10; 
            txtScore.Text = "Score: " + score; 
            Trex.Image = Properties.Resources.running; 

            foreach (Control x in this.Controls)
            {
                
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    
                    int position = rand.Next(600, 1000);
                    
                    x.Left = 640 + (x.Left + position + x.Width * 3);
                }
            }
            gameTimer.Start();
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
