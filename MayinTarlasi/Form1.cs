using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MayinTarlasi
{
    public partial class Form1 : Form
    {
        Image mineImage = Image.FromFile("mines.png");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BuildControls();
            GetBombs();
            GetCounterByBombs();
        }

        private void GetCounterByBombs()
        {
            Label lbl;
            foreach (Control item in this.Controls)
            {
                if (item is Label && item.Name != "label1" && item.Name != "label2")
                {
                    lbl = item as Label;
                    if (lbl.Image != mineImage)
                    {
                        lbl.Text = GetCounter(lbl);
                    }
                }
            }
        }

        private string GetCounter(Label lbl)
        {
            int counter = 0;
            Label currentLabel = null;

            foreach (Control item in this.Controls)
            {
                if (item is Label)
                {
                    currentLabel = item as Label;
                    if (currentLabel.Location.X == lbl.Location.X - 35)
                    {
                        if (currentLabel.Location.Y == lbl.Location.Y - 35 || currentLabel.Location.Y == lbl.Location.Y || currentLabel.Location.Y == lbl.Location.Y + 35)
                        {
                            if (currentLabel.Image == mineImage)
                                counter++;
                        }
                    }
                    else if (currentLabel.Location.X == lbl.Location.X)
                    {
                        if (currentLabel.Location.Y == lbl.Location.Y - 35 || currentLabel.Location.Y == lbl.Location.Y + 35)
                        {
                            if (currentLabel.Image == mineImage)
                                counter++;
                        }
                    }
                    else if (currentLabel.Location.X == lbl.Location.X + 35)
                    {
                        if (currentLabel.Location.Y == lbl.Location.Y - 35 || currentLabel.Location.Y == lbl.Location.Y || currentLabel.Location.Y == lbl.Location.Y + 35)
                        {
                            if (currentLabel.Image == mineImage)
                                counter++;
                        }
                    }
                }
            }

            return counter.ToString();
        }

        private void GetBombs()
        {
            Random rnd = new Random();
            int generatedX = 0,
                generatedY = 0;
            string labelName = string.Empty;
            Label currentLabel;

            for (int i = 0; i < 10; i++)
            {
                generatedX = rnd.Next(10);
                generatedY = rnd.Next(10);
                labelName = "lbl" + generatedX.ToString() + generatedY.ToString();

                currentLabel = this.Controls.Find(labelName, false)[0] as Label;
                if (currentLabel.Image != mineImage)
                {
                    currentLabel.Image = mineImage;
                }
                else
                {
                    i--;
                }
            }
        }

        private void BuildControls()
        {
            Button btn;
            Label lbl;

            int x = 50,
                y = 50;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    lbl = new Label();
                    lbl.Name = "lbl" + i.ToString() + j.ToString();
                    lbl.Text = "";
                    lbl.TextAlign = ContentAlignment.MiddleCenter;
                    lbl.Size = new Size(35, 35);
                    lbl.Location = new Point(x, y);
                    lbl.Hide(); // Visible = false;
                    this.Controls.Add(lbl);

                    btn = new Button();
                    btn.Name = "btn" + i.ToString() + j.ToString();
                    btn.Size = new Size(35, 35);
                    btn.Location = new Point(x, y);
                    btn.BackColor = Color.LightSeaGreen;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.MouseDownBackColor = Color.PaleTurquoise;
                    btn.MouseDown += Btn_MouseDown;
                    btn.Tag = lbl;
                    this.Controls.Add(btn);

                    x += 35;
                }

                y += 35;
                x = 50;
            }
        }

        private void Btn_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (e.Button == MouseButtons.Left)
            {
                btn.Visible = false;
                Label lbl = btn.Tag as Label;
                lbl.Show();
                if (lbl.Image == mineImage)
                {
                    //timer1.Enabled = false;
                    timer1.Stop();
                    MessageBox.Show("Game Over");
                    Application.Exit();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                btn.BackgroundImage = Image.FromFile("flag.png");
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                int mines = int.Parse(txtBombs.Text);
                txtBombs.Text = (mines - 1).ToString();
            }

            //timer1.Enabled = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int second = int.Parse(txtTimer.Text);
            second++;
            txtTimer.Text = second.ToString();
        }
    }
}
