using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PokerRules
{
    public class PokerHand
    {
        public PokerCard[] MyCards { get; private set; }
        public PokerCard[] CommCards { get; private set; }

        public PokerHand(PokerCard[] cards, PokerCard[] commCards)
        {
            MyCards = cards;
            CommCards = commCards;
        }
    }
}
