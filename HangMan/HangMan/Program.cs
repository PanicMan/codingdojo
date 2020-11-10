using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HangMan
{
    class Program
    {
        static void Main(string[] args)
        { 
            var hman = new Hangman("BigSecret");
            var guessed = hman.Guess('\n');
            Console.WriteLine("Here the hint: " + guessed);

            while (true)
            {   
                Console.Write("Guess a char: ");
                var ret = Console.ReadKey();
                Console.WriteLine("");

                guessed = hman.Guess(ret.KeyChar);
                Console.WriteLine("Here the guess: " + guessed);
                Console.WriteLine("");

                var hang = hman.GetHangman();
                Console.Write(hang);
                Console.WriteLine("");

                if (hman.GetTries() == 11)
                {
                    Console.WriteLine("You looooooose!!!!");
                    break;
                }
                else if (guessed.IndexOf('_') == -1)
                {
                    Console.WriteLine("You won! Congratulations!!!!");
                    break;
                }
            }

            Console.Read();
        }
    }
}
