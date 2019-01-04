using Game2.BattleFieldField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Game2.Figures
{
    public class Figure
    {
        public string typeFigure;

        public Point LeftTop;
        public Point RightTop;
        public Point LeftBottom;
        public Point RightBottom;

        public Point Center;

        public Image Image;
        public BitmapImage Bitmap;

        public Field field;

        public Figure(Point LeftTop, Field field)
        {
            this.LeftTop = LeftTop;

            RightTop.X = LeftTop.X + field.wightCellX;
            RightTop.Y = LeftTop.Y;

            LeftBottom.X = LeftTop.X;
            LeftBottom.Y = LeftTop.Y + field.hightCellY;

            RightBottom.X = LeftTop.X + field.wightCellX;
            RightBottom.Y = LeftTop.Y + field.hightCellY;

            Center.X = (RightTop.X - LeftTop.X) / 2 + LeftTop.X;
            Center.Y = (RightBottom.Y - RightTop.Y) / 2 + RightTop.Y;

            this.field = field;
        }       
    }
}
