using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace PokerHandSolver
{
    [TestFixture]
    public class SolverPrototypeTest
    {
        [Test]
        public void IsFullHouse_FullHouse_True()
        {
            SolverPrototype solver = new SolverPrototype("Deck1");
            HandFullHouse_Stub stub = new HandFullHouse_Stub();
            bool expected = true;

            bool actual = solver.IsFullHouse(stub);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void IsFullHouse_FourOfAKind_False()
        {
            SolverPrototype solver = new SolverPrototype("Deck1");
            HandFourOfAKind_Stub stub = new HandFourOfAKind_Stub();
            bool expected = false;

            bool actual = solver.IsFullHouse(stub);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void IsFourOfAKind_FourOfAKind_true()
        {
            SolverPrototype solver = new SolverPrototype("Deck1");
            HandFourOfAKind_Stub stub = new HandFourOfAKind_Stub();
            bool expected = true;

            bool actual = solver.IsFourOfAKind(stub);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void IsFourOfAKind_FullHouse_false()
        {
            SolverPrototype solver = new SolverPrototype("Deck1");
            HandFullHouse_Stub stub = new HandFullHouse_Stub();
            bool expected = false;

            bool actual = solver.IsFourOfAKind(stub);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void IsFlush_Flush_True()
        {
            SolverPrototype solver = new SolverPrototype("Deck1");
            HandFlush_Stub stub = new HandFlush_Stub();
            bool expected = true;

            bool actual = solver.IsFlush(stub);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void IsFlush_NotFlush_False()
        {
            SolverPrototype solver = new SolverPrototype("Deck1");
            HandFourOfAKind_Stub stub = new HandFourOfAKind_Stub();
            bool expected = false;

            bool actual = solver.IsFlush(stub);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void IsTwoPairs_TwoPairs_True()
        {
            SolverPrototype solver = new SolverPrototype("Deck1");
            HandTwoPairs_Stub hand = new HandTwoPairs_Stub();
            bool expected = true;

            bool actual = solver.IsTwoPairs(hand);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void IsTwoPairs_OnePair_True()
        {
            SolverPrototype solver = new SolverPrototype("Deck1");
            HandOnePairs_Stub hand = new HandOnePairs_Stub();
            bool expected = true;

            bool actual = solver.IsPair(hand);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }

    public class HandStub : IHand
    {
        public List<Card> Cards { get; private set; }
    }

    public class HandOnePairs_Stub : IHand
    {
        public List<Card> Cards
        {
            get;
            private set;
        }

        public HandOnePairs_Stub()
        {
            Cards = new List<Card>(){new Card(Suits.Spade,5),new Card(Suits.Diamond,5),new Card(Suits.Heart,6),new Card(Suits.Club,4)
                ,new Card(Suits.Spade,10)};
        }
    }

    public class HandTwoPairs_Stub : IHand
    {
        public List<Card> Cards
        {
            get;
            private set;
        }

        public HandTwoPairs_Stub()
        {
            Cards = new List<Card>(){new Card(Suits.Spade,5),new Card(Suits.Diamond,5),new Card(Suits.Heart,4),new Card(Suits.Club,4)
                ,new Card(Suits.Spade,10)};
        }
    }

    public class HandFullHouse_Stub : IHand
    {
        public List<Card> Cards
        {
            get;
            private set;
        }

        public HandFullHouse_Stub()
        {
            Cards = new List<Card>(){new Card(Suits.Club,5),new Card(Suits.Diamond,5),new Card(Suits.Heart,5),new Card(Suits.Club,10)
                ,new Card(Suits.Spade,10)};
        }
    }

    public class HandFourOfAKind_Stub : IHand
    {
        public List<Card> Cards
        {
            get;
            private set;
        }

        public HandFourOfAKind_Stub()
        {
            Cards = new List<Card>(){new Card(Suits.Club,5),new Card(Suits.Diamond,5),new Card(Suits.Heart,5),new Card(Suits.Spade,5)
                ,new Card(Suits.Spade,10)};
        }
    }

    public class HandFlush_Stub : IHand
    {
        public List<Card> Cards
        {
            get;
            private set;
        }

        public HandFlush_Stub()
        {
            Cards = new List<Card>(){new Card(Suits.Club,5),new Card(Suits.Club,2),new Card(Suits.Club,6),new Card(Suits.Club,9)
                ,new Card(Suits.Club,10)};
        }
    }

    public class SolverPrototypeService
    {
        public SolverPrototype Solver;

        public SolverPrototypeService(SolverPrototype solver)
        {

        }
    }
}
