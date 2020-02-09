using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        { 
            string pathway = "";
            string[] originalFile, adjustedFile;
            do
            {
                pathway = AskForInput();
                try
                {
                    originalFile = File.ReadAllLines(pathway);
                    adjustedFile = File.ReadAllLines(pathway);
                }
                catch (Exception e)
                {
                    string a = e.ToString();
                    string[] err = Regex.Split(a, @"\r");
                    Console.WriteLine("\n"+err[0]);
                    continue;
                }
                
                FindLine(adjustedFile);

                WriteLine(originalFile);

                Console.WriteLine("*******************************");

                WriteLine(adjustedFile);

            } while (Retry());
        }
        public static string AskForInput()
        {
            string pathway = "";
            do
            {
                Console.WriteLine("Please give the complete pathway to the target file, including file type.\n");
                pathway = Console.ReadLine();
                if (pathway[pathway.Length-1] == '.')
                    Console.WriteLine("Input is wrong, try again.");
                else
                    break;
            } while (true);
            
            return pathway;
        }
        public static void FindLine(string[] file)
        {
            string[] flag;
            bool write = true;
            string pattern1 = @"(\/\/)",
                   pattern2 = @"(\/\*)",
                   pattern3 = @"(\*\/)";

            for (int j = 0; j < file.Length; j++)
            {
                flag = Regex.Split(file[j], pattern1);
                file[j] = flag[0];

                if (Regex.IsMatch(file[j], pattern2))
                {
                    flag = Regex.Split(file[j], pattern2);
                    file[j] = flag[0];
                    write = false;
                }
                else if (Regex.IsMatch(file[j], pattern3))
                {
                    flag = Regex.Split(file[j], pattern3);
                    file[j] = flag[flag.Length - 1];
                    write = true;
                }
                if (!write)
                {
                    file[j] = "";
                }
            }
        }
        public static void WriteLine(string[] file)
        {
            foreach (var line in file)
            {
                if (Regex.IsMatch(line, @"\S+"))
                    Console.WriteLine(line);
                else
                    continue;
            }
        }
        public static bool Retry()
        {
            string retryM = "";
            do
            {
                Console.WriteLine("retry? Y/N");
                retryM = Console.ReadLine();
                if ("Y".Equals(retryM, StringComparison.OrdinalIgnoreCase))
                    return true;
                else if ("N".Equals(retryM, StringComparison.OrdinalIgnoreCase))
                    return false;
            } while (true);
        }
    }
}
