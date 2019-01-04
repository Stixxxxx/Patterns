using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Game2.BattleFieldField;
using Game2.Figures;

namespace Game2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int CountCells = 10;
        Field field;
        int killedEnimy = 0;
        TextBlock tbYouWin = new TextBlock();
        

        public MainWindow()
        {
            InitializeComponent();
            tbYouWin.Text = "You win!";
            tbYouWin.FontSize = 50;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            field = new Field(CanvasApp, CountCells, StackPanelTowers);
            field.AddCells();
            field.AddGate();
            field.AddEnimy();

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(100);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            Refresh();
        }

        private void Refresh()
        {
            CanvasApp.Children.Clear();
            StackPanelTowers.Children.Clear();
            StackPanelEvil.Children.Clear();

            field.DrawCells();
            field.DrawGates();
            field.DrawTowers();
            field.DrawTowersStackPanel();

            if (field.millisecondsDrawTowers != 0 && System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond - field.millisecondsDrawTowers > 1000)
            {

                field.drawEnimies();
                field.drawEnimiesMove();
            }

            if (field.millisecondsDrawTowers != 0 && System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond - field.millisecondsDrawTowers > 2000)
            {
                field.Shoting();
            }
            
            if (field.millisecondsDrawEnimis != -1)
            {
                foreach (var tower in field.towers)
                {

                    tower.millisecondsDrawAllEnimies = field.millisecondsDrawEnimis;
                }
            }

            for (int i = 0; i < field.enimies.Length; i++)
            {
                for (int k = 0; k < field.towers.Count; k++)
                {
                    for (int m = 0; m < field.towers[k].Bullets.Count; m++)
                    {
                        if (field.enimies[i] != null)
                        {
                            if (field.enimies[i].InCell(field.towers[k].Bullets[m].Center))
                            {
                                field.enimies[i]=null;
                                break;
                            }
                        }
                    }
                }
            }

            killedEnimy = 0;
            foreach ( var enimy in field.enimies)
            {
                
                var result = enimy;
                if (result == null)
                    killedEnimy++;
            }

            if (killedEnimy == field.enimies.Length)
            {
                CanvasApp.Children.Clear();
                StackPanelTowers.Children.Clear();
                StackPanelEvil.Children.Clear();                

                Canvas.SetLeft(tbYouWin, CanvasApp.ActualWidth / 2 - 25);
                Canvas.SetTop(tbYouWin, CanvasApp.ActualHeight / 2 - 25);
                CanvasApp.Children.Add(tbYouWin);
            }

        }

        private void DrawCells()
        {

        }

        private void CleanFields()
        {

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Point WindowPosition = Mouse.GetPosition(CanvasApp);
            try
            {
                field.addTower(WindowPosition);
            }
            catch (Exception c) { }



        }
    }
}
