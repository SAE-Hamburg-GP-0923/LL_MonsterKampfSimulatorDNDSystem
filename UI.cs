namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class UI
    {
        private ConsoleColor damageColor = ConsoleColor.Red;
        private ConsoleColor armorColor = ConsoleColor.Blue;
        private ConsoleColor hpColor = ConsoleColor.White;
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
            Console.Clear();
            Console.WriteLine("Nun gebe erneut alle Werte ein. Diesesmal für das zweite Monster!");
        }

        public void PrintEndGame(Monster _winningMonster, float _roundCount)
        {
            if (_winningMonster.BossRace != Game.EBossRace.Beholder)
            {
                ConsoleWriteColorLine($"{_winningMonster.MonsterName} hat nach {_roundCount} Runden gewonnen!", _winningMonster.MonsterColor);
            }
            else
            {
                ConsoleWriteColorLine($"Das Monster hat {_roundCount} Runden gegen den Beholder überlebt bevor es kläglich und einsam gestorben ist!", _winningMonster.MonsterColor);
            }
        }

        public void PrintHP(Monster _monster)
        {
            ConsoleWriteColor($"{_monster.MonsterName} hat noch ", _monster.MonsterColor);
            ConsoleWriteColor(_monster.HP.ToString(), hpColor);
            ConsoleWriteColorLine(" Leben!", _monster.MonsterColor);
        }

        public void PrintDamage(float _actualDamage, Monster _monster)
        {
            ConsoleWriteColor($"{_monster.MonsterName} hat ", _monster.MonsterColor);
            ConsoleWriteColor(_actualDamage.ToString(), damageColor);
            ConsoleWriteColorLine(" Schaden bekommen!", _monster.MonsterColor);
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
            Console.WriteLine("Im folgenden werden 6 Zahlen für das Monster ausgewürfelt!");
            Console.WriteLine(" ");
            Console.WriteLine("Die Werte sind Stärke, Geschicklichkeit, Konstitution, Intelligenz, Weisheit und Charisma!");
            Console.WriteLine("Du darfst diese gewürfelten Zahlen dann beliebig auf diese Werte verteilen!");
            Console.WriteLine(" ");
            Console.WriteLine("Jede Monsterrasse hat einen Hauptwert:");
            Console.WriteLine("Ork & Troll = Stärke");
            Console.WriteLine("Goblin & Zentaur = Geschicklichkeit");
            Console.WriteLine("Mindflayer = Intelligenz");
            Console.WriteLine("Hag = Weisheit");
            Console.WriteLine("Lich = Charisma");
            Console.WriteLine(" ");
            Console.WriteLine("Drücke nun eine beliebige Taste um zu beginnen!");
            Console.ReadKey();
            Console.Clear();

        }

        public void PrintRangeInstruction(float _min, float _max)
        {
            Console.WriteLine($"Der Wert muss zwischen {_min} und {_max} liegen!");
        }
        private void PrintHealSkill(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} benutzt Regeneration!", _monster.MonsterColor);
        }
        private void PrintDodgeSkill(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} ist ausgewichen!", _monster.MonsterColor);
        }
        private void PrintCriticalSkill(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} landet einen kritischen Treffer!", _monster.MonsterColor);
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
            _monster.PrintIsStunned += PrintIsStunned;
            _monster.PrintPetrificationState += PrintPetrificationState;
            _monster.PrintIsPetrified += PrintIsPetrified;
            _monster.PrintIsFeared += PrintIsFeared;
            _monster.PrintIsCharmed += PrintIsCharmed;
            _monster.PrintIsSlowed += PrintIsSlowed;
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
                    hag.MirrorImageHit += PrintMirrorImageHit;
                    break;
                case Lich lich:
                    lich.ActivateReviveSkill += PrintReviveSkill;
                    lich.ActivateCurseSkill += PrintCurseSkill;
                    lich.CurseEffectPrint += PrintCurseEffect;
                    break;
                case Beholder beholder:
                    beholder.ActivateParalysingRay += PrintParalysingRay;
                    beholder.ActivateEnervationRay += PrintEnervationRay;
                    beholder.ActivateDisintegrationRay += PrintDisintegrationRay;
                    beholder.ActivateDeathRay += PrintDeathRay;
                    beholder.ActivatePetrificationRay += PrintPetrificationRay;
                    beholder.ActivateSlowRay += PrintSlowRay;
                    beholder.ActivateFearRay += PrintFearRay;
                    beholder.ActivateCharmRay += PrintCharmRay;
                    break;
            }
        }

        private void PrintIsSlowed(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} ist verlangsamt und wüfelt daher mit Nachteil!", _monster.MonsterColor);
        }

        private void PrintCharmRay(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} betört seinen Gegner mit einem schicken Augenblinzeln!", _monster.MonsterColor);
        }

        private void PrintIsCharmed(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} ist vom Gegner betört und macht nur halben Schaden!", _monster.MonsterColor);
        }

        private void PrintIsFeared(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} hat Angst und kann nur seinen normalen Angriff benutzen!", _monster.MonsterColor);
        }

        private void PrintFearRay(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} verängstigt seinen Gegner!", _monster.MonsterColor);
        }

        private void PrintSlowRay(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} hat seinen Gegner verlangsamt!", _monster.MonsterColor);
        }

        private void PrintIsPetrified(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} wurde komplett Versteinert und ist tot!", _monster.MonsterColor);
        }

        private void PrintPetrificationState(Monster _monster, int _maxPetrifiedCount)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} hat noch {_maxPetrifiedCount - _monster.PetrifiedCounter} Runden bis zur vollständigen Versteinerung!", _monster.MonsterColor);
        }

        private void PrintPetrificationRay(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} beginnt seinen Gegner zu versteinern!", _monster.MonsterColor);
        }

        private void PrintEnervationRay(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} schießt einen Strahl negativer Energie auf seinen Gegner!", _monster.MonsterColor);
        }

        private void PrintDisintegrationRay(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} nutzt seinen stärksten Angriff!", _monster.MonsterColor);
        }

        private void PrintDeathRay(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} trifft seinen Gegner mit dunkler Magie!", _monster.MonsterColor);
        }

        private void PrintParalysingRay(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} betäubt seinen Gegner für eine Runde!", _monster.MonsterColor);
        }

        private void PrintIsStunned(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} ist betäubt und kann nicht angreifen!", _monster.MonsterColor);
        }

        private void PrintCurseEffect(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} reduziert den erhaltenen Schaden um die Hälfte, da der Angreifer verflucht ist!", _monster.MonsterColor);
        }

        private void PrintMirrorImageHit(Hag _monster)
        {
            ConsoleWriteColorLine("Es wurde eins der Spiegelbilder getroffen!", _monster.MonsterColor);
            ConsoleWriteColorLine($"Es sind noch {_monster.CurrentMirrorImages} vorhanden!", _monster.MonsterColor);
        }

        private void PrintCurseSkill(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} verflucht sein Opfer!", _monster.MonsterColor);
        }

        private void PrintReviveSkill(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} belebt sich wieder!", _monster.MonsterColor);
        }

        private void PrintMirrorImageSkill(Monster _monster, string _spellName)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} benutzt den Zauber '{_spellName}'!", _monster.MonsterColor);
        }

        private void PrintGrappleSkill(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} ergreift sein Opfer und hält es fest!", _monster.MonsterColor);
        }

        private void PrintDrainSkill(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} entzieht seinem Opfer Kraft!", _monster.MonsterColor);
        }

        private void PrintKickSkill(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} konternt mit einem Tritt", _monster.MonsterColor);
        }

        private void PrintDamageReduction(float _armorValue, Monster _monster)
        {
            ConsoleWriteColor($"{_monster.MonsterName} verhindert ", _monster.MonsterColor);
            ConsoleWriteColor(_armorValue.ToString(), armorColor);
            ConsoleWriteColorLine(" Punkte an Schaden!", _monster.MonsterColor);
        }

        private void PrintDamageCalculation(float _actualDamage, Monster _monster)
        {
            ConsoleWriteColor($"{_monster.MonsterName} kommt auf einen Gesamtwert von ", _monster.MonsterColor);
            ConsoleWriteColor(_actualDamage.ToString(), damageColor);
            ConsoleWriteColorLine(" Punkten an Schaden!", _monster.MonsterColor);
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
            Console.WriteLine($"{_monster.MonsterName} hat eine {_rolledValue} gewürfelt!");
        }

        public void PrintEndGameDraw()
        {
            Console.WriteLine("Das Spiel hat in einem Unentschieden geendet, da die maximale Anzahl an Runden erreicht wurde!");
        }
        public void StartBossFight(Monster _monster)
        {
            ConsoleWriteColorLine($"{_monster.MonsterName} hat zwar überlebt, doch aus den Schatten schiebt sich ein gewaltiges Wesen!", _monster.MonsterColor);
            ConsoleWriteColorLine($"Ein Boss erscheint! Zur Sicherheit, trinkt {_monster.MonsterName} alle seine Heiltränke um wieder mit vollem Leben zu starten!", _monster.MonsterColor);
            ConsoleWriteColorLine($"Drücke nun eine beliebige Taste um den finalen Kampf zu beginnen!", _monster.MonsterColor);
            Console.ReadKey();
            Console.Clear();
        }

        public static void ConsoleWriteColor(string _output, ConsoleColor _color)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = _color;
            Console.Write(_output);
            Console.ForegroundColor = currentColor;
        }

        public static void ConsoleWriteColorLine(string _output, ConsoleColor _color)
        {
            ConsoleWriteColor(_output + "\n", _color);
        }

    }
}
