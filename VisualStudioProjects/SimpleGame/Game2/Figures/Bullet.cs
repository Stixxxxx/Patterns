using Game2.BattleFieldField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game2.Figures
{
    public class Bullet
    {
        public Point Center;
        public Field field;
        public Point nextPoint;

        double startX;
        double startY;

        double destinationX;
        double destinationY;

        double currentX;
        double currentY;

        double lengthTraser;

        double speedBullet = 1;

        public Bullet(Point Center, Field field)
        {
            this.Center = Center;
            this.field = field;
            this.nextPoint = this.Center;

            if (startX == 0)
            {
                startX = Center.X;
                startY = Center.Y;
            }

            currentX = Center.X;
            currentY = Center.Y;
        }

        public void MoveBullet(Point moveTo)
        {

            destinationX = moveTo.X;
            destinationY = moveTo.Y;

            lengthTraser = Math.Sqrt(Math.Pow(destinationX - startX, 2) + Math.Pow(destinationY - startY, 2));


            Ellipse circle = new Ellipse()
            {
                Width = 6,
                Height = 6,
                Stroke = Brushes.Black,
                StrokeThickness = 6
            };


            field.CanvasApp.Children.Add(circle);

            circle.SetValue(Canvas.LeftProperty, currentX);
            circle.SetValue(Canvas.TopProperty, currentY);

            //if (destinationX - startX == 0)

            //currentY = ((((currentX - startX) / (destinationX - startX)) * (destinationY - startY)) + startY);
            currentY += (destinationY - startY) / lengthTraser * speedBullet;
            currentX += (destinationX - startX) / lengthTraser * speedBullet;

            Center.Y = currentY;
            Center.X = currentX;


        }

    }
}
