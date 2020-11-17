using System;
using System.Text;

namespace HangMan
{
    public class Hangman
    {
        private int _tries = 10;
        private string _secret;
        private string _guessed;

        public Hangman (string secret)
        {
            _secret = secret;
            _guessed = new String('_', secret.Length);
        }

        public int GetTries() { return _tries; }
        public string GetGuessed() { return _guessed; }

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
            {
                _tries--;
            }

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
                case 10: return "";
                case 9: return "┴────";
                case 8: return "│\n┴────"; 
                case 7: return "│\n│\n┴────"; 
                case 6: return "│\n│\n│\n┴────"; 
                case 5: return "┌──┐\n│\n│\n│\n┴────"; 
                case 4: return "┌──┐\n│  O\n│\n│\n┴────"; 
                case 3: return "┌──┐\n│  O\n│ \\│\n│\n┴────"; 
                case 2: return "┌──┐\n│  O\n│ \\│/\n│\n┴────"; 
                case 1: return "┌──┐\n│  O\n│ \\│/\n│ /\n┴────"; 
                case 0: return "┌──┐\n│  O\n│ \\│/\n│ / \\\n┴────"; 
            }
            return "";
        }
    }
}

