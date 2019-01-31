using System;

namespace CardDeck
{
    class Game
    {
        CardDeck _cardDeck;
        Player[] _players;
        byte _coll;
        bool _isEnd;

        public Game()
        {
            
            _cardDeck = new CardDeck();
        }

        public void BeginGame()
        {
            
            Console.Write("Введите колличество игроков: ");
            _coll = byte.Parse(Console.ReadLine());
            _players = new Player[_coll];

            for (byte i = 0; i < _coll; i++)
            {
                Console.Write($"Введите имя {i + 1}-го игрока: ");
                _players[i] = new Player(Console.ReadLine());
                _players[i].TakeCard(_cardDeck);
                _players[i].TakeCard(_cardDeck);
                _players[i].TakeCard(_cardDeck);
                _players[i].TakeCard(_cardDeck); //начальная рука 4 карты
            }

            Console.Clear();
            ConsoleKey key;
            do
            {
                for (byte i = 0; i < _coll; i++)
                {
                    Step(i);
                    if (true) ;
                }
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Escape || );
        }

        public bool Step(byte playerID)
        {
            bool flag = false;
            PlayerList();
            Console.Write("Введите номер игрока на которого будет совершен ход: ");
            byte num;
            if (byte.TryParse(Console.ReadLine(), out num))
                if (num > 0 && num < _coll) //;
            return flag;
        }

        public bool Atack(Player master, Player slave)
        {
            bool flag = false;

            return flag;
        }

        public void PlayerList()
        {
            Console.WriteLine("Список игроков:");
            for (byte i = 0; i < _coll; i++)
                Console.WriteLine($"{i+1}. Игрок {_players[i].Name}: {_players[i].HandSize} карт");
        }
    }
}
