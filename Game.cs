namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Game
    {
        private bool debug;
        private bool gameRunning = true;
        private Monster monster1;
        private Monster monster2;
        private Input userInput;
        private UI text;
        private delegate void EndGamePrintHandler(Monster _winningMonster, int _roundCount);
        private event EndGamePrintHandler endGamePrint;
        private delegate void ChangeStatHandler(Monster _monsterToChangeStatsOn);
        private Action startGame;
        private int roundCount;

        public List<float> RolledStats = new List<float>();
        private Random dice = new Random();
        private int rolledGenericValue;

        public static bool ShowDiceRolling;
        /*
        Ideas:
        Beholder inklusive all rays (boss?)
         */

        public enum EMonsterRace
        {
            //strenght fighter
            Ork = 1,
            //strength fighter
            Troll = 2,
            //dex fighter
            Goblin = 3,
            //dex fighter
            Centaur = 4,
            //int caster
            Mindflayer = 5,
            //wis caster
            Hag = 6,
            //char caster
            Lich = 7,

        }
        public Game(bool _debug, bool _showDiceRolling)
        {
            debug = _debug;
            ShowDiceRolling = _showDiceRolling;
        }

        public void GameInit()
        {
            userInput = new Input();
            text = new UI();
            text.RegisterInput(userInput);
            text.PrintInstructions();
            monster1 = CreateMonster();
            text.RegisterMonsters(monster1);
            text.PrintNextMonsterText();
            monster2 = CreateMonster();
            text.RegisterMonsters(monster2);
            startGame += text.StartGame;
            endGamePrint += text.PrintEndGame;
            if (monster1.Initiative >= monster2.Initiative)
            {
                GameUpdate(monster1, monster2);
            }
            else if (monster2.Initiative > monster1.Initiative)
            {
                GameUpdate(monster2, monster1);
            }
        }

        //TODO: Max round count
        private void GameUpdate(Monster _firstMonster, Monster _secondMonster)
        {
            startGame.Invoke();
            while (gameRunning)
            {
                if (_firstMonster.HP > 0 && _secondMonster.HP > 0)
                {
                    roundCount++;
                    _firstMonster.Attack(_secondMonster);
                    CheckVictoryCondition(_firstMonster, _secondMonster);
                    if (!gameRunning) break;
                    if (ShowDiceRolling) Console.ReadKey();
                    _secondMonster.Attack(_firstMonster);
                    CheckVictoryCondition(_firstMonster, _secondMonster);
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private void CheckVictoryCondition(Monster _firstMonster, Monster _secondMonster)
        {
            if (_firstMonster.HP <= 0 || _firstMonster.MainUsedStatValue <= 0)
            {
                gameRunning = false;
                endGamePrint.Invoke(_secondMonster, roundCount);
            }
            else if (_secondMonster.HP <= 0 || _secondMonster.MainUsedStatValue <= 0)
            {
                gameRunning = false;
                endGamePrint.Invoke(_firstMonster, roundCount);
            }
        }
        private Monster CreateMonster()
        {
            var statSet = RollMonsterStats();
            int raceInput;
            do
            {
                raceInput = userInput.GetMonsterRaceInput(1, 3);

            } while (monster1 != null && (EMonsterRace)raceInput == monster1.MonsterRace);
            switch ((EMonsterRace)raceInput)
            {
                case EMonsterRace.Ork:
                    return new Ork(userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), 8);
                case EMonsterRace.Troll:
                    return new Troll(userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), 12);
                case EMonsterRace.Goblin:
                    return new Goblin(userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), 6);
                case EMonsterRace.Centaur:
                    return new Centaur(userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), 8);
                case EMonsterRace.Mindflayer:
                    return new Mindflayer(userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), 4);
                case EMonsterRace.Hag:
                    return new Hag(userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), 6);
                case EMonsterRace.Lich:
                    return new Lich(userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), userInput.ChooseStat(statSet), 8);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public List<float> RollMonsterStats()
        {
            for (int i = 0; i < 6; i++)
            {
                rolledGenericValue = 0;
                float stat = RollDice(3, 6);
                RolledStats.Add(stat);
            }
            return RolledStats;
        }

        public int RollDice(int _diceAmount, int _maxDiceValue)
        {
            for (int i = 0; i < _diceAmount; i++)
            {
                rolledGenericValue += dice.Next(1, _maxDiceValue + 1);
            }
            return rolledGenericValue;
        }
    }
}
