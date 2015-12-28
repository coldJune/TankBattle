using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class Form1 : Form
    {
        TankCilent tc = new TankCilent();

        Boolean startClik = false;
        Boolean stopClik = false;
        public Form1()
        {
            
            InitializeComponent();
        }



        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            tc.update(g);
            label2.Text = Tank.bombNumber.ToString();
            label4.Text = tc.myTank.restartChance.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            this.MinimumSize = new Size(TankCilent.GAME_WIDTH, TankCilent.GAME_HEIGHT);
           
            tc.lauchFrame();
            
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (startClik == false&&stopClik==false)
            {
                g.DrawImage(Properties.Resources.主图, 0, 0);
            }
            else{
                tc.paint(g);
            }
        
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.Refresh();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.AllPaintingInWmPaint, true);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            tc.myTank.KeyPressed(e);
           
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            tc.myTank.KeyReleased(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startClik = true;
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stopClik = true;
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            help f2 = new help();
            f2.Show();
            stopClik = true;
            timer1.Stop();
        }

        private void label1_Click(object sender, EventArgs e)
        {
         
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                case Keys.Left:
                case Keys.Down:
                case Keys.Right:
     return true;
                default:
                    return base.ProcessDialogKey(keyData);
            }
        }
    }
}
