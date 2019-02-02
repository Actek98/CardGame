using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDeck
{
    class Card
    {
        int _mast;
        public int Mast { get => _mast; set => _mast = value; }
        int _weight;
        public int Weight { get => _weight; private set => _weight = value; }

        public Card(int card)
        {
            Mast = card / 13;
            Weight = card % 13;  // Вес карты от 2 до 14 (туз)
        }

    }
}
