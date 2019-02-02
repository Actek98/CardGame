using System;

namespace CardDeck
{
    class CardDeck
    {
        byte[] _deck = {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,
            31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51};

        Random _rnd;

        int _current = 0;

        public bool _empty { get; private set; }

        private void RandomShuffle()
        {
            for (byte i = 0; i < _deck.Length; i++)
            {
                int rnd = _rnd.Next(_deck.Length);
                byte tmp = _deck[i];
                _deck[i] = _deck[rnd];
                _deck[rnd] = tmp;
            }
        }

        public CardDeck(int n = -1)
        {
            if (n < 0)
                _rnd = new Random();
            else
                _rnd = new Random(n);

            _empty = false;
            RandomShuffle();
        }

        public Card NextCard
        {
            get
            {
                if (_current == 51) _empty = true;
                return new Card(_deck[_current++]);
            }
        }

        public void ResetDeck()
        {
            RandomShuffle();
            _current = 0;
            _empty = false;
        }

        public byte Reserv()
        {
            return (byte)(52 - _current);
        }
    }

}
