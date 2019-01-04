using Game2.BattleField;
using Game2.Different;
using Game2.Figures;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Game2.BattleFieldField
{
    public class Field
    {
        public Canvas CanvasApp;
        public StackPanel StackPanelTowers;

        public int amountCells;

        public double hightCellY;
        public double wightCellX;

        public Cell[,] cells;
        public List<Gate> gates;
        public List<Tower> towers;
        public Enimy[] enimies;
        public int avilableTowers = 3;

        public double xCoordinateBegin = 0.000;
        public double yCoordinateBegin = 0.000;

        double xCoordinateCurrent;
        double yCoordinateCurrent;

        //time
        public long millisecondsDrawTowers;
        public long millisecondsDrawEnimis = -1;

        Point bufferEnimiesCenter;
        bool enimyZero = false;

        public Field(Canvas CanvasApp, int amountCells, StackPanel StackPanelTowers)
        {
            this.CanvasApp = CanvasApp;
            this.amountCells = amountCells;

            hightCellY = CanvasApp.ActualHeight / amountCells;
            wightCellX = CanvasApp.ActualWidth / amountCells;

            cells = new Cell[amountCells, amountCells];
            gates = new List<Gate>();
            towers = new List<Tower>();
            enimies = new Enimy[3];
            this.StackPanelTowers = StackPanelTowers;

        }

        public void AddCells()
        {
            xCoordinateCurrent = xCoordinateBegin;
            yCoordinateCurrent = yCoordinateBegin;

            for (int y = 0; y <= 9; y++)
            {
                for (int x = 0; x <= 9; x++)
                {
                    cells[x, y] = new Cell(
                        new Point
                        {
                            X = xCoordinateCurrent,
                            Y = yCoordinateCurrent
                        },
                        new Point
                        {
                            X = xCoordinateCurrent + wightCellX,
                            Y = yCoordinateCurrent
                        },
                        new Point
                        {
                            X = xCoordinateCurrent,
                            Y = yCoordinateCurrent + hightCellY
                        },
                        new Point
                        {
                            X = xCoordinateCurrent + wightCellX,
                            Y = yCoordinateCurrent + hightCellY
                        });


                    xCoordinateCurrent += wightCellX;
                }

                xCoordinateCurrent = xCoordinateBegin;
                yCoordinateCurrent += hightCellY;
            }

        }



        public void DrawCells()
        {


            for (int y = 0; y <= 9; y++)
            {
                for (int x = 0; x <= 9; x++)
                {
                    cells[x, y].drawCell(CanvasApp);
                }
            }

        }

        public void AddGate()
        {

            Gate gate = new Gate(cells[0, 0].LeftTop, this);
            gates.Add(gate);

            Gate gate2 = new Gate(cells[9, 9].LeftTop, this);
            gates.Add(gate2);
        }

        public void DrawGates()
        {

            for (int i = 0; i < gates.Count; i++)
            {
                Image Image = new Image();
                Image.Source = gates[i].Bitmap;
                Image.Width = this.wightCellX;
                Image.Height = this.hightCellY;
                Image.SetValue(Canvas.LeftProperty, gates[i].LeftTop.X);
                Image.SetValue(Canvas.TopProperty, gates[i].LeftTop.Y);
                CanvasApp.Children.Add(Image);
            }
        }


        public void addTower(Point WindowPosition)
        {

            Tower tower;
            //todo Переделать проверки, проверять весь список на пересечение
            for (int y = 0; y <= amountCells - 1; y++)
            {
                for (int x = 0; x <= amountCells - 1; x++)
                {
                    //проверка в какой ячейке
                    bool result = cells[x, y].InCell(WindowPosition);
                    if (result)
                    {

                        bool result2 = inCellAnyEntry(gates, WindowPosition);

                        if (!result2)
                        {
                            bool result3 = inCellAnyEntry(towers, WindowPosition);

                            if (!result3)
                            {
                                if (avilableTowers != 0)
                                {
                                    //рисуем башню в выбранной ячейке
                                    tower = new Tower(cells[x, y].LeftTop, this);
                                    towers.Add(tower);
                                    avilableTowers--;
                                    if (avilableTowers == 0)
                                        millisecondsDrawTowers = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
                                }
                            }

                        }

                    }

                }

            }

        }

        public void DrawTowers()
        {

            for (int i = 0; i < towers.Count; i++)
            {
                Image Image = new Image();
                Image.Source = towers[i].Bitmap;
                Image.Width = this.wightCellX;
                Image.Height = this.hightCellY;
                Image.SetValue(Canvas.LeftProperty, towers[i].LeftTop.X);
                Image.SetValue(Canvas.TopProperty, towers[i].LeftTop.Y);
                CanvasApp.Children.Add(Image);

            }
        }



        public void DrawTowersStackPanel()
        {
            for (int y = 0; y <= avilableTowers - 1; y++)
            {
                Tower tower = new Tower(new Point { X = 0, Y = 0 }, this);

                Image Image = new Image();
                Image.Source = tower.Bitmap;
                Image.Width = this.wightCellX;
                Image.Height = this.hightCellY;
                StackPanelTowers.Children.Add(Image);
            }

        }

        public void AddEnimy()
        {

            Enimy enimy = new Enimy(cells[0, 1].LeftTop, this);
            enimies[0] = enimy;

            Enimy enimy2 = new Enimy(cells[1, 1].LeftTop, this);
            enimies[1] = enimy2;

            Enimy enimy3 = new Enimy(cells[1, 0].LeftTop, this);
            enimies[2] = enimy3;
        }

        public void drawEnimies()
        {

            for (int i = 0; i < enimies.Length; i++)
            {
                if (enimies[i] != null)
                {
                    Image Image = new Image();
                    Image.Source = enimies[i].Bitmap;
                    Image.Width = this.wightCellX;
                    Image.Height = this.hightCellY;
                    Image.SetValue(Canvas.LeftProperty, enimies[i].LeftTop.X);
                    Image.SetValue(Canvas.TopProperty, enimies[i].LeftTop.Y);
                    CanvasApp.Children.Add(Image);

                    if (enimies.Length == i + 1 && millisecondsDrawEnimis == -1)
                        millisecondsDrawEnimis = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;

                }

            }


        }

        public void drawEnimiesMove()
        {
            if (enimies[0] != null)
            {
                enimies[0].LeftTop.X += 1;
                enimies[0].LeftTop.Y += 1;
                enimies[0].Center.X += 1;
                enimies[0].Center.Y += 1;
                enimies[0].RightTop.X += 1;
                enimies[0].RightTop.Y += 1;
                enimies[0].RightBottom.X += 1;
                enimies[0].RightBottom.Y += 1;
                enimies[0].LeftBottom.X += 1;
                enimies[0].LeftBottom.Y += 1;
            }


            if (enimies[1] != null)
            {
                enimies[1].LeftTop.X += 1.200;
                enimies[1].LeftTop.Y += 1.200;
                enimies[1].Center.X += 1.200;
                enimies[1].Center.Y += 1.200;
                enimies[1].RightTop.X += 1.200;
                enimies[1].RightTop.Y += 1.200;
                enimies[1].RightBottom.X += 1.200;
                enimies[1].RightBottom.Y += 1.200;
                enimies[1].LeftBottom.X += 1.200;
                enimies[1].LeftBottom.Y += 1.200;
            }

            if (enimies[2] != null)
            {
                enimies[2].LeftTop.X += 1.100;
                enimies[2].LeftTop.Y += 1.100;
                enimies[2].Center.X += 1.100;
                enimies[2].Center.Y += 1.100;
                enimies[2].RightTop.X += 1.100;
                enimies[2].RightTop.Y += 1.100;
                enimies[2].RightBottom.X += 1.100;
                enimies[2].RightBottom.Y += 1.100;
                enimies[2].LeftBottom.X += 1.100;
                enimies[2].LeftBottom.Y += 1.100;
            }




        }


        internal void Shoting()
        {
            double[][] Items = new double[towers.Count][];

            double[] Items2;

            for (int k = 0; k < towers.Count; k++)
            {

                Items2 = new double[enimies.Length];

                for (int i = 0; i < enimies.Length; i++)
                {
                    if (enimies[i] != null)
                    {
                        var result = Utilits.GetLength(towers[k].Center, enimies[i].Center);
                        Items2[i] = result;
                    }
                    else
                    {
                        Items2[i] = 9999999999999999999;
                    }
                }
                Items[k] = Items2;
            }


            for (int k = 0; k < towers.Count; k++)
            {

                Items[k] = Utilits.ArrayWithMin(Items[k]);
            }


            for (int k = 0; k < towers.Count; k++)
            {
                for (int i = 0; i < enimies.Length; i++)
                {
                    if (Items[k][i] != -1)
                    {

                        if (enimies[i] != null)
                        {
                            towers[k].Shoting(enimies[i].Center);
                            bufferEnimiesCenter = enimies[i].Center;
                        }
                        else
                        {
                            enimyZero = true;
                        }

                    }

                }
                if (enimyZero == true)
                towers[k].Shoting(bufferEnimiesCenter);
            }
        }


        private bool inCellAnyEntry(List<Gate> gates, Point WindowPosition)
        {

            bool result = false;
            for (int y = 0; y <= gates.Count - 1; y++)
            {

                result = gates[y].InCell(WindowPosition);
                if (result) break;

            }

            return result;
        }

        private bool inCellAnyEntry(List<Tower> tower, Point WindowPosition)
        {
            bool result = false; ;

            for (int y = 0; y <= tower.Count - 1; y++)
            {
                result = tower[y].InCell(WindowPosition);
                if (result) break;
            }

            return result;
        }
    }
}
