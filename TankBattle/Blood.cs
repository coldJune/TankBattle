using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    class Blood
    {
        int x, y, w, h;//血块四顶点坐标
        int step = 0;
        private Boolean live = true;//血块是否存在

        public void setLive(Boolean live)
        {
            this.live = live;
        }
        public Boolean isLive()
        {
            return live;
        }
        //血块出现的位置坐标
        private int[,] position ={{50,50},{500,50},{50,500},{300,300}};
        /*
         * 初始化
         */ 
       public  Blood()
        {
            x = position[0,0];
            y = position[0,1];
            w = h = 15;
        }
        /*
         *拿到图片
         */
       Image image = Properties.Resources.心;

        /*
         * 画出血块
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
            if (step == position.GetLength(1))
            {
                step = 0;
            }
            x = position[step, 0];
            y = position[step, 1];
        }
        //得到包裹血块的矩形
        public Rectangle getRect()
        {
            return new Rectangle(x, y, w, h);
        }
    }
}
