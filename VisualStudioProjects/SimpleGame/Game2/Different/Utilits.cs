using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Game2.Different
{
    static class Utilits
    {
        public static double GetLength(Point centerTower, Point centerEnimy)
        {
            double d = Math.Sqrt(Math.Pow(centerTower.X - centerEnimy.X, 2) + Math.Pow(centerTower.Y - centerEnimy.Y, 2));
            return d;
        }


        public static double[] ArrayWithMin(double [] array)
        {

            double[] array2 = new double[array.Length];

            var result = array.Min();


            for (int k = 0; k < array.Length; k++)
            {
                if (array[k] == result)
                {
                    array2[k] = result;
                }
                else {
                    array2[k] = -1;
                }

            }

            return array2;
        }
    }
}
