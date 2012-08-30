using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHandSolver
{
    public class SolverPrototype
    {
        public Deck Deck { get; private set; }
        public Hand Player1Hand { get; private set; }
        public Hand Player2Hand { get; private set; }

        public SolverPrototype(string deckID)
        {
            Deck = new Deck(deckID);
            Player1Hand = new Hand(Deck);
            Player2Hand = new Hand(Deck);
        }

        public bool IsPairOrTwoPair(Hand hand)
        {
            return (IsPair(hand) || IsTwoPairs(hand));
        }

        public int Evaluate()
        {
            if ((int)Player1Hand.PokerHand == (int)Player2Hand.PokerHand)
            {
                for (int i = 0; i < Player1Hand.PokerHandRanks.Count; i++)
                {
                    if (Player1Hand.PokerHandRanks[i] > Player2Hand.PokerHandRanks[i]) return 0;
                    else if (Player2Hand.PokerHandRanks[i] > Player1Hand.PokerHandRanks[i]) return 1;
                }
                throw new Exception("Tied game");
            }
            else if ((int)Player1Hand.PokerHand > (int)Player2Hand.PokerHand)
            {
                return 0;
            }
            else if ((int)Player1Hand.PokerHand < (int)Player2Hand.PokerHand)
            {
                return 1;
            }

            return -1;
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
