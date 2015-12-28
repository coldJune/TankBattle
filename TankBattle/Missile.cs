using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    class Missile
    {   /*
         * 定义子弹速度
         */ 
        public const int XSPEED = 20;
        public const int YSPPED = 20;
        /*
         * 声明子弹长宽
         */ 
        public const int WIDTH = 10;
        public const int HEIGHT = 10;

        int x, y;//子弹位置
        Direction dir;//子弹方向

        private Boolean ownMissile;//己方子弹
        private Boolean live = true;//子弹默认存活
        private TankCilent tc;
        public Boolean isLive()
        {
            return live;
        }

        Missile(int x,int y,Direction dir)
        {
            this.x = x;
            this.y = y;
            this.dir = dir;
        }
        public Missile(int x,int y,Boolean ownMissile,Direction dir,TankCilent tc):
        this(x,y,dir){
            this.tc = tc;
            this.ownMissile = ownMissile;
        }
        private static   Dictionary<String,Image>  imags = new Dictionary<string,Image>();
        static Missile(){
             Image[] missiles = new Image[4];
             
            
            missiles[0] =Properties.Resources.missileL;
            missiles[1] =Properties.Resources.missileU;
            missiles[2] =Properties.Resources.missileR;
            missiles[3] =Properties.Resources.MissileD;
           
          
            imags.Add("L",missiles[0]);
             imags.Add("U",missiles[1]);
             imags.Add("R",missiles[2]);
             imags.Add("D",missiles[3]);

         }

        public void draw(Graphics g)
        {
            switch (dir)
            {
                case Direction.L:
                    g.DrawImage(imags["L"],x,y);
                    break;
                case Direction.U:
                    g.DrawImage(imags["U"], x, y);
                    break;
                case Direction.R:
                    g.DrawImage(imags["R"], x, y);
                    break;
                case Direction.D:
                    g.DrawImage(imags["D"], x, y);
                    break;
            }
            move();
        }

        /*
         * 定义子弹移动
         */
        private void move()
        {
            if (!live) {
                TankCilent.missile.Remove(this);
            }

            switch (dir)
            {
                case Direction.L:
                    x -= XSPEED;
                    break;
                case Direction .U:
                    y -= YSPPED;
                    break;
                case Direction.R:
                    x += XSPEED;
                    break;
                case Direction.D:
                    y += YSPPED;
                    break;
                case Direction.STOP:
                    break;
            }
           //子弹出界则死亡
            if (x < 0 || y < 0)
            {
                live = false;
            }
        }
        //拿到包围子弹的矩形
        public Rectangle getRect()
        {
            return new Rectangle(x, y, WIDTH, HEIGHT);
        }

        /*
         * 打坦克
         */
        public Boolean hitTank(Tank t)
        {
            if (this.live && this.getRect().IntersectsWith(t.getRect()) && this.ownMissile != t.isOwnTank() && t.isLive())
            {
                if (t.isOwnTank())
                {
                    t.setLife(t.getLife() - 10);
                    if (t.getLife() <= 0)
                    {
                        t.setLive(false);
                    }
                }
                else
                {
                    t.setLive(false);
                }

                this.live = false;
                Explode e = new Explode(x, y, tc);
                TankCilent.explodes.Add(e);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean hitTanks(List<Tank> tanks)
        {
            for(int i=0;i<tanks.Count();i++){
                if(hitTank(tanks[i])){
                    return true;
                }
            }
            return false;
        }

        //打墙
        public void hitWall(Wall w)
        {
            for (int i = 0; i < Wall.getGrix(); i++)
            {
                for (int j = 0; j < Wall.getGriy(); j++)
                {
                    if (this.live && w.isLive() && this.getRect().IntersectsWith(w.getRect(i, j, 0)))
                    {
                        if(Wall.map[i,j]==1){
                            Wall.map[i, j] = 0;
                        } if (Wall.map[i, j] == 2)
                        {

                        } if (Wall.map[i, j] == 4 || Wall.map[i, j] == 5 || Wall.map[i, j] == 6 || Wall.map[i, j] == 7)
                        {
                            Wall.map[i, j] = 0;
                            TankCilent.baselive = false;
                        }
                        this.live = false;
                    }
                }
            }
        }
    }
}
