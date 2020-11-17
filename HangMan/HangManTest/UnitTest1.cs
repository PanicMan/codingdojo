using HangMan;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HangManTest
{
    [TestClass]
    public class UnitTest1
    {
        [DataTestMethod]
        [DataRow("Secret", "______", "x")]
        [DataRow("Secret", "__c___", "c")]
        [DataRow("Secret", "S_c___", "cs")]
        [DataRow("Secret", "Sec_e_", "cse")]
        [DataRow("Secret", "Secre_", "cser")]
        [DataRow("Secret", "Secret", "csert")]
        public void TestHangmanGuess(string _secret, string _expected, string _keys)
        {
            var man = new Hangman(_secret);
            
            string guessed = "";
            foreach (char c in _keys)
            {
                guessed = man.Guess(c);
            }

            guessed = man.GetGuessed();
            Assert.IsTrue(guessed == _expected, "guess work correct");
        }

        [DataTestMethod]
        [DataRow("Secret", "s", 10, "")]
        [DataRow("Secret", "sp", 9, "┴────")]
        [DataRow("Secret", "sxx", 8, "│\n┴────")]
        public void TestHangmanHang(string _secret, string _keys, int _expected, string _hang)
        {
            var man = new Hangman(_secret);

            string guessed = "";
            foreach (char c in _keys)
            {
                guessed = man.Guess(c);
            }

            var tries = man.GetTries();
            var hang = man.GetHangman();
            
            Assert.IsTrue(tries == _expected, "Tries correct");
            Assert.IsTrue(hang == _hang, "Hangman correct");
        }
    }
}
