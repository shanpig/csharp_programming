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
        Initially, there is a Robot at position (0, 0). Given a sequence of its moves, judge if this robot makes a circle, which means it moves back to the original place.

        The move sequence is represented by a string. And each move is represent by a character. The valid robot moves are R (Right), L (Left), U (Up) and D (down). The output should be true or false representing whether the robot makes a circle.

        Example1
        Input: "UDRL"
        Output: true

        Example2
        Input: "LL"
        Output: false

        */
        public static int x, y;
        public static string command;
        public static bool origin, END;
        static void Main(string[] args)
        {
            do
            {
                initialize();
                input();
                move();
                judge();
                output();
                endProgram();
            } while (END);
        }
        public static void initialize()
        {
            x = y = 0;
            END = true;
        }
        public static void input()
        {
            Console.WriteLine("Input movement by typing combinations of U(up)D(down)L(left)R(right)");
            command = Console.ReadLine();
        }
        public static void move()
        {
            for (int i = 0; i < command.Length; i++)
            {
                if (command[i] == 'U')
                    y++;
                else if (command[i] == 'D')
                    y--;
                else if (command[i] == 'L')
                    x--;
                else if (command[i] == 'R')
                    x++;
                else
                    Console.WriteLine("Error, '" + command[i] + "' is not a command.");
            }
        }
        public static void judge()
        {
            if (x == 0 && y == 0)
                origin = true;
            else
                origin = false;
        }
        public static void output()
        {
            Console.WriteLine("Coordinate of the robot : (" + x + ", " + y + ")\n" + "Does it go back to the origin ? " + origin);
        }
        public static void endProgram()
        {
            string end;
            while (true)
            {
                Console.WriteLine("Do you want to retry?");
                end = Console.ReadLine();
                if (end == "N")
                {
                    END = false;
                    break;
                }
                else if (end == "Y")
                    break;
                else
                    Console.WriteLine("Please reply Y or N.");

            }


        }
    }
}
