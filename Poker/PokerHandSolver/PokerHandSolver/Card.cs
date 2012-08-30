using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHandSolver
{
    public enum Suits
    {
        Diamond,
        Club,
        Heart,
        Spade
    }
    public class Card
    {
        public int Rank { get; private set; }
        public Suits Suit { get; private set; }

        public Card(Suits suit, int rank)
        {
            if (rank <= 0 || rank >= 14)
            {
                throw new ArgumentOutOfRangeException("Rank");
            }

            Rank = rank;
            Suit = suit;
        }
    }
}
