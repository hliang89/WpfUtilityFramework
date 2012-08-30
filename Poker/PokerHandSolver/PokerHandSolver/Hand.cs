using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHandSolver
{
    public enum PokerHandTypes
    {
        HighCard,
        Pair,
        TwoPair,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush
    }   

    public class Hand : IHand
    {
        public readonly int Handsize = 5;
        public List<Card> Cards { get; private set; }
        public PokerHandTypes PokerHand { get; private set; }
        public List<int> RanksInOrder { get; private set; }
        public List<int> PokerHandRanks { get; private set; }

        public Hand(Deck deck)
        {
            Cards = new List<Card>();
            for (int i = 0; i < Handsize; i++)
            {
                Cards.Add(deck.DrawTopCard());
            }
            GetRanksInOrder();
            GetPokerHand();
        }

        public void DrawNewHand(Deck deck)
        {
            Cards = new List<Card>();
            for (int i = 0; i < Handsize; i++)
            {
                Cards.Add(deck.DrawTopCard());
            }
            GetRanksInOrder();
            GetPokerHand();
        }

        private void GetRanksInOrder()
        {
            RanksInOrder = new List<int>();
            foreach (Card card in Cards)
            {
                RanksInOrder.Add(card.Rank);
            }
            RanksInOrder.Sort();
            RanksInOrder.Reverse();
        }

        private void GetStraightRanks()
        {
            if (RanksInOrder[RanksInOrder.Count - 1] == 1 && RanksInOrder[0] == 13)
            {
                PokerHandRanks.Add(14);
                PokerHandRanks.AddRange(RanksInOrder.Take(RanksInOrder.Count - 1));
            }
            else
            {
                PokerHandRanks = RanksInOrder;
            }
        }

        private void GetNoneStraightRanks()
        {
            Dictionary<int, int> rankOrderedInstances = new Dictionary<int, int>();
            rankOrderedInstances = GetRankInstances(this).OrderByDescending(c => c.Value).ToDictionary(c => c.Key, c => c.Value);
            if (rankOrderedInstances.ContainsKey(1))
            {
                rankOrderedInstances.Remove(1);
                PokerHandRanks.Add(14);
            }
            foreach (KeyValuePair<int, int> pair in rankOrderedInstances)
            {
                PokerHandRanks.Add(pair.Key);
            }
        }

        private void GetPokerHand()
        {
            PokerHandRanks = new List<int>();
            if (IsStraightFlush(this))
            {
                PokerHand = PokerHandTypes.StraightFlush;
                GetStraightRanks();
                return;
            }
            else if (IsFourOfAKind(this)) 
            {
                PokerHand = PokerHandTypes.FourOfAKind;
                GetNoneStraightRanks();
                return;
            }
            else if (IsFullHouse(this))
            {
                PokerHand = PokerHandTypes.FullHouse;
                GetNoneStraightRanks();
                return;
            }
            else if (IsFlush(this))
            {
                PokerHand = PokerHandTypes.Flush;
                GetStraightRanks();
                return;
            }
            else if (IsStraight(this))
            {
                PokerHand = PokerHandTypes.Straight;
                GetStraightRanks();
                return;
            }
            else if (IsTwoPairs(this))
            {
                PokerHand = PokerHandTypes.TwoPair;
                GetNoneStraightRanks();
                return;
            }
            else if (IsPair(this))
            {
                PokerHand = PokerHandTypes.Pair;
                GetNoneStraightRanks();
                return;
            }
            PokerHand = PokerHandTypes.HighCard;
            GetStraightRanks();
            //throw new InvalidOperationException();
        }

        public bool IsStraightFlush(IHand hand)
        {
            if (!IsFlush(hand)) return false;
            if (!IsStraight(hand)) return false;
            return true;
        }

        public bool IsFlush(IHand hand)
        {
            Suits firstSuit = hand.Cards[0].Suit;

            for (int i = 1; i < hand.Cards.Count; i++)
            {
                if (hand.Cards[i].Suit != firstSuit) return false;
            }
            return true;
        }

        public static bool IsStraight(IHand hand)
        {
            List<int> ranks = new List<int>();
            for (int i = 0; i < hand.Cards.Count; i++)
            {
                ranks.Add(hand.Cards[i].Rank);
            }
            ranks.Sort();

            if (ranks[0] != 1)
            {
                for (int i = 1; i < ranks.Count; i++)
                    if (ranks[i] != (ranks[i - 1] + 1)) return false;
            }
            else
            {
                if (ranks[1] != 10)
                {
                    for (int i = 1; i < ranks.Count; i++)
                        if (ranks[i] != (ranks[i - 1] + 1)) return false;
                }
                else
                {
                    ranks.RemoveAt(0);
                    ranks.Add(14);
                    for (int i = 1; i < ranks.Count; i++)
                        if (ranks[i] != (ranks[i - 1] + 1)) return false;
                }
            }
            return true;
        }

        public bool IsFourOfAKind(IHand hand)
        {
            List<int> ranks = GetRanksFromHand(hand);
            Dictionary<int, int> rankInstances = new Dictionary<int, int>();
            foreach (int rank in ranks)
            {
                if (!rankInstances.ContainsKey(rank))
                    rankInstances.Add(rank, 1);
                else
                {
                    rankInstances[rank] += 1;
                    if (rankInstances[rank] == 4) return true;
                }
            }
            return false;
        }

        public bool IsFullHouse(IHand hand)
        {
            List<int> ranks = GetRanksFromHand(hand);
            List<int> instances = new List<int>();
            Dictionary<int, int> rankInstances = new Dictionary<int, int>();
            rankInstances = GetRankInstances(hand);
            instances = rankInstances.Values.ToList();
            return (instances.Contains(3) && instances.Contains(2));
        }

        public List<int> GetRanksFromHand(IHand hand)
        {
            List<int> ranks = new List<int>();
            for (int i = 0; i < hand.Cards.Count; i++)
            {
                ranks.Add(hand.Cards[i].Rank);
            }
            return ranks;
        }

        public bool IsTwoPairs(IHand hand)
        {
            List<int> ranks = GetRanksFromHand(hand);
            List<int> instances = new List<int>();
            Dictionary<int, int> rankInstances = new Dictionary<int, int>();
            rankInstances = GetRankInstances(hand);
            instances = rankInstances.Values.ToList();
            int count = 0;
            for (int i = 0; i < instances.Count; i++)
            {
                if (instances[i] == 2) count++;
            }
            return (count == 2);
        }

        public bool IsPair(IHand hand)
        {
            List<int> ranks = GetRanksFromHand(hand);
            List<int> instances = new List<int>();
            Dictionary<int, int> rankInstances = new Dictionary<int, int>();
            rankInstances = GetRankInstances(hand);
            instances = rankInstances.Values.ToList();
            int count = 0;
            for (int i = 0; i < instances.Count; i++)
            {
                if (instances[i] == 2) count++;
            }
            return (count == 1);
        }

        public Dictionary<int, int> GetRankInstances(IHand hand)
        {
            List<int> ranks = GetRanksFromHand(hand);
            Dictionary<int, int> rankInstances = new Dictionary<int, int>();
            foreach (int rank in ranks)
            {
                if (!rankInstances.ContainsKey(rank))
                    rankInstances.Add(rank, 1);
                else
                {
                    rankInstances[rank] += 1;
                }
            }
            return rankInstances;
        }
    }
}
