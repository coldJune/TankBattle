using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    class Explode
    {
        int x, y;//爆炸的位置
        private Boolean live = true;//产生到消失
        private TankCilent tc;

        /*
        *拿到图片
        */
        Image[] image = { Properties.Resources._0,
                          Properties.Resources._1, 
                          Properties.Resources._2, 
                          Properties.Resources._3, 
                          Properties.Resources._4, 
                          Properties.Resources._5, 
                          Properties.Resources._6,
                          Properties.Resources._7, 
                          Properties.Resources._8,
                          Properties.Resources._9,
                          Properties.Resources._10,
                        };
        int step = 0;

        private static Boolean init = false;//初始化爆炸图片
       public  Explode(int x,int y,TankCilent tc) {
           this.x = x;
           this.y = y;
           this.tc = tc;
        }

        public void draw(Graphics g)
        {
            if (!init)
            {
                foreach (Image im in image)
                {
                    g.DrawImage(im,-100,-100);
                }
                init = true;
            }
            if (!live)
            {
                TankCilent.explodes.Remove(this);
                return;
            }
            if (step == image.Length)
            {
                live = false;
                step = 0;
                return;
            }
            g.DrawImage(image[step], x, y);
            step++;
        }
    }
}
