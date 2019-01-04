using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Game2.BattleField
{
   public class Cell
    {   public Point LeftTop;
        public Point RightTop;
        public Point LeftBottom;
        public Point RightBottom;
        public Point Center;

        double opticalyLine = 0.050;

        public Cell(Point leftTop, Point rightTop, Point leftBottom, Point rightBottom)
        {
            LeftTop = leftTop;
            RightTop = rightTop;
            LeftBottom = leftBottom;
            RightBottom = rightBottom;
            Center = new Point();
            Center.X = (RightTop.X - LeftTop.X) / 2 + LeftTop.X;
            Center.Y = (LeftBottom.Y - LeftTop.Y) / 2 + LeftTop.Y;
        }

        public void drawCell(Canvas CanvasApp)
        {
            Line Line = new Line();
            Line.X1 =  RightTop.X;// BeginPoint.X;
            Line.Y1 =  RightTop.Y;// BeginPoint.Y;
            Line.X2 =  RightBottom.X;
            Line.Y2 =  RightBottom.Y;
            Line.Stroke = System.Windows.Media.Brushes.Black;
            Line.StrokeThickness = 1;
            Line.HorizontalAlignment = HorizontalAlignment.Left;
            Line.VerticalAlignment = VerticalAlignment.Center;
            Line.Opacity = opticalyLine;
            CanvasApp.Children.Add(Line);

            Line = new Line();
            Line.X1 =  LeftBottom.X;// BeginPoint.X;
            Line.Y1 =  LeftBottom.Y;// BeginPoint.Y;
            Line.X2 =  RightBottom.X;
            Line.Y2 =  RightBottom.Y;
            Line.Stroke = System.Windows.Media.Brushes.Black;
            Line.StrokeThickness = 1;
            Line.HorizontalAlignment = HorizontalAlignment.Left;
            Line.VerticalAlignment = VerticalAlignment.Center;
            Line.Opacity = opticalyLine;
            CanvasApp.Children.Add(Line);

            Line = new Line();
            Line.X1 =  LeftTop.X;// BeginPoint.X;
            Line.Y1 =  LeftTop.Y;// BeginPoint.Y;
            Line.X2 =  RightTop.X;
            Line.Y2 =  RightTop.Y;
            Line.Stroke = System.Windows.Media.Brushes.Black;
            Line.StrokeThickness = 1;
            Line.HorizontalAlignment = HorizontalAlignment.Left;
            Line.VerticalAlignment = VerticalAlignment.Center;
            Line.Opacity = opticalyLine;
            CanvasApp.Children.Add(Line);

            Line = new Line();
            Line.X1 =  LeftTop.X;// BeginPoint.X;
            Line.Y1 =  LeftTop.Y;// BeginPoint.Y;
            Line.X2 =  LeftBottom.X;
            Line.Y2 =  LeftBottom.Y;
            Line.Stroke = System.Windows.Media.Brushes.Black;
            Line.StrokeThickness = 1;
            Line.HorizontalAlignment = HorizontalAlignment.Left;
            Line.VerticalAlignment = VerticalAlignment.Center;
            Line.Opacity = opticalyLine;

            CanvasApp.Children.Add(Line);
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
