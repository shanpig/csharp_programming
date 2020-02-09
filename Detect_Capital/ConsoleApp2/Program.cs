using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        public static string word, lowerWord, upperWord, normalWord, end;
        public static bool endMessage;
        static void Main(string[] args)
        {
            /*Given a word, you need to judge whether the usage of capitals in it is right or not.

              We define the usage of capitals in a word to be right when one of the following cases holds:

              All letters in this word are capitals, like "USA".
              All letters in this word are not capitals, like "leetcode".
              Only the first letter in this word is capital if it has more than one letter, like "Google".
              Otherwise, we define that this word doesn't use capitals in a right way.*/
            do
            {
                initialize();
                input();
                output(detect());
                endProgram();
            } while (!endMessage);
        }
        public static void initialize()
        {
            word = lowerWord = upperWord = normalWord = end = "";
            endMessage = false;
        }
        public static void input()
        {
            Console.WriteLine("Enter a word.");
            word = Console.ReadLine();
        }
        public static bool detect()
        {
            lowerWord = word.ToLower();
            upperWord = word.ToUpper();
            normalWord = upperWord[0] + lowerWord.Substring(1);
            if (word == lowerWord || word == upperWord || word == normalWord)
                return true;
            else
                return false;
        }
        public static void output(bool result)
        {
            if (result)
                Console.WriteLine("Your word \"" + word + " is in correct capitalization.");
            else
                Console.WriteLine("Your word \"" + word + "is not correctly capitalized. Maybe you want to type the following words?\n"+upperWord+"\n"+lowerWord+"\n"+normalWord);
        }
        public static void endProgram()
        {
            do
            {
                Console.WriteLine("Retry? (Y/N)");
                end = Console.ReadLine();
                if (end == "Y")
                {
                    endMessage = false;
                    break;
                }
                else if (end == "N")
                {
                    endMessage = true;
                    break;
                }
            } while (true);
        }
    }
}
