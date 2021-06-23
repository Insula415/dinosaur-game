using System;
using System.Windows.Forms;

namespace Dinosaur_Game
{
    public partial class Form1 : Form
    {
        private bool _jumping;
        private int _jumpSpeed = 10;
        private int _force = 12;
        private int _score;
        private int _obstacleSpeed = 10;
        private readonly Random _rand = new Random();


        public Form1()
        {
            InitializeComponent();

            ResetGame();
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
            Trex.Top += _jumpSpeed;

            txtScore.Text = "Score: " + _score;

            if (_jumping && _force < 0)
            {
                _jumping = false;
            }

            if (_jumping)
            {
                _jumpSpeed = -12;
                _force -= 1;
            }
            else
            {
                _jumpSpeed = 12;
            }

            foreach (Control x in this.Controls)
            {
                if (!(x is PictureBox) || (string) x.Tag != "obstacle") continue;
                x.Left -= _obstacleSpeed;

                if (x.Left + x.Width < -120)
                {
                    x.Left = this.ClientSize.Width + _rand.Next(200, 800);
                    _score++;
                }

                if (!Trex.Bounds.IntersectsWith(x.Bounds)) continue;
                gameTimer.Stop();
                Trex.Image = Properties.Resources.dead;
                txtScore.Text += "  Press R to restart";
            }
            if (Trex.Top >= 380 && !_jumping)
            {
                
                _force = 12; 
                Trex.Top = Floor.Top - Trex.Height; 
                _jumpSpeed = 0;
            }

            
            if (_score >= 10)
            {
                _obstacleSpeed = 15;
            }
        }

        private void ResetGame()
        {
           
            _force = 12; 
            Trex.Top = Floor.Top - Trex.Height; 
            _jumpSpeed = 0; 
            _jumping = false;
            _score = 0;
            _obstacleSpeed = 10; 
            txtScore.Text = "Score: " + _score; 
            Trex.Image = Properties.Resources.running; 

            foreach (Control x in this.Controls)
            {
                if (!(x is PictureBox) || (string) x.Tag != "obstacle") continue;
                var position = _rand.Next(600, 1000);
                    
                x.Left = 640 + (x.Left + position + x.Width * 3);
            }
            gameTimer.Start();
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !_jumping)
            {
                _jumping = true;
            }
        }
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                ResetGame();
            }
            if (_jumping)
            {
                _jumping = false;
            }
        }
    }
}
