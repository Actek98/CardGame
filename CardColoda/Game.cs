using System;

namespace CardDeck
{

    class Game
    {
        public static readonly string[] _masts = { "♠", "♣", "♥", "♦" };
        public static readonly string[] _mastsDecrypt = { "ПИКА", "ТРЕФА", "ЧИРВА", "БУБНА" };
        public static readonly string[] _names = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "В", "Д", "К", "Т" };

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
                    Console.Clear();
                    Step(i);
                    if (IsEnd()) break;
                }
            } while (!IsEnd());
        }

        public void EndGame()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Игра закончена!");
            PlayerScore();
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

        /// <summary>
        /// Ход игрока до неудачи
        /// </summary>
        /// <param name="playerID">Номер игрока</param>
        public void Step(byte playerID)
        {
            bool flag = false;
            for (int i = playerID + 1; ; i++)
            {
                if (i == playerID) i++;
                i %= _coll;
                if (i == playerID) i++;
                Console.Clear();
                PlayerList();
                Console.WriteLine($"Карт в колоде: {_cardDeck.Reserv()}");
                Console.WriteLine($"Ходит игрок: {_players[playerID].Name}");
                Console.WriteLine($"Ход на игрока: {_players[i].Name}");
                _players[playerID].DrawHand();
                Console.ReadKey();

                Console.Clear();
                if (_players[i].HandSize == 0)
                {
                    Console.WriteLine($"У игрока {_players[i].Name} нет карт! Ход на следующего игрока.");
                    System.Threading.Thread.Sleep(1500);
                    continue;
                }
                //_players[playerID].DrawHand(); 
                // Либо нужно планировать ход заранее, либо противник будет видеть твои карты во время твоего хода.

                flag = Atack(_players[playerID], _players[i]);
                _players[playerID].CloseChest();
                if (_players[i].HandSize == 0) _players[i].TakeCard(_cardDeck);
                Console.ReadKey();
                if (IsEnd()) return;
                if (!flag)
                {
                    Console.WriteLine("Переход хода.");
                    Console.ReadKey();
                    break;
                }
            }
        }

        /// <summary>
        /// Ход одного игрока на другого
        /// </summary>
        /// <param name="master">Игрок совершающий ход</param>
        /// <param name="slave">Игрок на которого совершают ход</param>
        /// <returns></returns>
        public bool Atack(Player master, Player slave)
        {
            bool flag = false;
            int masts;
            byte weight, coll = 0;
            weight = CardWeight();
            masts = slave.DoHaveCard(weight);
            if (masts > 0)
            {
                Console.WriteLine($"У игрока {slave.Name} есть карта(ы) достоинства {_names[weight]}");
                int tmp = masts;
                while (tmp != 0)
                {
                    coll += (byte)(tmp & 1);
                    tmp = tmp >> 1;
                }
                if (coll == CardColl())
                {
                    Console.WriteLine($"У игрока {slave.Name} есть {coll} карт достоинства {_names[weight]}");
                    if (masts == CardMast(coll)) 
                    {
                        Console.WriteLine($"Вы угадали и получаете эти карты!");
                        for (byte i = 0; i < 4; i++)
                        {
                            if (((masts >> i) & 1) == 1)
                            {
                                master.TakeCard(weight, i);
                                slave.RemoveCard(weight, i);
                            }
                        }
                        return true;
                    }
                    else
                        Console.WriteLine("Вы не угадали масти.");
                }
                else
                    Console.WriteLine("Вы не угадали количество.");
            }
            else
                Console.WriteLine($"У игрока {slave.Name} нет карт достоинства {_names[weight]}");

            master.TakeCard(_cardDeck);
            return false;
        }

        /// <summary>
        /// Запрос веса карты с защитой от дурака
        /// </summary>
        /// <returns>Вес карты</returns>
        private byte CardWeight()
        {
            string s;
            byte weight = 20;
            for (; ; )
            {
                Console.Write("Введите достоинство карты: ");
                s = string.Empty;
                s = Console.ReadLine().ToUpper();
                for (byte i = 0; i < 13; i++)
                {
                    //if (s.CompareTo(_names[i]) == 0)
                    if (s == _names[i])
                    {
                        weight = i;
                        break;
                    }
                }
                if (weight == 20)
                {
                    Console.Write("Допустимые значения: 2, 3, 4, 5, 6, 7 ,8 ,9 ,10, В, Д, К, Т!");
                    System.Threading.Thread.Sleep(1500);
                    InputClear();
                    continue;
                }
                return weight;
            }
        }

        /// <summary>
        /// Ввод колличества карт с защитой от дурака
        /// </summary>
        /// <returns>Колличество карт</returns>
        private byte CardColl()
        {
            byte coll = 20;
            
            for (; ; )
            {
                Console.Write("Введите колличество карт: ");
                if (byte.TryParse(Console.ReadLine(), out coll) && coll > 0 && coll < 5) return coll;
                else
                {
                    Console.Write("Допустимые значения: 1, 2, 3, 4");
                    System.Threading.Thread.Sleep(1500);
                    InputClear();
                    continue;
                }
            }
        }

        /// <summary>
        /// Ввод мастей карт
        /// </summary>
        /// <param name="coll">Колличество мастей которое будет введено</param>
        /// <returns></returns>
        private int CardMast(byte coll)
        {
            string s;
            int masts = 0;
            int oldmasts = masts;
            for (byte i = 0; i < coll; i++)
            {
                
                for (; ; )
                {
                    Console.Write("Введите масть карты: ");
                    s = string.Empty;
                    s = Console.ReadLine().ToUpper();
                    for (byte j = 0; j < 4; j++)
                    {
                        if (s == _mastsDecrypt[j])
                        {
                            masts |= 1 << j;
                            break;
                        }
                    }
                    if (oldmasts == masts)
                    {
                        Console.Write("Допустимые значения: пика, трефа, чирва, буба!");
                        System.Threading.Thread.Sleep(1500);
                        InputClear();
                        continue;
                    }
                    else break;
                }
                oldmasts = masts;
            }
            return masts;
        }

        /// <summary>
        /// Проверка собраны ли все сундуки в игре
        /// </summary>
        private bool IsEnd()
        {
            byte score = 0;
            foreach (Player player in _players)
                score += player.Score;
            if (score >= 13) return true;
            return false;
        }

        /// <summary>
        /// Вывод списка игроков с размером колод и текущим счетом
        /// </summary>
        private void PlayerList()
        {
            Console.WriteLine("Список игроков:");
            for (byte i = 0; i < _coll; i++)
                Console.WriteLine($"{i+1}. Игрок {_players[i].Name}. Карт: {_players[i].HandSize}. Счет: {_players[i].Score}.");
        }

        /// <summary>
        /// Вывод счета в конце игры
        /// </summary>
        private void PlayerScore()
        {
            Console.WriteLine("Счет:");
            for (byte i = 0; i < _coll; i++)
                Console.WriteLine($"{i + 1}. Игрок {_players[i].Name}. Счет: {_players[i].Score}.");
        }

        /// <summary>
        /// Очистка неправильно введенных пользователем значений из консоли
        /// </summary>
        private void InputClear()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new String(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 2);
            Console.Write(new String(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}
