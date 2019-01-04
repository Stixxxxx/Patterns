using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Game2.BattleFieldField;

namespace Game2.Figures
{
    public class Tower : Figure
    {
        Point BeginShot;
        Point EndShot;
        Bullet bullet;
        int countBullet = 5;
        public List<Bullet> Bullets = new List<Bullet>();
        long millisecondsFromCreatingTower = 0;
        public long millisecondsDrawAllEnimies = 0;


        public Tower(Point LeftTop, Field field) : base(LeftTop, field)
        {
            Image = new Image();
            Bitmap = new BitmapImage(new Uri("pack://application:,,,/Images/Tower.png", UriKind.Absolute));

            for (int i = 0; i<countBullet; i++) {

                bullet = new Bullet(Center, field);
                Bullets.Add(bullet);
            }
            millisecondsFromCreatingTower = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        }

        public Tower CreateGate()
        {
            return this;
        }

    
        public void Shoting(Point moveTo)
        {
            

            Bullets[0].MoveBullet(moveTo);

            if (millisecondsDrawAllEnimies != 0 && System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond - millisecondsDrawAllEnimies > 5000)
            {
                Bullets[1].MoveBullet(moveTo);
            }

            if (millisecondsDrawAllEnimies != 0 && System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond - millisecondsDrawAllEnimies > 10000)
            {
                Bullets[2].MoveBullet(moveTo);
            }

            if (millisecondsDrawAllEnimies != 0 && System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond - millisecondsDrawAllEnimies > 15000)
            {
                Bullets[3].MoveBullet(moveTo);
            }

            if (millisecondsDrawAllEnimies != 0 && System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond - millisecondsDrawAllEnimies > 20000)
            {
                Bullets[4].MoveBullet(moveTo);
            }

        }


        public bool InCell(Point Position)
        {
            if (Position.X > LeftTop.X && Position.X < RightTop.X && Position.Y > LeftTop.Y && Position.Y < RightBottom.Y)
            {
                return true;
            }

            return false;
        }

      
        }
}
