using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Input
    {
        public Action<float, float> printInputError;
        public Action<float, float> printRangeInstruction;
        public Action printRaceError;
        public Action printStep1;
        public Action printStep2;
        public Action printStep3;
        public Action printStep4;
        public Action printStep5;
        public Action printStep6;
        public Action printStepRace;
        public Action<List<float>> printRemainingStats;
        private int stepID = 1;
        public float GetMonsterFloatInput(float _min, float _max)
        {
            while (true)
            {
                switch (stepID)
                {
                    case 1:
                        printStep1.Invoke();
                        stepID++;
                        break;
                    case 2:
                        printStep2.Invoke();
                        stepID++;
                        break;
                    case 3:
                        printStep3.Invoke();
                        stepID++;
                        break;
                    case 4:
                        printStep4.Invoke();
                        stepID = 1;
                        break;
                }
                printRangeInstruction.Invoke(_min, _max);
                var userInput = Console.ReadLine();
                if (float.TryParse(userInput, out var floatInput) && floatInput >= _min && floatInput <= _max)
                {
                    Console.Clear();
                    return floatInput;
                }
                else
                {
                    stepID--;
                    printInputError.Invoke(_min, _max);
                }
            }
        }
        public int GetMonsterRaceInput(float _min, float _max)
        {
            while (true)
            {
                printStepRace.Invoke();
                var userInput = Console.ReadLine();
                if (int.TryParse(userInput, out var intInput) && intInput >= _min && intInput <= _max)
                {
                    Console.Clear();
                    return intInput;
                }
                else
                {
                    Console.Clear();
                    printInputError.Invoke(_min, _max);
                }
            }
        }

        internal Game.EMonsterRace ChooseDifferentRace(Game.EMonsterRace _race)
        {
            while (true)
            {
                Console.Clear();
                printRaceError.Invoke();
                printStep5.Invoke();
                var userInput = Console.ReadLine();
                if (int.TryParse(userInput, out var intInput) && intInput <= 3 && intInput >= 1 && intInput != (int)_race)
                {
                    return (Game.EMonsterRace)intInput;
                }
            }
        }

        public float GetAnyFloatInput(float _min, float _max)
        {
            while (true)
            {
                var userInput = Console.ReadLine();
                if (int.TryParse(userInput, out var floatInput))
                {
                    return floatInput;
                }
                else
                {
                    printInputError.Invoke(_min, _max);
                }
            }
        }

        public float ChooseStat(List<float> _rolledStats)
        {
            while (true)
            {
                switch (stepID)
                {
                    case 1:
                        printStep1.Invoke();
                        stepID++;
                        break;
                    case 2:
                        printStep2.Invoke();
                        stepID++;
                        break;
                    case 3:
                        printStep3.Invoke();
                        stepID++;
                        break;
                    case 4:
                        printStep4.Invoke();
                        stepID++;
                        break;
                    case 5:
                        printStep5.Invoke();
                        stepID++;
                        break;
                    case 6:
                        printStep6.Invoke();
                        stepID = 1;
                        break;
                }
                printRemainingStats.Invoke(_rolledStats);
                var userInput = Console.ReadLine();
                if (float.TryParse(userInput, out float floatInput))
                {
                    foreach (float stat in _rolledStats)
                    {
                        if (floatInput == stat)
                        {
                            _rolledStats.Remove(stat);
                            return stat;
                        }
                        else
                        {
                            stepID--;
                        }
                    }
                }
                else
                {
                    stepID--;
                }
            }
        }
    }
}
