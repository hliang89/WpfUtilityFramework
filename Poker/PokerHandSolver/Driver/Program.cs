using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokerHandSolver;

namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMain2();
            
            Console.ReadLine();
        }

        public static void TestMain2()
        {
            List<int> handTypeCounts = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };

            SolverPrototype solver = new SolverPrototype("Deck1");

            Console.WriteLine("Player 1 hand: {0}, Highest ranked: {1}",
                solver.Player1Hand.PokerHand.ToString(), solver.Player1Hand.PokerHandRanks[0].ToString());

            Console.WriteLine("Player 2 hand: {0}, Highest ranked: {1}",
                solver.Player2Hand.PokerHand.ToString(), solver.Player2Hand.PokerHandRanks[0].ToString());

            Console.WriteLine("Player " + (solver.Evaluate() + 1) + " has won!");

            for (int i = 0; i < 1000; i++)
            {
                while (solver.Deck.Cards.Count >= 8)
                {
                    Console.WriteLine("--------");
                    Console.WriteLine("New Game");

                    solver.Player1Hand.DrawNewHand(solver.Deck);
                    solver.Player2Hand.DrawNewHand(solver.Deck);

                    Console.WriteLine("Player 1 hand: {0}, Highest ranked: {1}",
                    solver.Player1Hand.PokerHand.ToString(), solver.Player1Hand.PokerHandRanks[0].ToString());
                    handTypeCounts[(int)solver.Player1Hand.PokerHand] += 1;

                    Console.WriteLine("Player 2 hand: {0}, Highest ranked: {1}",
                        solver.Player2Hand.PokerHand.ToString(), solver.Player2Hand.PokerHandRanks[0].ToString());
                    handTypeCounts[(int)solver.Player1Hand.PokerHand] += 1;

                    Console.WriteLine("Player " + (solver.Evaluate() + 1) + " has won!");
                }

                solver.Deck.ReshuffleDeck();
                Console.WriteLine("Reshuffled deck");
            }

            Console.WriteLine("----------Total Hand counts--------------");
            Console.WriteLine("Striaght Flush: {0}, Four of a kind: {1}, Fullhouse: {2}, Flush: {3}, Straight: {4}, Two Pair: {5}, Pair: {6}, High: {7}",
                handTypeCounts[7], handTypeCounts[6], handTypeCounts[5], handTypeCounts[4], handTypeCounts[3], handTypeCounts[2], handTypeCounts[1], handTypeCounts[0]);
        }                

        public static void TestMain1()
        {
            Deck deck = new Deck("Deck1");
            deck.ShuffleDeck();
            PrintCards(deck);

            Card card1 = deck.DrawTopCard();

            PrintCard(card1);
            PrintCards(deck);

            Console.WriteLine("");
            deck.ReshuffleDeck();
            PrintCards(deck);
            Console.WriteLine("");
        }

        public static void PrintCards(Deck deck)
        {
            Console.WriteLine("----------Print Deck started....------------");
            for (int i = 0; i < deck.Cards.Count; i++)
            {
                Console.WriteLine("Card number: {0}, Suit: {1}, Rank: {2}", i, deck.Cards[i].Suit.ToString(), deck.Cards[i].Rank);
            }
            Console.WriteLine("----------Print Deck Ended....------------");
        }

        public static void PrintCard(Card card)
        {
            Console.WriteLine("----------Print card started....------------");
            Console.WriteLine("Card Printed, Suit: {0}, Rank: {1}", card.Suit, card.Rank);
            Console.WriteLine("----------Print card Ended....------------");
        }
    }
}
