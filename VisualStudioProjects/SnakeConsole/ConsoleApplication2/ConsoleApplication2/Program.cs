using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _001
{
    class Program
    {



        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.SetWindowSize(100, 50);

            Console.Title = "Game";

            Console.Clear();

            Console.CursorVisible = false;

            int iCount = 0;

            int up = 0;
            int down = 1;
            int left = 2;
            int right = 3;
            int forward = right;

            List<int> xWall = new List<int>();
            List<int> yWall = new List<int>();


            for (int i = 10; i < 51; i++)
            {

                xWall.Add(i);
                Console.CursorLeft = i;
                Console.CursorTop = 10;
                Console.Write("▒");

                Console.CursorLeft = i;
                Console.CursorTop = 40;
                Console.Write("▒");
            }

            for (int i = 10; i < 41; i++)
            {

                yWall.Add(i);
                Console.CursorLeft = 10;
                Console.CursorTop = i;
                Console.Write("▒");

                Console.CursorLeft = 50;
                Console.CursorTop = i;
                Console.Write("▒");
            }



            List<int> xAppleList = new List<int>();
            List<int> yAppleList = new List<int>();






            Random rAppleX = new Random();

            for (int i = 0; i < 10; i++)



            {


                int xApple = rAppleX.Next(11, 49);

                xAppleList.Add(xApple);

                int yApple = rAppleX.Next(11, 39);

                yAppleList.Add(yApple);

                Console.CursorLeft = xApple;
                Console.CursorTop = yApple;

                Console.Write('0');



            }





            int xCount = 4;
            int yCount = 1;
            Console.CursorLeft = xCount;
            Console.CursorTop = yCount;
            Console.Write("count =");

            int xResultCount = 12;
            int yResultCount = 1;
            Console.CursorLeft = xResultCount;
            Console.CursorTop = yResultCount;
            Console.Write("0");


            int x = 25;
            int y = 25;

            List<int> xSnake = new List<int>();
            List<int> ySnake = new List<int>();

            //----------------------— 
            Random r = new Random();

            int countApple = 0;

            ConsoleKey key = 0;

            while (true)
            {


                System.Threading.Thread.Sleep(500 - countApple * 40);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    key = keyInfo.Key;



                }





                //if (key.Key == null) 

                //{ 


                //} 


                if (key != 0)
                {


                    xSnake.Add(x);
                    ySnake.Add(y);



                    int xIndexDeliteSnake = xSnake.Count() - 1 - countApple;
                    int yIndexDeliteSnake = ySnake.Count() - 1 - countApple;




                    Console.CursorLeft = xSnake[xIndexDeliteSnake];
                    Console.CursorTop = ySnake[yIndexDeliteSnake];
                    Console.Write(" ");



                    if (key == ConsoleKey.UpArrow)

                    {
                        forward = up;
                        y--;
                    }


                    if (key == ConsoleKey.DownArrow)

                    {
                        y++;
                        forward = down;
                    }

                    if (key == ConsoleKey.RightArrow)
                    {
                        x++;

                        forward = right;
                    }

                    if (key == ConsoleKey.LeftArrow)
                    {
                        x--;

                        forward = left;
                    }



                    Console.CursorLeft = x;

                    Console.CursorTop = y;

                    Console.Write("X");


                    int g = 0;
                    for (int i = 0; i <= xAppleList.Count - 1; i++)
                        if (xAppleList[i] == x && yAppleList[i] == y)
                        {

                            countApple++;
                            int xResult = 12;
                            int yResult = 1;
                            Console.CursorLeft = xResult;
                            Console.CursorTop = yResult;
                            Console.Write(countApple);

                            xAppleList.RemoveAt(i);
                            yAppleList.RemoveAt(i);

                        }


                    for (int i = 10; i < 51; i++)
                    {

                        Console.CursorLeft = i;
                        Console.CursorTop = 10;

                        if (Console.CursorLeft == x && Console.CursorTop == y)

                        {
                            Console.CursorLeft = 25;
                            Console.CursorTop = 25;
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.Write("Game over");
                            System.Threading.Thread.Sleep(5000000);

                        }


                        Console.CursorLeft = i;
                        Console.CursorTop = 40;

                        if (Console.CursorLeft == x && Console.CursorTop == y)


                        {
                            Console.CursorLeft = 25;
                            Console.CursorTop = 25;
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.Write("Game over");
                            System.Threading.Thread.Sleep(5000000);

                        }



                    }

                    for (int i = 10; i < 41; i++)
                    {


                        Console.CursorLeft = 10;
                        Console.CursorTop = i;

                        if (Console.CursorLeft == x && Console.CursorTop == y)

                        {
                            Console.CursorLeft = 25;
                            Console.CursorTop = 25;
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.Write("Game over");
                            System.Threading.Thread.Sleep(5000000);

                        }


                        Console.CursorLeft = 50;
                        Console.CursorTop = i;



                        if (Console.CursorLeft == x && Console.CursorTop == y)

                        {
                            Console.CursorLeft = 25;
                            Console.CursorTop = 25;
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.Write("Game over");
                            System.Threading.Thread.Sleep(5000000);

                        }


                    }





                    int x2IndexDeliteSnake = xSnake.Count() - 1 - countApple;
                    int y2IndexDeliteSnake = ySnake.Count() - 1 - countApple;

                    for (int i = x2IndexDeliteSnake; i < xSnake.Count() - 1; i++)
                    {


                        if (xSnake[i] == x && ySnake[i] == y)

                        {
                            Console.CursorLeft = 25;
                            Console.CursorTop = 25;
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.Write("Game over");
                            System.Threading.Thread.Sleep(5000000);

                        }


                    }



                    if (countApple == 10)
                    {

                        Console.CursorLeft = 25;
                        Console.CursorTop = 25;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("You win");
                        System.Threading.Thread.Sleep(5000000);



                    }



                }


            }

            Console.CursorLeft = 10;
            Console.CursorTop = 20;

            Console.ReadLine();

        }




    }
}