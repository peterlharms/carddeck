﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardDeck;

namespace UnitTestProject1
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestOneCard()
		{
			Card card = new Card(Value.Ace, Suit.Spades);
			Assert.AreEqual(card.CardValue, Value.Ace);
			Assert.AreEqual(card.CardSuit, Suit.Spades);
			Assert.AreEqual(51, card.OrdinalValue());
		}
		[TestMethod]
		public void TestSuit()
		{
			Assert.AreEqual(4, Enum.GetValues(typeof(Suit)).Length);
		}
		[TestMethod]
		public void TestValue()
		{
			Assert.AreEqual(13, Enum.GetValues(typeof(Value)).Length);
		}

		[TestMethod]
		public void TestNewDeck()
		{
			Deck deck = new Deck();
			Assert.AreEqual(52, deck.Size());
			Assert.IsTrue(deckIsOrdered(deck));
			Assert.IsTrue(AllCardsThere(deck));
		}

		[TestMethod]
		public void TestCut()
		{
			Deck deck = new Deck();
			deck.DivideDeck();
			bool[] found = new bool[52];
			for (int i = 0; i < 52; i++)
				found[i] = false;
			foreach (Card card in deck.LeftHalf)
				found[card.OrdinalValue()] = true;
			foreach (Card card in deck.RightHalf)
				found[card.OrdinalValue()] = true;
			for (int i = 0; i < 52; i++)
				Assert.IsTrue(found[i]);
		}
		[TestMethod]
		public void TestShuffle()
		{
			Deck deck = new Deck();
			Assert.IsTrue(deckIsOrdered(deck));
			deck.shuffle();
			Assert.IsFalse(deckIsOrdered(deck));
			Assert.IsTrue(AllCardsThere(deck));
		}
		private bool deckIsOrdered(Deck deck)
		{
			int last_ordinal = deck.TheDeck[0].OrdinalValue();
			for (int i = 1; i < 52; i++)
			{
				int next_ordinal = deck.TheDeck[i].OrdinalValue();
				if (next_ordinal != (last_ordinal + 1))
					return false;
				last_ordinal = next_ordinal;
			}
			return true;
		}

		private bool AllCardsThere(Deck deck)
		{
			bool[] found = new bool[52];
			for (int i = 0; i < 52; i++)
				found[i] = false;

			foreach (Card card in deck.TheDeck)
				found[card.OrdinalValue()] = true;

			for (int i = 0; i < 52; i++)
				if (!found[i])
					return false;

			return true;
		}

		[TestMethod]
		public void TestDeal()
		{
			Deck deck = new Deck();
			deck.shuffle();

			bool[] found = new bool[52];
			for (int i = 0; i < 52; i++)
				found[i] = false;
			
			// as the cards are dealt, 
			// make sure the size of the deck is going down

			for (int i = 0; i < 52; i++)
			{
				Card card = deck.dealOneCard();
				found[card.OrdinalValue()] = true;
				Assert.AreEqual(52 - i - 1, deck.Size());
			}

			// check for null when the deck is empty

			Card nullCard = deck.dealOneCard();
			Assert.IsNull(nullCard);

			// check to make sure that every possible card was returned

			for (int i = 0; i < 52; i++)
				Assert.IsTrue(found[i]);
		}
	}
}
