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
        int _weight;
        public int Weight { get => _weight; private set => _weight = value; }

        static string[] masts = { "♠", "♣", "♥", "♦" };
        static string[] names = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "В", "Д", "К", "Т" };

        public Card(int card)
    {
        _mast = card / 13;
        Weight = card % 13;  // Вес карты от 2 до 14 (туз)
    }

    public override string ToString()
    {
        return $"{names[Weight],2} {masts[_mast]}";
    }

}
}
