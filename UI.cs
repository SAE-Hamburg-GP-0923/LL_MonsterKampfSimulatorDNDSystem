
using System.Threading;

namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class UI
    {
        public void PrintErrorMessage(float _min, float _max)
        {
            Console.Clear();
            Console.WriteLine("Bitte gebe einen vernünftigen Wert ein!");
            Console.WriteLine($"Alle Werte dürfen nur zwischen {_min} und {_max} liegen!");
        }

        public void PrintChooseStrength()
        {
            Console.Clear();
            Console.WriteLine("Bitte gebe die Zahl ein, welche du für deine Stärke benutzten willst!");
        }
        public void PrintChooseDexterity()
        {
            Console.Clear();
            Console.WriteLine("Bitte gebe die Zahl ein, welche du für deine Geschicklichkeit benutzten willst!");
        }

        public void PrintChooseConstitution()
        {
            Console.Clear();
            Console.WriteLine("Bitte gebe die Zahl ein, welche du für deine Konstitution benutzten willst!");
        }

        public void PrintChooseIntelligence()
        {
            Console.Clear();
            Console.WriteLine("Bitte gebe die Zahl ein, welche du für deine Intelligenz benutzten willst!");
        }
        private void PrintChooseCharisma()
        {
            Console.Clear();
            Console.WriteLine("Bitte gebe die Zahl ein, welche du für dein Charisma benutzten willst!");
        }

        private void PrintChooseWisdom()
        {
            Console.Clear();
            Console.WriteLine("Bitte gebe die Zahl ein, welche du für deine Weisheit benutzten willst!");
        }

        public void PrintInputRace()
        {
            Console.WriteLine("Bitte wähle die Rasse des Monsters!");
            foreach (int entry in Enum.GetValues(typeof(Game.EMonsterRace)))
            {
                Console.WriteLine($"[{entry}] - {(Game.EMonsterRace)entry}");
            }
        }

        public void PrintNextMonsterText()
        {
            Console.WriteLine("Nun gebe erneut alle Werte ein. Diesesmal für das zweite Monster!");
        }

        public void PrintEndGame(Monster _winningMonster, int _roundCount)
        {
            //Console.Clear();
            Console.WriteLine($"Der {_winningMonster.MonsterName} hat nach {_roundCount} Runden gewonnen!");
        }

        public void PrintHP(Monster _monster)
        {
            Console.WriteLine($"{_monster.MonsterName} hat noch {_monster.HP} Leben!");
        }

        public void PrintDamage(Monster _monster, float _actualDamage)
        {
            Console.WriteLine($"Der {_monster.MonsterName} hat {_actualDamage} Punkte Schaden bekommen!");
        }
        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("Alle Werte sind in Ordnung! Drücke eine beliebige Taste zum beginnen der Simulation!");
            Console.WriteLine("Die Simulation basiert auf Runden! Um die nächste Runde auszuführen drücke eine beliebige Taste nachdem die aktuellen Lebenswerte erschienen sind!");
            Console.ReadKey();
            Console.Clear();
        }
        public void PrintInstructions()
        {
            Console.WriteLine("Willkommen bei dieser kleinen Monster Kampf Simulation!");
            Console.WriteLine("Im folgenden definierst du die folgenden Werte für beide Monster!");
            Console.WriteLine("HP = Lebenspunkte");
            Console.WriteLine("AP = Angriffspunkte");
            Console.WriteLine("DP = Verteidigungspunkte");
            Console.WriteLine("S = Geschwindigkeit");
            Console.WriteLine("Sollten beide Monster gleich viel Geschwindigkeit haben, beginnt das erste welches du erstellt hast.");
            Console.WriteLine("Drücke nun eine beliebige Taste um zu beginnen!");
            Console.ReadKey();
            Console.Clear();

        }

        public void PrintRangeInstruction(float _min, float _max)
        {
            Console.WriteLine($"Der Wert muss zwischen {_min} und {_max} liegen!");
        }
        private void PrintHealSkill()
        {
            Console.WriteLine("Troll benutzt Regeneration!");
        }
        private void PrintDodgeSkill()
        {
            Console.WriteLine("Der Goblin ist ausgewichen!");
        }
        private void PrintCriticalSkill()
        {
            Console.WriteLine("Der Ork landet einen kritischen Treffer!");
        }

        public void RegisterInput(Input _userInput)
        {
            _userInput.printStep1 += PrintChooseStrength;
            _userInput.printStep2 += PrintChooseDexterity;
            _userInput.printStep3 += PrintChooseConstitution;
            _userInput.printStep4 += PrintChooseIntelligence;
            _userInput.printStep5 += PrintChooseWisdom;
            _userInput.printStep6 += PrintChooseCharisma;
            _userInput.printStepRace += PrintInputRace;
            _userInput.printInputError += PrintErrorMessage;
            _userInput.printRangeInstruction += PrintRangeInstruction;
            _userInput.printRemainingStats += PrintRemainingStats;
        }

        private void PrintRemainingStats(List<float> _list)
        {
            Console.WriteLine("Du hast noch folgende Werte zum Verteilen! : ");
            foreach (var stat in _list)
            {
                Console.WriteLine(stat);
            }
        }


        public void RegisterMonsters(Monster _monster)
        {
            _monster.HPPrint += PrintHP;
            _monster.DamagePrint += PrintDamage;
            _monster.PrintDiceRollingAnim += PrintDiceRollingAnim;
            _monster.DamageCalculationPrint += PrintDamageCalculation;
            _monster.DamageReducedPrint += PrintDamageReduction;
            switch (_monster)
            {
                case Ork ork:
                    ork.ActivateCriticalSkill += PrintCriticalSkill;
                    break;
                case Troll troll:
                    troll.ActivateHealSkill += PrintHealSkill;
                    break;
                case Goblin goblin:
                    goblin.ActivateDodgeSkill += PrintDodgeSkill;
                    break;
                case Centaur centaur:
                    centaur.ActivateKickSkill += PrintKickSkill;
                    break;
                case Mindflayer mindflayer:
                    mindflayer.ActivateDrainStatSkill += PrintDrainSkill;
                    mindflayer.ActivateGrappleSkill += PrintGrappleSkill;
                    break;
                case Hag hag:
                    hag.ActivateMirrorImageSkill += PrintMirrorImageSkill;
                    break;
                case Lich lich:
                    lich.ActivateReviveSkill += PrintReviveSkill;
                    break;
            }
        }

        private void PrintReviveSkill()
        {
            Console.WriteLine("Der untote Magier belebt sich wieder!");
        }

        private void PrintMirrorImageSkill()
        {
            Console.WriteLine("Die Vettel benutzt den Zauber 'Spiegelbild'!");
        }

        private void PrintGrappleSkill()
        {
            Console.WriteLine("Der Mindflayer ergreift sein Opfer und hält es fest!");
        }

        private void PrintDrainSkill()
        {
            Console.WriteLine("Der Mindflayer entzieht seinem Opfer Kraft!");
        }

        private void PrintKickSkill()
        {
            Console.WriteLine("Der Zentaur konternt mit einem Tritt");
        }

        private void PrintDamageReduction(float _armorValue, Monster _monster)
        {
            Console.WriteLine($"Der {_monster.MonsterName} verhindert {_armorValue} an Schaden!");
        }

        private void PrintDamageCalculation(float _actualDamage, Monster _monster)
        {
            Console.WriteLine($"Der {_monster.MonsterName} kommt auf einen Gesamtwert von {_actualDamage} Schaden!");
        }

        private void PrintDiceRollingAnim(float _rolledValue, Monster _monster)
        {
            for (int i = 0; i < 6; i++)
            {
                Random random = new Random();
                Console.Clear();
                Console.Write(random.Next(1, 20));
                Thread.Sleep(50);
            }
            Console.Clear();
            Console.WriteLine($"Der {_monster.MonsterName} hat eine {_rolledValue} gewürfelt!");
        }
    }
}
