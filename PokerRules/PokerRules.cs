using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerRules
{
    public static class PokerRules
    {
        private static Random rnd = new Random(DateTime.Now.Second);

        private static List<PokerCard> CreateSortedList(PokerHand hand, PokerCard[] CommCards)
        {
            List<PokerCard> allCards = new List<PokerCard>();
            allCards.Add(hand.MyCards[0]);
            allCards.Add(hand.MyCards[1]);

            foreach (PokerCard commCard in CommCards)
            {
                allCards.Add(commCard);
            }
            allCards.Sort();
            allCards.Reverse();
            return allCards;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static PokerCard IsHighcard(PokerHand hand, PokerCard[] CommCards)
        {
            return hand.MyCards[0].Card > hand.MyCards[1].Card ? hand.MyCards[0] : hand.MyCards[1];
        }

        public static PokerCard IsPair(PokerHand hand, PokerCard[] CommCards)
        {
            PokerCard retPair = new PokerCard(PokerCard.Cards.Undefined, PokerCard.Suits.Hearts);

            if (hand.MyCards[0].Card == hand.MyCards[1].Card)
            {
                retPair = hand.MyCards[0];
            }

            foreach (PokerCard commCard in CommCards)
            {
                if ((commCard.Card == hand.MyCards[0].Card || commCard.Card == hand.MyCards[1].Card) &&
                    commCard.Card > retPair.Card)
                {
                    retPair = commCard;
                }
            }

            return retPair;
        }
        public static Tuple<PokerCard, PokerCard> IsTwoPairs(PokerHand hand, PokerCard[] CommCards)
        {
            PokerCard retPair1 = new PokerCard(PokerCard.Cards.Undefined, PokerCard.Suits.Hearts);
            PokerCard retPair2 = new PokerCard(PokerCard.Cards.Undefined, PokerCard.Suits.Hearts);

            foreach (PokerCard commCard in CommCards)
            {
                if (commCard.Card == hand.MyCards[0].Card)
                {
                    retPair1 = commCard;
                }
            }

            if (retPair1.Card != PokerCard.Cards.Undefined)
            {
                foreach (PokerCard commCard in CommCards)
                {
                    if (commCard.Card == hand.MyCards[1].Card)
                    {
                        retPair2 = commCard;
                    }
                }
            }

            return new Tuple<PokerCard, PokerCard>(retPair1, retPair2);
        }
        public static PokerCard IsThreeOfKind(PokerHand hand, PokerCard[] CommCards)
        {
            PokerCard retTripple = new PokerCard(PokerCard.Cards.Undefined, PokerCard.Suits.Hearts);
            var allCards = CreateSortedList(hand, CommCards);

            int counter = 0;
            PokerCard prevCard = new PokerCard(PokerCard.Cards.Undefined, PokerCard.Suits.Hearts);
            foreach (PokerCard card in allCards)
            {
                if (prevCard.Card == PokerCard.Cards.Undefined || prevCard.Card == card.Card)
                {
                    counter++;
                }
                else
                {
                    counter = 0;
                }
                prevCard = card;

                if (counter == 3)
                {
                    retTripple = prevCard;
                    break;
                }
            }

            return retTripple;
        }
        public static PokerCard IsStraight(PokerHand hand, PokerCard[] CommCards)
        {
            PokerCard retTopCard = new PokerCard(PokerCard.Cards.Undefined, PokerCard.Suits.Hearts);
            var allCards = CreateSortedList(hand, CommCards);

            bool lowAceCase = false;
            int counter = 0;
            PokerCard prevCard = new PokerCard(PokerCard.Cards.Undefined, PokerCard.Suits.Hearts);
            foreach (PokerCard card in allCards)
            {
                if (prevCard.Card == PokerCard.Cards.Undefined)
                {
                    counter++;
                }
                else if (prevCard.Card == card.Card || (int)prevCard.Card == (int)card.Card + 1)
                {
                    counter++;

                    if (card.Card == PokerCard.Cards.Two && allCards[0].Card == PokerCard.Cards.Ace && counter != 5)
                    {
                        lowAceCase = true;
                        counter++;
                    }
                }
                else
                {
                    counter = 1;
                }
                prevCard = card;

                if (counter == 5)
                {
                    retTopCard = allCards[allCards.IndexOf(card) - (lowAceCase ? 3 : 4)];
                    break;
                }
            }

            return retTopCard;
        }
        public static PokerCard IsFlush(PokerHand hand, PokerCard[] CommCards)
        {
            PokerCard retTopCard = new PokerCard(PokerCard.Cards.Undefined, PokerCard.Suits.Hearts);
            var allCards = CreateSortedList(hand, CommCards);

            List<PokerCard> countSpades     = new List<PokerCard>();
            List<PokerCard> countHearts     = new List<PokerCard>();
            List<PokerCard> countDiamonds   = new List<PokerCard>();
            List<PokerCard> countClubs      = new List<PokerCard>();

            foreach (PokerCard card in allCards)
            {
                switch (card.Suit)
                {
                    case PokerCard.Suits.Spades:
                        countSpades.Add(card);
                        break;
                    case PokerCard.Suits.Hearts:
                        countHearts.Add(card);
                        break;
                    case PokerCard.Suits.Diamonds:
                        countDiamonds.Add(card);
                        break;
                    case PokerCard.Suits.Clubs:
                        countClubs.Add(card);
                        break;
                }
            }

            if (countSpades.Count >= 5)
            {
                retTopCard = countSpades[0];
            }
            else if (countHearts.Count >= 5)
            {
                retTopCard = countHearts[0];
            }
            else if (countDiamonds.Count >= 5)
            {
                retTopCard = countDiamonds[0];
            }
            else if (countClubs.Count >= 5)
            {
                retTopCard = countClubs[0];
            }

            return retTopCard;
        }
        public static PokerCard IsFlushFnc1(PokerHand hand, PokerCard[] CommCards)
        {
            var allCards = CreateSortedList(hand, CommCards);
            var result = allCards.GroupBy(c => c.Suit).FirstOrDefault(g => g.Count() >= 5);
            return result != null ? result.FirstOrDefault() : new PokerCard(PokerCard.Cards.Undefined, PokerCard.Suits.Hearts);
        }
        public static PokerCard IsFlushFnc2(PokerHand hand, PokerCard[] CommCards)
        {
            var allCards = CreateSortedList(hand, CommCards);
            allCards.Shuffle();

            //Hint: https://docs.microsoft.com/de-de/dotnet/csharp/linq/group-query-results
            var querySuits =
                from card in allCards
                orderby card.Card descending
                group card by card.Suit into newGroup
                where newGroup.Count() >= 5
                orderby newGroup.Key
                select newGroup;

            foreach (var suitGroup in querySuits)
            {
                return suitGroup.FirstOrDefault();
            }
            return new PokerCard(PokerCard.Cards.Undefined, PokerCard.Suits.Hearts);
        }
    }
}