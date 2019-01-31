using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDeck
{
    class Player
    {
        List<Card>[] _hand;
        string name;
        public string Name { get; set; }

        byte _handSize;
        public byte HandSize { get => _handSize; set => _handSize = value; }

        byte _score;
        public byte Score { get => _score; set => _score = value; }
        

        public Player()
        {
            _hand = new List<Card>[13];
            for (byte i = 0; i < 13; i++)
                _hand[i] = new List<Card>();
            HandSize = 0;
            Score = 0;
        }

        public void TakeCard(CardDeck deck)
        {
            Card card = deck.NextCard;
            _hand[card.Weight].Add(card);
            HandSize++;
        }

        public void DrawHand()
        {
            Console.WriteLine("Ваша колода: ");
            for (byte i = 0; i < 13; i++)
            {
                if (_hand[i].Count != 0)
                {
                    for(byte j = 0; j < _hand[i].Count; j++)
                    {
                        Console.Write($"{_hand[i][j]}");
                    }
                    Console.WriteLine();
                }
            }
        }

        public void TakeAway(byte count, List<Card> cards)
        {
            for (byte i = 0; i < cards.Count; i++)
            {
                _hand[cards[0].Weight].Add(cards[i]);
            }
        }

        private void CloseChest()
        {
            for (byte i = 0; i < HandSize; i++)
                if (_hand[i].Count == 4)
                {
                    Score++;
                    _hand[i].Clear();
                }
        }

    }
}
