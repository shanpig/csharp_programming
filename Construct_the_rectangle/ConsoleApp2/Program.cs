using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        public static int area, width, length;
        public static string inString, endMessage;
        public static bool end;
        static void Main(string[] args)
        {
            /*For a web developer, it is very important to know how to design a web page's size. So, given a specific rectangular web page’s area, your job by now is to design a rectangular web page, whose length L and width W satisfy the following requirements:

              1. The area of the rectangular web page you designed must equal to the given target area.

              2. The width W should not be larger than the length L, which means L >= W.

              3. The difference between length L and width W should be as small as possible.

              You need to output the length L and the width W of the web page you designed in sequence.*/
            do
            {
                initialize();
                input();
                calculate();
                output();
                endProgram();
            } while (!end);

        }
        public static void initialize()
        {
            area = width = length = 0;
            inString = endMessage = "";
            end = false;
        }
        public static void input()
        {
            do
            {
                Console.WriteLine("Input the total window area.");
                inString = Console.ReadLine();
                if (!judgeError(inString))
                    Console.WriteLine("Your number is out of range or not even a number. Try again.");
                else
                    break;
            } while (true);
        }
        public static bool judgeError(string input)
        {
            if (int.TryParse(input, out area))
                return true;
            else
                return false;
        }
        public static void calculate()
        {
            for (int w = 1; w < area/2; w++)
            {
                if (w > area / w)
                    break;
                if (area % w == 0)
                {
                    width = w;
                    length = area / w;
                }
            }
        }
        public static void output()
        {
            Console.WriteLine("Your calculated parameters are\nwidth=" + width + "\nlength=" + length);
        }
        public static void endProgram()
        {
            do
            {
                Console.WriteLine("Retry? (Y/N)");
                endMessage = Console.ReadLine();
                if (endMessage == "Y")
                    end = false;
                else if (endMessage == "N")
                    end = true;
            } while (true);
        }
    }
}
