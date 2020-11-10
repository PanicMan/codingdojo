using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HangMan;

namespace HangManTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestHangmanGuess()
        {
            var man = new Hangman("Secret");
            var guessed = man.Guess('c');
            Assert.IsTrue(guessed == "__c___", "Firest guess correct");

            guessed = man.Guess('s');
            Assert.IsTrue(guessed == "S_c___", "Second guess correct");
        }

        [TestMethod]
        public void TestHangmanHang()
        {
            var man = new Hangman("Secret");
            var guessed = man.Guess('\n');
            var tries = man.GetTries();
            var hang = man.GetHangman();
            
            Assert.IsTrue(tries == 1, "Tries correct");
            Assert.IsTrue(hang == "", "Hangman correct");
            
            guessed = man.Guess('p');
            
            tries = man.GetTries();
            hang = man.GetHangman();
            
            Assert.IsTrue(tries == 2, "Tries correct");
            Assert.IsTrue(hang == "┴────", "Hangman correct");
        }
    }
}
