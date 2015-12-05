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


        public Form1()
        {
            
            InitializeComponent();
        }



        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush sb = new SolidBrush(Color.Black);

            g.FillRectangle(sb, new Rectangle(0, 0, TankCilent.PLAY_WIDTH, TankCilent.PLAY_HEIGHT));     
              
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(TankCilent.GAME_WIDTH, TankCilent.GAME_HEIGHT);
            tc.lauchFrame();
            
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            tc.paint(g);
            timer1.Start();
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


   
      
    }
}
