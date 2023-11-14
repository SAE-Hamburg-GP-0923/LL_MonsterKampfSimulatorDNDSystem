namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Input
    {
        #region Action and help variable
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
        #endregion

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
                    //var currentStepID = stepID - 1;
                    foreach (float stat in _rolledStats)
                    {
                        if (floatInput == stat)
                        {
                            _rolledStats.Remove(stat);
                            return stat;
                        }
                    }
                    stepID--;
                }
                else
                {
                    stepID--;
                }
            }
        }
    }
}
