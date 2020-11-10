using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMan
{
    public class Hangman
    {
        private int _tries = 0;
        private string _secret;
        private string _guessed;

        public Hangman (string secret)
        {
            _secret = secret;
            _guessed = new String('_', secret.Length);
        }

        public int GetTries() { return _tries; }

        public string Guess (char letter)
        {
            StringBuilder sbGuessed = new StringBuilder(_guessed);

            bool found = false;
            var upperSecret = _secret.ToUpper();
            var upperLetter = letter.ToString().ToUpper();

            for (int i=0; i<_secret.Length; i++)
            {
                if (upperSecret[i] == upperLetter[0])
                {
                    found = true;
                    sbGuessed[i] = _secret[i]; 
                }
            }

            if (!found)
                _tries++;

            _guessed = sbGuessed.ToString();
            return _guessed;
        }

        public string GetHangman ()
        {
            /*
            ┌──┐ 
            │  O 
            │ \│/
            │ / \
            ┴────
            */

            switch (_tries)
            {
                case 1: return "";
                case 2: return "┴────";
                case 3: return "│\n┴────"; 
                case 4: return "│\n│\n┴────"; 
                case 5: return "│\n│\n│\n┴────"; 
                case 6: return "┌──┐\n│\n│\n│\n┴────"; 
                case 7: return "┌──┐\n│  O\n│\n│\n┴────"; 
                case 8: return "┌──┐\n│  O\n│ \\│\n│\n┴────"; 
                case 9: return "┌──┐\n│  O\n│ \\│/\n│\n┴────"; 
                case 10: return "┌──┐\n│  O\n│ \\│/\n│ /\n┴────"; 
                case 11: return "┌──┐\n│  O\n│ \\│/\n│ / \\\n┴────"; 
            }
            return "";
        }
    }
}

