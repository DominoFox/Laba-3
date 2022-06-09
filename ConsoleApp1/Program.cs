using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static Point GaussMin(Func<Point, double> f, Point start, double pr = 0.001, double g = 0.1)
        {

            double h = 1.9e-3;
            int pointSize = start.pointValue.Length;

            double[] newPoint = new double[pointSize];
            double u = 0.0;
            for (int k = 0; k < 1000; k++)
            {
                double stopConditionSum = 0;
                for (int i = 0; i < pointSize; i++)
                {

                    Point newStart = start;
                    newStart[i] = newStart[i] + h;
                    double I1 = f(newStart);
                    newStart[i] = newStart[i] - h;
                    double I2 = f(newStart);
                    newStart[i] = newStart[i] - h;
                    double gr = (I1 - I2) / (pointSize * h);
                    u = u - g * gr;
                    newPoint[i] = u;
                    stopConditionSum += Math.Pow(gr, 2);

                }
                if (Math.Sqrt(stopConditionSum) <= pr)
                {
                    Console.WriteLine($"Done.Number of steps: {k}");
                    break;
                }
            }
            return new Point(newPoint);
        }

        static void Main(string[] args)
        {
            var res = GaussMin(f, new Point(new double[] { 2, 2 }));
            for (int i = 0; i < res.pointValue.Length; i++)
            {
                Console.WriteLine($"{res[i]}; \n");
            }
            Console.ReadKey();
        }

        static double f(Point point)
        {
            return point[0] + point[1] - ((point[0] + point[1]) * (point[0] + point[1])) - 4 * point[1] * point[1];
        }
    }
}