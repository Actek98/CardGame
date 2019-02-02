using System;

namespace CardDeck
{
    class Hand
    {

        bool[,] _hand;

        byte _handSize;
        public byte HandSize { get => _handSize; set => _handSize = value; }

        public Hand()
        {
            _hand = new bool[13, 4];
            for (byte i = 0; i < 13; i++)
                for (byte j = 0; j < 4; j++)
                    _hand[i, j] = false;
            HandSize = 0;
        }

        public bool DoHave(byte i, byte j)
        {
            return _hand[i, j];
        }

        public void AddCard(byte i, byte j)
        {
            _hand[i, j] = true;
        }

        public void RemoveCard(byte i, byte j)
        {
            _hand[i, j] = false;
        }

        public bool IsChest(byte i)
        {
            for (byte j = 0; j < 4; j++)
                if (!_hand[i, j])
                    return false;
            return true;
        }

        public void Draw()
        {
            for (byte i = 0; i < 13; i++)
            {
                for (byte j = 0; j < 4; j++)
                    if (_hand[i, j]) Console.Write("{0,2} {1}   ", Game._names[i], Game._masts[j]);
                if (Console.CursorLeft != 0) Console.WriteLine(); 
            }
        }
    }
}
