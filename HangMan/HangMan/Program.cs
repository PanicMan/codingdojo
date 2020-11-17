using System;

namespace HangMan
{
    class Program
    {
        static void Main(string[] args)
        {
            string _secret = "BigSecret";
            var hman = new Hangman(_secret);
            var guessed = hman.GetGuessed();
            Console.WriteLine("Here the hint: " + guessed);

            while (true)
            {   
                Console.Write(String.Format("{0} tries left. Guess a char: ", hman.GetTries()));
                var ret = Console.ReadKey();
                Console.WriteLine("");

                guessed = hman.Guess(ret.KeyChar);
                Console.WriteLine("Here the guess: " + guessed);
                Console.WriteLine("");

                var hang = hman.GetHangman();
                Console.Write(hang);
                Console.WriteLine("");

                if (hman.GetTries() == 0)
                {
                    Console.WriteLine("You looooooose!!!!");
                    Console.WriteLine("The secret was: " + _secret);
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
