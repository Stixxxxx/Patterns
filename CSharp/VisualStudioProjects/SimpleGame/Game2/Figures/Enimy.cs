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
    public class Enimy : Figure
    {
        public          int Life;

        public Enimy(Point LeftTop, Field field) : base(LeftTop, field)
        {
            Life = 3;
            Image = new Image();
            Bitmap = new BitmapImage(new Uri("pack://application:,,,/Images/Evil.png", UriKind.Absolute));
        }

        public Enimy CreateGate()
        {
            return this;
        }

        public void DrawEnimyCanvas(Enimy enimy, Field field)
        {
            Image = new Image();
            Image.Source = Bitmap;
            Image.Width = field.wightCellX;
            Image.Height = field.hightCellY;
            Image.SetValue(Canvas.LeftProperty, enimy.LeftTop.X);
            Image.SetValue(Canvas.TopProperty, enimy.LeftTop.Y);
            field.CanvasApp.Children.Add(Image);
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
