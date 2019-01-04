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
    public class Gate : Figure
    {       
        
        public Gate(Point TopLeft, Field field): base(TopLeft, field)
        {
            Image = new Image();
            Bitmap = new BitmapImage(new Uri("pack://application:,,,/Images/Gate.png", UriKind.Absolute));
        }

        public Gate CreateGate( )
        {            
            return this;
        }       

        public void drawGate(Gate gate, Field field)
        {
            Image = new Image();
            Image.Source = Bitmap;
            Image.Width = field.wightCellX;
            Image.Height = field.hightCellY;
            Image.SetValue(Canvas.LeftProperty, gate.LeftTop.X);
            Image.SetValue(Canvas.TopProperty, gate.LeftTop.Y);
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
