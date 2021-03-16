using NUnit.Framework;
using PokerRules;
using System;

namespace PokerRulesTest
{
    public class TestRules
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckIsHighCard()
        {
            PokerCard[] CommCards = new PokerCard[5];
            CommCards[0] = new PokerCard(PokerCard.Cards.Four, PokerCard.Suits.Clubs);
            CommCards[1] = new PokerCard(PokerCard.Cards.King, PokerCard.Suits.Spades);
            CommCards[2] = new PokerCard(PokerCard.Cards.Four, PokerCard.Suits.Hearts);
            CommCards[3] = new PokerCard(PokerCard.Cards.Eight, PokerCard.Suits.Spades);
            CommCards[4] = new PokerCard(PokerCard.Cards.Seven, PokerCard.Suits.Spades);

            PokerCard[] HoleCardsCheck = new PokerCard[2];
            HoleCardsCheck[0] = new PokerCard(PokerCard.Cards.Ace, PokerCard.Suits.Hearts);
            HoleCardsCheck[1] = new PokerCard(PokerCard.Cards.King, PokerCard.Suits.Diamonds);
            PokerHand HandCheck = new PokerHand(HoleCardsCheck, CommCards);

            Assert.IsTrue(PokerRules.PokerRules.IsHighcard(HandCheck, CommCards).Card == PokerCard.Cards.Ace);
        }

        [Test]
        public void CheckIsPair()
        {
            PokerCard[] CommCards = new PokerCard[5];
            CommCards[0] = new PokerCard(PokerCard.Cards.Four, PokerCard.Suits.Clubs);
            CommCards[1] = new PokerCard(PokerCard.Cards.King, PokerCard.Suits.Spades);
            CommCards[2] = new PokerCard(PokerCard.Cards.Four, PokerCard.Suits.Hearts);
            CommCards[3] = new PokerCard(PokerCard.Cards.Eight, PokerCard.Suits.Spades);
            CommCards[4] = new PokerCard(PokerCard.Cards.Seven, PokerCard.Suits.Spades);

            PokerCard[] HoleCardsCheck = new PokerCard[2];
            HoleCardsCheck[0] = new PokerCard(PokerCard.Cards.Ace, PokerCard.Suits.Hearts);
            HoleCardsCheck[1] = new PokerCard(PokerCard.Cards.King, PokerCard.Suits.Diamonds);
            PokerHand HandCheck = new PokerHand(HoleCardsCheck, CommCards);

            Assert.IsTrue(PokerRules.PokerRules.IsPair(HandCheck, CommCards).Card == PokerCard.Cards.King);
        }

        [Test]
        public void CheckIsTwoPairs()
        {
            PokerCard[] CommCards = new PokerCard[5];
            CommCards[0] = new PokerCard(PokerCard.Cards.Four, PokerCard.Suits.Clubs);
            CommCards[1] = new PokerCard(PokerCard.Cards.King, PokerCard.Suits.Spades);
            CommCards[2] = new PokerCard(PokerCard.Cards.Four, PokerCard.Suits.Hearts);
            CommCards[3] = new PokerCard(PokerCard.Cards.Eight, PokerCard.Suits.Spades);
            CommCards[4] = new PokerCard(PokerCard.Cards.Seven, PokerCard.Suits.Spades);

            PokerCard[] HoleCardsCheck = new PokerCard[2];
            HoleCardsCheck[0] = new PokerCard(PokerCard.Cards.Eight, PokerCard.Suits.Hearts);
            HoleCardsCheck[1] = new PokerCard(PokerCard.Cards.King, PokerCard.Suits.Diamonds);
            PokerHand HandCheck = new PokerHand(HoleCardsCheck, CommCards);

            Tuple<PokerCard, PokerCard> retVal = PokerRules.PokerRules.IsTwoPairs(HandCheck, CommCards);
            Assert.IsTrue(retVal.Item1.Card == PokerCard.Cards.Eight);
            Assert.IsTrue(retVal.Item2.Card == PokerCard.Cards.King);
        }

        [Test]
        public void CheckIsTriple()
        {
            PokerCard[] CommCards = new PokerCard[5];
            CommCards[0] = new PokerCard(PokerCard.Cards.Four, PokerCard.Suits.Clubs);
            CommCards[1] = new PokerCard(PokerCard.Cards.King, PokerCard.Suits.Spades);
            CommCards[2] = new PokerCard(PokerCard.Cards.Four, PokerCard.Suits.Hearts);
            CommCards[3] = new PokerCard(PokerCard.Cards.Eight, PokerCard.Suits.Spades);
            CommCards[4] = new PokerCard(PokerCard.Cards.Seven, PokerCard.Suits.Spades);

            PokerCard[] HoleCardsCheck = new PokerCard[2];
            HoleCardsCheck[0] = new PokerCard(PokerCard.Cards.King, PokerCard.Suits.Hearts);
            HoleCardsCheck[1] = new PokerCard(PokerCard.Cards.King, PokerCard.Suits.Diamonds);
            PokerHand HandCheck = new PokerHand(HoleCardsCheck, CommCards);

            Assert.IsTrue(PokerRules.PokerRules.IsThreeOfKind(HandCheck, CommCards).Card == PokerCard.Cards.King);
        }

        [Test]
        public void CheckIsStraight()
        {
            PokerCard[] CommCards = new PokerCard[5];
            CommCards[0] = new PokerCard(PokerCard.Cards.Four, PokerCard.Suits.Clubs);
            CommCards[1] = new PokerCard(PokerCard.Cards.Six, PokerCard.Suits.Spades);
            CommCards[2] = new PokerCard(PokerCard.Cards.Eight, PokerCard.Suits.Hearts);
            CommCards[3] = new PokerCard(PokerCard.Cards.Eight, PokerCard.Suits.Spades);
            CommCards[4] = new PokerCard(PokerCard.Cards.King, PokerCard.Suits.Spades);

            PokerCard[] HoleCardsCheck = new PokerCard[2];
            HoleCardsCheck[0] = new PokerCard(PokerCard.Cards.Five, PokerCard.Suits.Hearts);
            HoleCardsCheck[1] = new PokerCard(PokerCard.Cards.Seven, PokerCard.Suits.Diamonds);
            PokerHand HandCheck = new PokerHand(HoleCardsCheck, CommCards);

            Assert.IsTrue(PokerRules.PokerRules.IsStraight(HandCheck, CommCards).Card == PokerCard.Cards.Eight);

            //With low Ace
            CommCards[0] = new PokerCard(PokerCard.Cards.Four, PokerCard.Suits.Clubs);
            CommCards[1] = new PokerCard(PokerCard.Cards.Two, PokerCard.Suits.Spades);
            CommCards[2] = new PokerCard(PokerCard.Cards.King, PokerCard.Suits.Hearts);
            CommCards[3] = new PokerCard(PokerCard.Cards.Queeen, PokerCard.Suits.Spades);
            CommCards[4] = new PokerCard(PokerCard.Cards.Ace, PokerCard.Suits.Spades);

            HoleCardsCheck[0] = new PokerCard(PokerCard.Cards.Five, PokerCard.Suits.Hearts);
            HoleCardsCheck[1] = new PokerCard(PokerCard.Cards.Three, PokerCard.Suits.Diamonds);
            HandCheck = new PokerHand(HoleCardsCheck, CommCards);

            Assert.IsTrue(PokerRules.PokerRules.IsStraight(HandCheck, CommCards).Card == PokerCard.Cards.Five);
        }
        [Test]
        public void CheckIsFlush()
        {
            PokerCard[] CommCards = new PokerCard[5];
            CommCards[0] = new PokerCard(PokerCard.Cards.Four, PokerCard.Suits.Hearts);
            CommCards[1] = new PokerCard(PokerCard.Cards.Two, PokerCard.Suits.Hearts);
            CommCards[2] = new PokerCard(PokerCard.Cards.King, PokerCard.Suits.Hearts);
            CommCards[3] = new PokerCard(PokerCard.Cards.Queeen, PokerCard.Suits.Spades);
            CommCards[4] = new PokerCard(PokerCard.Cards.Ace, PokerCard.Suits.Spades);

            PokerCard[] HoleCardsCheck = new PokerCard[2];
            HoleCardsCheck[0] = new PokerCard(PokerCard.Cards.Jack, PokerCard.Suits.Hearts);
            HoleCardsCheck[1] = new PokerCard(PokerCard.Cards.Ace, PokerCard.Suits.Hearts);

            PokerHand HandCheck = new PokerHand(HoleCardsCheck, CommCards);

            Assert.IsTrue(PokerRules.PokerRules.IsFlush(HandCheck, CommCards).Card == PokerCard.Cards.Ace);
            Assert.IsTrue(PokerRules.PokerRules.IsFlush(HandCheck, CommCards).Suit == PokerCard.Suits.Hearts);

            //Functional programming 1
            Assert.IsTrue(PokerRules.PokerRules.IsFlushFnc1(HandCheck, CommCards).Card == PokerCard.Cards.Ace);
            Assert.IsTrue(PokerRules.PokerRules.IsFlushFnc1(HandCheck, CommCards).Suit == PokerCard.Suits.Hearts);

            //Functional programming 2
            Assert.IsTrue(PokerRules.PokerRules.IsFlushFnc2(HandCheck, CommCards).Card == PokerCard.Cards.Ace);
            Assert.IsTrue(PokerRules.PokerRules.IsFlushFnc2(HandCheck, CommCards).Suit == PokerCard.Suits.Hearts);
        }
    }
    public class TestHands
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CompareTwoHands()
        {
            PokerCard[] CommCards = new PokerCard[5];
            CommCards[0] = new PokerCard(PokerCard.Cards.Four, PokerCard.Suits.Clubs);
            CommCards[1] = new PokerCard(PokerCard.Cards.King, PokerCard.Suits.Spades);
            CommCards[2] = new PokerCard(PokerCard.Cards.Four, PokerCard.Suits.Hearts);
            CommCards[3] = new PokerCard(PokerCard.Cards.Eight, PokerCard.Suits.Spades);
            CommCards[4] = new PokerCard(PokerCard.Cards.Seven, PokerCard.Suits.Spades);

            PokerCard[] HoleCardsTed = new PokerCard[2];
            HoleCardsTed[0] = new PokerCard(PokerCard.Cards.King, PokerCard.Suits.Hearts);
            HoleCardsTed[1] = new PokerCard(PokerCard.Cards.King, PokerCard.Suits.Diamonds);
            PokerHand handTed = new PokerHand(HoleCardsTed, CommCards);

            PokerCard[] HoleCardsCarol = new PokerCard[2];
            HoleCardsCarol[0] = new PokerCard(PokerCard.Cards.Ace, PokerCard.Suits.Spades);
            HoleCardsCarol[1] = new PokerCard(PokerCard.Cards.Nine, PokerCard.Suits.Spades);
            PokerHand handCarol = new PokerHand(HoleCardsCarol, CommCards);

            Assert.Pass();
        }

    }
}