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
            Init();

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
            } while (key != ConsoleKey.Escape || true);
        }

        /// <summary>
        /// Создание списка игроков
        /// </summary>
        private void Init()
        {
            for(; ; )
            { 
                Console.Clear();
                Console.Write("Введите колличество игроков: ");
                if (byte.TryParse(Console.ReadLine(), out _coll) && _coll > 1  && _coll < 7)
                {
                    _players = new Player[_coll];
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Write("Колличество от 2-х до 6-и!");
                    System.Threading.Thread.Sleep(2000);
                    continue;
                }
            }

            for (byte i = 0; i < _coll; i++)
                _players[i] = new Player();
            PlayersInit();
        }

        /// <summary>
        /// Ввод имен игроков
        /// </summary>
        private void PlayersInit()
        {
            char ch;
            for (; ; )
            {
                Console.Clear();
                Console.WriteLine("Вводите разные имена!");
                for (byte i = 0; i < _coll; i++)
                {
                    Console.Write($"Введите имя {i + 1}-го игрока: ");
                    _players[i].Name = Console.ReadLine();
                }
                Console.WriteLine("Вас устраивают имена? (д/н)");
                for (; ; )
                {
                    ch = Console.ReadKey(true).KeyChar;
                    if (ch == 'Д' || ch == 'д')
                    {
                        Console.Clear();
                        return;
                    }
                    if (ch == 'н' || ch == 'Н') break;
                }
            }
        }

        public bool Step(byte playerID)
        {
            bool flag = false;
            PlayerList();
            Console.Write("Введите номер игрока на которого будет совершен ход: ");
            byte num;
            if (byte.TryParse(Console.ReadLine(), out num))
                if (num > 0 && num < _coll) ; 
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
                Console.WriteLine($"{i+1}. Игрок {_players[i].Name}. Карт: {_players[i].HandSize}");
        }
    }
}
