using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        /*
        The Hamming distance between two integers is the number of positions at which the corresponding bits are different.
        Given two integers x and y, calculate the Hamming distance.
        
        Input: x = 1, y = 4
        Output: 2
        Note: 0 ≦ x, y ≦ 2^31;

        Explanation:
        1   (0 0 0 1)
        4   (0 1 0 0)
               ↑   ↑

        The above arrows point to positions where the corresponding bits are different.
        */
        public static int X, Y, hammingCount=0;
        public static string x, y, binX, binY, endOrNot;
        public static bool boolX, boolY, end=false;
        static void Main(string[] args)
        {
            do
            {
                input();
                hamming();
                output();
                Console.WriteLine("retry? (Y//N)");
                endOrNot = Console.ReadLine();
                if (endOrNot == "Y")
                {
                    end = false;
                }
                else if (endOrNot == "N")
                {
                    end = true;
                }

            } while (!end);
        }

        public static void input()
        {
            boolX = boolY = false;//初始化
            while (!boolX || !boolY)
            {
                Console.WriteLine("Input:");
                x = Console.ReadLine();//輸入值X
                y = Console.ReadLine();//輸入值Y
                boolX = int.TryParse(x, out X);//決定是否為數字或overflow
                boolY = int.TryParse(y, out Y);
                if (!boolX || !boolY)//錯誤訊息
                {
                    Console.WriteLine("At least one of your inputs is not a number or out of range.");
                }

            }

        }
        public static void hamming()
        {
            binX = Convert.ToString(X, 2).PadLeft(32, '0');//將數值換成二進位
            binY = Convert.ToString(Y, 2).PadLeft(32, '0');
            for (int i = 0; i < 32; i++)//檢查不同之處
            {
                if (binX[i] != binY[i])
                {
                    hammingCount++;
                }
            }
        }
        public static void output()
        {
            Console.WriteLine("Output:\nX=" + binX + "\nY=" + binY + "\nhamming=" + hammingCount);
        }
    }
}
