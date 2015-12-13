using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    class TankCilent
    {
        //设定窗口大小
        public  const int GAME_WIDTH = 800;
        public const int GAME_HEIGHT = 640;

        //设定游戏界面大小
        public const int PLAY_WIDTH = 600;
        public const int PLAY_HEIGHT = 600;
        public  Boolean baselive = true;
        public  int TANKNUM = 0;

        public static TankCilent tc;

    public  Tank myTank = new Tank(160, 550, true, Direction.STOP, tc);

      public static List<Explode> explodes = new List<Explode>();//装爆炸
        public  static List<Missile> missile = new List<Missile>();//装子弹
        public  static List<Tank> tanks = new List<Tank>();//装坦克

        Bitmap offScreenImage = null;
        public Image[] backGroundImages = {Properties.Resources.开始,
                                                   Properties.Resources.帮助,
                                                   Properties.Resources.you_win,
                                                   Properties.Resources.game_over};
        

         Blood blood = new Blood();//画出血块
        Bomb bomb = new Bomb();

        public void paint(Graphics g)
        {
           
            g.DrawImage(backGroundImages[0], 600, 0);
            Wall w = new Wall();
            w.draw(g);

            blood.draw(g);
            bomb.draw(g);
            myTank.draw(g);
            myTank.collidesWithTanks(tanks);//己方撞敌方
            myTank.collodesWithWall(w);//己方撞墙
            myTank.eat(blood);//吃血块
            myTank.eat(bomb);//吃炸弹
            //生成随机数，杜绝产生随机数相同的情况
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random r = new Random(seekSeek);
            if (tanks.Count() <= 4 && TANKNUM < 30)
            {
                for (int i = 0; i < 1; i++)//死一个添加一个
                {
                    TANKNUM++;
                    int k = 0;
                    while (k == 0)
                    {
                        int x = r.Next(29);
                        int y = r.Next(14);
                        Boolean b = Tank.collidesWithTanks(tanks,x*20,y*20);
                        if (Wall.map[x, y] == 0 && Wall.map[x + 1, y] == 0 && Wall.map[x, y + 1]==0 && Wall.map[x + 1, y + 1] == 0 && !b)
                        {
                            tanks.Add(new Tank(x * 20, y * 20, false, Direction.D, this));
                            k++;
                        }
                    }
                }
            }
            if (TANKNUM >= 30 && tanks.Count() == 0)
            {
                g.DrawImage(backGroundImages[3], 0, 0);
                myTank.restartChance = 0;
                myTank.setLive(false);
            }
            else if (myTank.isLive() == false && myTank.restartChance == 0)
            {
                g.DrawImage(backGroundImages[4], 0, 0);
            }
            if (baselive == false)
            {
                g.DrawImage(backGroundImages[4], 0, 0);
                myTank.restartChance = 0;
                myTank.setLive(false);
            }

            //画子弹
            for (int i = 0; i < missile.Count(); i++)
            {
                Missile m = missile[i];
                m.hitTanks(tanks);
                m.hitTank(myTank);

                m.hitWall(w);

                m.draw(g);
            }

            //画爆炸效果
            for (int i = 0; i < explodes.Count(); i++)
            {
                Explode e = explodes[i];
                e.draw(g);
            }

            //画坦克
            for (int i = 0; i < tanks.Count(); i++)
            {
                Tank t = tanks[i];

                t.collidesWithTanks(tanks);
                t.collodesWithWall(w);
                t.draw(g);
            }
        }
        public void update(Graphics g)
        {
            if (offScreenImage == null)
            {
                offScreenImage = new Bitmap(PLAY_WIDTH ,PLAY_HEIGHT);
            }
            Graphics gOffScreen = Graphics.FromImage(offScreenImage);
            gOffScreen.Clear(Color.Black);
            SolidBrush sb = new SolidBrush(Color.Black);
            gOffScreen.FillRectangle(sb,0,0, PLAY_WIDTH, PLAY_HEIGHT);
            g.DrawImage(offScreenImage,0,0);
        }
        //坦克主窗口
        public void lauchFrame(){
           
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random r = new Random(seekSeek);
            for (int i = 0; i < 5; i++)
            {
                int k = 0;
                while (k == 0)
                {
                    int x = r.Next(29);
                    int y = r.Next(14);
                    Boolean b = Tank.collidesWithTanks(tanks, x * 20, y * 20);
                    if (Wall.map[x, y] == 0 && Wall.map[x + 1, y] == 0 && Wall.map[x, y + 1] == 0 && Wall.map[x + 1, y + 1] == 0 && !b)
                    {
                        tanks.Add(new Tank(x * 20, y * 20, false, Direction.D, this));
                        k++;
                    }
                }
            }
           
           
        }
       
        
    }
}
