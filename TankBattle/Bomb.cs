using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    class Bomb
    {
         int x, y, w, h;//炸弹四顶点坐标
        int step = 0;
        private Boolean live = true;//炸弹是否存在

        public void setLive(Boolean live)
        {
            this.live = live;
        }
        public Boolean isLive()
        {
            return live;
        }
        //炸弹出现的位置坐标
        private int[,] position ={{200,200}};
        /*
         * 初始化
         */ 
       public  Bomb()
        {
            x = position[0,0];
            y = position[0,1];
            w = h = 15;
        }
        /*
         *拿到图片
         */
        Image image = Properties.Resources.炸弹;

        /*
         * 画出炸弹
         */
        public void draw(Graphics g)
        {
            if (!live) return;
            g.DrawImage(image, x, y);
            move();
        }
        void move()
        {
            step++;
            int le = position.Length;
            if (step == position.Length-1)
            {
                step = 0;
            }
            x = position[step, 0];
            y = position[step, 1];
        }
        //得到包裹炸弹的矩形
        public Rectangle getRect()
        {
            return new Rectangle(x, y, w, h);
        }
    }
}
