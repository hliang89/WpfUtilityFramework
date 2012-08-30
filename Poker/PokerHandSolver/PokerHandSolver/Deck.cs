using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHandSolver
{
    public class Deck
    {
        public readonly int StdDeckSize = 52;

        public string DeckID { get; private set; }
        public bool IsEmpty { get; private set; }

        private List<Card> cards;
        public List<Card> Cards
        { get { return cards; } private set { cards = value; }}

        private List<Card> dealtCards;
        public List<Card> DealtCards { get { return dealtCards; } private set { dealtCards = value; } }

        public Deck(string deckID)
        {
            DeckID = deckID;
            MakeDeck();
            ShuffleDeck();
        }

        private void MakeDeck()
        {
            Cards = new List<Card>();
            Card card;
            for (int i = 0; i < Enum.GetValues(typeof(Suits)).Length; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    card = new Card((Suits)i, j + 1);
                    Cards.Add(card);
                }
            }

            DealtCards = new List<Card>();
        }

        public void ShuffleDeck()
        {
            Random rand = new Random();
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
               int swapIndex = rand.Next(i);
               Card temp = Cards[i];
               Cards[i] = Cards[swapIndex];
               Cards[swapIndex] = temp;
            }
        }

        public Card DrawTopCard()
        {
            if (cards.Count == 0)
            {
                IsEmpty = true;
                throw new InvalidOperationException("Invalid Operation: Deck is empty");
            }
            Card card = Cards[0];
            Cards.RemoveAt(0);
            DealtCards.Add(card);
            return card;
        }

        public Card DrawRandomCard()
        {
            Random rand = new Random();
            int index = rand.Next(Cards.Count - 1);
            Card card = Cards[index];
            Cards.RemoveAt(index);
            return card;
        }

        public void ReshuffleDeck()
        {
            Cards.AddRange(DealtCards);
            ShuffleDeck();
            DealtCards = new List<Card>();
        }
    }
}

