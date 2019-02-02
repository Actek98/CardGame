using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDeck
{
    class Player
    {
        Hand _hand;
        string _name;
        public string Name { get => _name; set => _name = value; }

        byte _handSize;
        public byte HandSize { get => _handSize; set => _handSize = value; }

        byte _score;
        public byte Score { get => _score; set => _score = value; }
        

        public Player()
        {
            _hand = new Hand();
            Score = 0;
        }

        /// <summary>
        /// Взять в руку карту из колоды
        /// </summary>
        /// <param name="deck">Колода из которой берется карта.</param>
        public void TakeCard(CardDeck deck)
        {
            if (!deck._empty)
            {
                Card card = deck.NextCard;
                _hand.AddCard((byte)card.Weight, (byte)card.Mast);
                HandSize++;
                Console.WriteLine($"Игрок {Name} взял карту из колоды.");
            }
        }

        /// <summary>
        /// Взять определенную карту в руку
        /// </summary>
        /// <param name="i">Вес карты в диапазоне 0 - 12 (2 - Туз).</param>
        /// <param name="j">Масть карты в диапазоне 0 - 3 (пики, трефы, чирвы, бубны).</param>
        public void TakeCard(byte i, byte j)
        {
            _hand.AddCard(i, j);
            HandSize++;
        }

        /// <summary>
        /// Убрать карту из руки
        /// </summary>
        /// <param name="i">Вес карты в диапазоне 0 - 12 (2 - Туз).</param>
        /// <param name="j">Масть карты в диапазоне 0 - 3 (пики, трефы, чирвы, бубны).</param>
        public void RemoveCard(byte i, byte j)
        {
            _hand.RemoveCard(i, j);
            HandSize--;
        }

        /// <summary>
        /// Ищет карты определенного дсотоинства у игрока
        /// </summary>
        /// <param name="weight">Достоинство карты</param>
        /// <returns>Число, мадшие 4 бита которого показывают наличие мастей</returns>
        public byte DoHaveCard(byte weight)
        {
            int masts = 0;
            for (byte i = 0; i < 4; i++)
            {
                masts = masts >> 1;
                if (_hand.DoHave(weight, i))
                    masts |= 8;
            }
            return (byte)masts;
        }

        /// <summary>
        /// Прорисовка карт в руке игрока 
        /// </summary>
        public void DrawHand()
        {
            Console.WriteLine("Ваша колода: ");
            _hand.Draw();
        }

        /// <summary>
        /// Закрыть существующие сундуки
        /// </summary>
        public void CloseChest()
        {
            for (byte i = 0; i < 13; i++)
                if (_hand.IsChest(i))
                {
                    for (byte j = 0; j < 4; j++)
                        RemoveCard(i, j);
                    Score++;
                    Console.WriteLine($"Игрок {Name} сложил сундучок. ");
                }
        }

    }
}
