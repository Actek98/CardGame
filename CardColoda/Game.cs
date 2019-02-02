using System;

namespace CardDeck
{

    class Game
    {
        CardDeck _cardDeck;
        Player[] _players;
        byte _coll;

        public Game()
        {
            _cardDeck = new CardDeck();
        }

        public void BeginGame()
        {
            Init();
            Start();
            Console.Clear();
            ConsoleKey key;
            do
            {
                for (byte i = 0; i < _coll; i++)
                {
                   if (Step(i)) break;

                }
                Console.Write("Hi");
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Escape && !IsEnd());
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
                    System.Threading.Thread.Sleep(1000);
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

        /// <summary>
        /// Начальная сдача карт
        /// </summary>
        private void Start()
        {
            foreach (Player player in _players)
            {
                player.TakeCard(_cardDeck);
                player.TakeCard(_cardDeck);
                player.TakeCard(_cardDeck);
                player.TakeCard(_cardDeck);
                player.CloseChest();
            }
        }

        public bool Step(byte playerID)
        {
            bool flag = false;
            for (int i = playerID + 1; ; i++)
            {
                i %= _coll;
                if (i == playerID) i++;
                Console.Clear();
                PlayerList();
                Console.WriteLine();

                _players[playerID].DrawHand();
                Console.WriteLine();

                Console.WriteLine($"Ход на игрока: {_players[i].Name}");
                Atack(_players[playerID], _players[i]);

                Console.ReadKey();

                if (IsEnd()) return true;
            }
            return flag;
        }

        public bool Atack(Player master, Player slave)
        {
            bool flag = false;
            string s;
            byte I, J;
            Console.WriteLine("Введите достоинство карты: ");
            for (; ; )
            {
                s = Console.ReadLine();
                if (s.Length == 0)
                {
                    Console.Write("Не оставляйте простую строку!");
                    System.Threading.Thread.Sleep(1000);
                    Console.SetCursorPosition(0, Console.CursorTop-1);
                    continue;
                }
                switch (s[0])
                {
                    case '1':
                            
                        break;
                    case 'В':
                        I = 9;
                        break;
                    case 'Д':
                        I = 10;
                        break;
                    case 'К':
                        I = 11;
                        break;
                    case 'Т':
                        I = 12;
                        break;
                }
            }

            return flag;
        }

        private bool IsEnd()
        {
            byte score = 0;
            foreach (Player player in _players)
                score += player.Score;
            if (score >= 13) return true;
            return false;
        }

        public void PlayerList()
        {
            Console.WriteLine("Список игроков:");
            for (byte i = 0; i < _coll; i++)
                Console.WriteLine($"{i+1}. Игрок {_players[i].Name}. Карт: {_players[i].HandSize}. Счет: {_players[i].Score}.");
            Console.WriteLine();
        }
    }
}
