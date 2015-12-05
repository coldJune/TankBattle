using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    class Wall
    {   /*
         * wo1 -- 1 土墙
         * wo2 -- 2 石头墙
         * wo3 -- 3 草地
         * 基地
         * Bo1 -- 4
         * Bo2 -- 5
         * Bo3 -- 6
         * Bo4 -- 7
         * s   -- 8 河流
         */ 
        static Image[] img = null;
        public Wall()
        {
            img = new Image[9];
            img[1] = Properties.Resources.w01;
            img[2] = Properties.Resources.w02;
            img[3] = Properties.Resources.w03;
            img[4] = Properties.Resources.B01;
            img[5] = Properties.Resources.B02;
            img[6] = Properties.Resources.B03;
            img[7] = Properties.Resources.B04;
            img[8] = Properties.Resources.s;
        }
       public  static int[,] map = {
		{0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,1,0,0,0,0,0,3,3,3,3,3,3,0,0,0,0,0,1,1,1,1,0,1,1,0},
		{0,0,0,0,0,1,0,0,0,0,0,3,3,3,3,3,3,0,0,0,0,0,0,1,1,1,0,0,1,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,1,1,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
		
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,8,0,0,0,0,0,0,0,0,0,0,0,0},
		{1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,8,1,1,0,0,0,0,0,0,2,1,1,1},
		{1,1,1,1,0,0,0,0,0,0,0,0,1,1,1,1,1,8,1,1,0,0,0,0,0,0,2,1,1,1},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,2,1,4,6},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,2,1,5,7},
		{0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,0,0,8,0,0,0,0,0,0,0,0,2,1,1,1},
		{0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,8,0,0,0,0,0,0,1,1,2,1,1,1},
		{0,0,0,1,1,1,1,0,0,0,0,0,1,1,1,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,1,1,1,1,0,0,0,0,0,1,1,1,0,0,8,0,0,1,1,1,0,0,0,0,0,0,0},
		
		{0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,1,1,0,0,0},
		{0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,1,1,1,1,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,1,1,1,0,0,1,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,1,1,1,0,0,1,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,1,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,1,1,1,1,1,0,0,0},
		{0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,8,0,0,0,1,1,1,1,1,1,1,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,1,1,1,1,1,1,1,0,0},
		{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0},
		
	}; 
        private Boolean live = true;//墙是否存活
        public Boolean isLive()
        {
            return live;
        }
        private static int Griwidth = 20;
        private static int Griheight = 20;
        private static int Grix = TankCilent.PLAY_WIDTH / 20;
        private static int Griy = TankCilent.PLAY_HEIGHT / 20;

        public static int getGriwidth()
        {
            return Griwidth;
        }
        public static void setGriwidth(int griwidth)
        {
            Griwidth = griwidth;
        }

        public static int getGriheight()
        {
            return Griheight;
        }
        public static void setGriheight(int griheight)
        {
            Griheight = griheight;
        }

        public static int getGrix()
        {
            return Grix;
        }
        public static void setGrix(int grix)
        {
            Grix = grix;
        }

        public static int getGriy()
        {
            return Griy;
        }
        public static void setGriy(int griy)
        {
            Griy = griy;
        }
        /*
         * 碰撞检测
         */ 
        public Rectangle getRect(int i, int j, int k)
        {
            if (k == 1)
            {
                if (map[i, j] == 0 || map[i, j] == 3)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(i * Griwidth, j * Griheight, Griwidth, Griheight);
            }
            else if (k == 0)
            {
                if (map[i, j] == 0 || map[i, j] == 3 || map[i, j] == 8)//子弹撞击
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(i * Griwidth, j * Griheight, Griwidth, Griheight);
            }
            else
            {
                return new Rectangle(0, 0, 0, 0);
            }
        }
        /*
         * 画墙
         */ 
        public void draw(Graphics g){
            for (int i = 0; i < Griy; i++)
            {
                for (int j = 0; j < Grix; j++)
                {
                    if (map[i, j] != 0)
                    {
                        g.DrawImage(img[map[i,j]],i*Griheight,j*Griwidth ,Griheight,Griwidth);
                    }
                }
            }
        }
    }
}
