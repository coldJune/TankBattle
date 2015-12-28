using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    class Tank
    {
        /*
         *定义坦克速度 
          */
    
        public const int XSPEED = 7;
        public const int YSPEED = 7;
        
        /*
         * 定义坦克宽度和高度
         */
        public const int WIDTH = 40;
        public const int HEIGHT = 40;
        /*
         * 定义关于坦克存活
         */
        public Boolean live = true;
        private int life = 100;

        public static int bombNumber = 3;//爆炸次数
        public int restartChance = 1;//重新开始次数

        public void setLife(int life)
        {
             this.life = life;
        }

        public int getLife()
        {
            return this.life;
        }

        public void setLive(Boolean live)
        {
            this.live = live;
        }

        public Boolean isLive()
        {
            return this.live;
        }

        TankCilent tc;
        private Boolean ownTank;//创建一个己方坦克

        public Boolean isOwnTank(){
            return ownTank;
        }
        private   int x, y;//当前位置
        private   int oldX, oldY;//上一个位置

        private static Random r = new Random();//创建一个随机数

        /*
         * 记录按键的状态
         */
        private Boolean bL = false;//左
        private Boolean bR = false;//右
        private Boolean bU = false;//上
        private Boolean bD = false;//下

        private Direction dir = Direction.STOP;//方向默认停止
        private Direction barrelDir = Direction.U;//炮筒默认向上
        private int step = r.Next(12)+3;//随机移动步数3~15；
        /*
         *装载图片
         */ 
       private static   Dictionary<String,Image>  imags = new Dictionary<string,Image>();
        static Tank(){
             Image[] tanks = new Image[8];
             
            
            tanks[0] = Properties.Resources.tankL1;
            tanks[1] = Properties.Resources.tankU1;
            tanks[2] = Properties.Resources.tankR1;
            tanks[3] = Properties.Resources.tankD1;
            tanks[4] = Properties.Resources.tankL2;
            tanks[5] = Properties.Resources.tankU2;
            tanks[6] = Properties.Resources.tankR2;
            tanks[7] = Properties.Resources.tankD2;
          
            imags.Add("L1",tanks[0]);
             imags.Add("U1",tanks[1]);
             imags.Add("R1",tanks[2]);
             imags.Add("D1",tanks[3]);
             imags.Add("L2",tanks[4]);
             imags.Add("U2",tanks[5]);
             imags.Add("R2",tanks[6]);
             imags.Add("D2",tanks[7]);
         }   
        Tank(int x,int y, Boolean ownTank){
            this.x = x;
            this.y = y;
            this.ownTank = ownTank;
        }
       public  Tank(int x,int y,Boolean ownTank,Direction dir,TankCilent tc)
            :this(x,y,ownTank){
           this.dir=dir;
            this.tc=tc;
        }
       Tank() { }
        /*
         * 画坦克
         */ 
        public void draw(Graphics g){
            if(!live){
                if(!ownTank){
                    TankCilent.tanks.Remove(this);
                }
                return;
            }
            if(ownTank){
               drawLine(g);
            }
            move();
            if (ownTank)
            {
                switch (barrelDir)
                {
                    case Direction.L:
                        g.DrawImage(imags["L1"],x,y);
                        break;
                    case Direction.U:
                        g.DrawImage(imags["U1"],x,y);
                        break;
                    case Direction.R:
                        g.DrawImage(imags["R1"],x,y);
                        break;
                    case Direction.D:
                        g.DrawImage(imags["D1"],x,y);
                        break;
                }
            }else{
                switch(barrelDir){
                     case Direction.L:
                        g.DrawImage(imags["L2"],x,y);
                        break;
                    case Direction.U:
                        g.DrawImage(imags["U2"],x,y);
                        break;
                    case Direction.R:
                        g.DrawImage(imags["R2"],x,y);
                        break;
                    case Direction.D:
                        g.DrawImage(imags["D2"],x,y);
                        break;
                }
            }
            
        }

         Direction[] dirs = new Direction[]{Direction.L,Direction.U,Direction.R,Direction.D,Direction.STOP};//将方向转换为一个数组
        /*
         * 移动的函数定义
         */ 
        private void move(){
            this.oldX=x;
            this.oldY=y;
            switch(dir){
                case Direction.L:
                    x -= XSPEED;
                    break;
                case Direction.U:
                    y -= YSPEED;
                    break;
                case Direction.R:
                    x += XSPEED;
                    break;
                case Direction.D:
                    y += YSPEED;
                    break;
                case Direction.STOP:
                    break;

            }
            //坦克方向和炮筒方向一致
            if(this.dir!=Direction.STOP){
                this.barrelDir=this.dir;
            }
            /*
             * 坦克出界处理
             */
            if(x<0)x=0;
            if(y<25)y=25;
            if (x + Tank.WIDTH >TankCilent.GAME_WIDTH - 200) x = TankCilent.GAME_WIDTH - Tank.WIDTH - 200;
            if (y + Tank.HEIGHT > TankCilent.GAME_HEIGHT-40) y = TankCilent.GAME_HEIGHT - Tank.HEIGHT-40;
            
            if(!ownTank){
               
                if (step == 0)//处理坦克撞墙后不动
                {
                    step =r.Next(12) + 3;//step减少到0则生成一个新的数
                    int rn = r.Next(dirs.Length);
                    dir = dirs[rn];//改变方向
                }
                step--;//移动一次可移动步数减一，直到为0；

                if (r.Next(40) > 32) this.fire();//生成随机数来指示开火时间
            }
        }
        /*
         * 处理按键,fang
         */
        public void KeyPressed(KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            switch (key)
            {
                case Keys.F2:
                    if (!this.live)
                    {
                        if (restartChance > 0)
                        {
                            this.live = true;
                            this.life = 100;
                            restartChance--;
                        }
                    }
                    break;
                case Keys.J:
                    bL = true;
                    break;
                case Keys.I:
                    bU = true;
                    break;
                case Keys.L:
                    bR=true;
                    break;
                case Keys.K:
                    bD=true;
                    break;
            }
            localDirection();
        }

        public void KeyReleased(KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            switch (key)
            {
                case Keys.Space:
                    fire();
                    break;
                case Keys.A:
                    if (bombNumber > 0)
                    {
                        superFire();
                        bombNumber--;
                    }
                    break;
                case Keys.J:
                    bL = false;
                    break;
                case Keys.K:
                    bD = false;
                    break;
                case Keys.I:
                    bU = false;
                    break;
                case Keys.L:
                    bR = false;
                    break;
            }
            localDirection();
        }

        /*
         * 停止，回到上一步
         */ 
        private void stay(){
            x=oldX;
            y=oldY;
        }
        /*
         * 定义坦克方向
         */ 
        void localDirection(){
            if(bL&&!bU&&!bR&&!bD)dir=Direction.L;
            else if(!bL&&bU&&!bR&&!bD)dir = Direction.U;
            else if(!bL&&!bU&&bR&&!bD)dir = Direction.R;
            else if(!bL&&!bU&&!bR&&bD)dir = Direction.D;
            else if(!bL&&!bU&&!bR&&!bD)dir = Direction.STOP;
        }
        /*
         * 开火
         */ 
        public Missile fire(){
            if(!live)return null;//死了不能开火
            int x=this.x+Tank.WIDTH/2-Missile.WIDTH/2;
            int y=this.y+Tank.HEIGHT/2-Missile.HEIGHT/2;

            Missile m = new Missile(x,y,ownTank,barrelDir,this.tc);
            TankCilent.missile.Add(m);
            return m;
        }
        public Missile fire(Direction dir){
            if(!live)return null;
            int x= this.x+Tank.WIDTH/2-Missile.WIDTH/2;
            int y=this.y+Tank.HEIGHT/2-Missile.HEIGHT/2;
            Missile m= new Missile(x,y,ownTank,dir,tc);
            TankCilent.missile.Add(m);
            return m;
        }
        
        private void superFire(){
            for(int i=0;i<4;i++){
                for(int j=0;j<4;j++){
                    fire(dirs[i]);
                }
            }
        }
        //拿到包裹坦克的矩形
        public  Rectangle getRect(){
            return new Rectangle(x,y,WIDTH,HEIGHT);
        }
        
        //坦克撞墙
        public void collodesWithWall(Wall w){
            for(int i=0;i<Wall.getGrix();i++){
                for(int j=0;j<Wall.getGriy();j++){
                    if(this.live&&w.isLive()&&this.getRect().IntersectsWith(w.getRect(i,j,1))){
                        this.stay();
                    }
                }
            }
        }
        
        //坦克撞坦克
        public Boolean collidesWithTanks(List<Tank> tanks){
            for(int i=0;i<tanks.Count();i++){
                Tank t = tanks[i];
                if(this!=t){
                    if(this.live&&t.isLive()&&this.getRect().IntersectsWith(t.getRect())){
                        this.stay();
                        t.stay();
                        return true;
                    }
                }
            }
            return false;
        }

        public static Boolean collidesWithTanks(List<Tank> tanks,int x,int y){//x,y为坦克出现的位置
            for(int i=0;i<tanks.Count();i++){
                Tank p = new Tank(x,y,true);
                Tank t = tanks[i];
                if(t.isLive()&&p.getRect().IntersectsWith(t.getRect())){
                    p.setLive(false);
                    return true;
                }
            }
            return false;
        }



          /*
         *血条 
         */
   
            public void drawLine(Graphics g)
            {
                SolidBrush sb = new SolidBrush(Color.Red);
                g.DrawRectangle(new Pen(sb),this.x,this.y-10,WIDTH,10);//画在坦克上方
                int w = WIDTH * this.life / 100;
                g.FillRectangle(sb,this.x,this.y-10,w,10);
            }
        
        /*
         * 吃血块
         */ 
        public Boolean eat(Blood blood){
            if(this.live&&blood.isLive()&&this.getRect().IntersectsWith(blood.getRect())){
                this.life=100;
                blood.setLive(false);
                return true;
            }
            return false;
        }

        public Boolean eat(Bomb bomb){
            if(this.live&&bomb.isLive()&&this.getRect().IntersectsWith(bomb.getRect())){
                bombNumber++;
                bomb.setLive(false);
                return true;
            }
            return false;
        }
    }



      

    
}
