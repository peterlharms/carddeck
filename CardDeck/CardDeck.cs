using System;
using System.Collections.Generic;

namespace CardDeck
{
	public enum Suit
	{
		Clubs = 0,
		Diamonds = 1,
		Hearts = 2,
		Spades = 3
	}

	public enum Value
	{
		Two = 2,
		Three = 3,
		Four = 4,
		Five = 5,
		Six = 6,
		Seven = 7,
		Eight = 8,
		Nine = 9,
		Ten = 10,
		Jack = 11,
		Queen = 12,
		King = 13,
		Ace = 14
	}
	public class Card
	{
		private Value _value;
		private Suit _suit;

		public Card(Value newValue, Suit newSuit)
		{
			_suit = newSuit;
			_value = newValue;
		}

		public Value CardValue
		{
			get
		{
			return _value;
		}
			protected set
			{
				_value = value;
			}
		}

		public Suit CardSuit
		{
			get
			{
				return _suit;
			}
			protected set
			{
				_suit = value;
			}
		}

		public int OrdinalValue
		{
			get
			{
				return ((int)_suit * 13 + (int)_value - 2);
			}
		}
	}

	public class Deck
	{
		private List<Card> _deck;
		public List<Card> TheDeck
		{
			get
			{
				return _deck;
			}
			protected set
			{
				_deck = value;
			}
		}
	
		// for shuffling
		private List<Card> _left;
		public List<Card> LeftHalf
		{
			get
			{
				return _left;
			}
			private set
			{
				_left = value;
			}
		}
		private List<Card> _right;
		public List<Card> RightHalf
		{
			get
			{
				return _right;
			}
			private
				set
			{
				_right = value;
			}
		}
		private Random _random = new Random();

		public Deck()
		{
			_deck = new List<Card>();

			foreach (Suit suit in Enum.GetValues(typeof(Suit)))
			{
				foreach (Value value in Enum.GetValues(typeof(Value)))
				{
					_deck.Add(new Card(value, suit));
				}
			}
		}

		public int Size()
		{
			if (_deck == null)
				return 0;
			return _deck.Count;
		}

		public Card GetCard(int n)
		{
			return _deck[n];
		}

		public void DivideDeck()
		{
			// cut deck into left and right
			// must be at least one on each side,
			// so cut point is from 0 (one card on left)
			// to 50 (51 cards on left, one on right)

			int _cutPoint = _random.Next(51);  // returns 0 to 50
			int nLeft = _cutPoint + 1;
			int nRight = 52 - nLeft;

			_left = new List<Card>();
			for (int i = 0; i < nLeft; i++)
				_left.Add(_deck[i]);

			_right = new List<Card>();
			for (int i = 0; i < nRight; i++)
				_right.Add(_deck[i + nLeft]);
		}

		private void SwapLeftRight()
		{
			// put together right and left in reverse order
			// (right first)

			_deck = new List<Card>(52);
			_deck.AddRange(_right);
			_deck.AddRange(_left);
		}
		private void RandomMerge()
		{
			// What you do with your hands when you shuffle...
			// take a few cards from each side,
			// alternating from side to side,
			// until there are no cards from one side,
			// then take the remainder from the other side

			_deck = new List<Card>(52);

			int nLeft = _left.Count;
			int nRight = _right.Count;
			int xLeft = 0;
			int xRight = 0;
			bool left = true;
			while (nLeft > 0 || nRight > 0)
			{
				// take from 1 to 6 cards at a time (arbitrary)
				int nCards = _random.Next(6) + 1;
				int nTake = nCards;
				if (left)
				{
					if (nTake > nLeft) nTake = nLeft;
					for (int i = 0; i < nTake; i++)
						_deck.Add( _left[xLeft++] );
					nLeft = nLeft - nTake;
				}
				else
				{
					if (nTake > nRight) nTake = nRight;
					for (int i = 0; i < nTake; i++)
						_deck.Add( _right[xRight++] );
					nRight = nRight - nTake;
				}
				left = !left;
			}

		}

		public void shuffle()
		{
			// number of passes is random number from 5 to 10 (arbitrary)
			int npass = _random.Next(6) + 5;

			// for each pass, cut and random merge

			for (int i = 0; i < npass; i++)
			{
				DivideDeck();
				RandomMerge();
			}

			// Final cut and swap

			DivideDeck();
			SwapLeftRight();
		}

		public Card dealOneCard()
		{
			Card last = null;
			if (_deck.Count > 0)
			{
				last = _deck[_deck.Count - 1];
				_deck.RemoveAt(_deck.Count - 1);
			}
			return last;
		}
		
	}
}


