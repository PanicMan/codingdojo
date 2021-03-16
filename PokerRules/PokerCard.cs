using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PokerRules
{
    public class PokerCard: IComparable<PokerCard>
    {
        public enum Cards
        {
            Undefined = 1,
            Two = 2,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queeen,
            King,
            Ace
        };
        public enum Suits
        {
            Spades,
            Hearts,
            Diamonds,
            Clubs
        };

        public Cards Card { get; private set; }
        public Suits Suit { get; private set; }

        public PokerCard(Cards card, Suits suit)
        {
            Card = card;
            Suit = suit;
        }

        public int CompareTo(PokerCard other)
        {
            return Card == other.Card ? 0 : Card < other.Card ? -1 : 1;
        }
    }
}
