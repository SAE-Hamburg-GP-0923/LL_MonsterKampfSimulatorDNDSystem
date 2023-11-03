using System.Runtime.Intrinsics.Arm;

namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Monster
    {
        protected float hp;
        public float HP
        {
            get { return hp; }
            protected set
            {
                if (hp != value)
                {
                    hp = MathF.Ceiling(value);
                    HPPrint.Invoke(this);
                }

            }
        }

        protected float maxHP;
        public float MaxHP => maxHP;
        protected float strength;
        public float Strength => strength;
        protected float dexterity;
        public float Dexterity => dexterity;
        protected float initiative;
        public float Initiative => initiative;
        protected float constitution;
        public float Constitution => constitution;

        protected float intelligence;
        public float Intelligence => intelligence;
        protected float wisdom;
        public float Wisdom => wisdom;

        protected float charisma;
        public float Charisma => charisma;
        protected float armor;
        public float Armor => armor;

        protected ConsoleColor monsterColor;
        public ConsoleColor MonsterColor => monsterColor;

        protected float rolledValue;
        protected int maxDiceValue;
        protected float mainUsedStatValue;
        public float MainUsedStatValue => mainUsedStatValue;
        protected Random monsterDice = new Random();
        protected float usedAttackDiceAmount;
        public float UsedAttackDiceAmount => usedAttackDiceAmount;

        public Game.EMonsterRace MonsterRace;
        protected string monsterName;
        public Game.EBossRace BossRace;
        public string MonsterName => monsterName;
        public Action<float, Monster> DamagePrint;
        public Action<Monster> HPPrint;
        public Action<float, Monster> PrintDiceRollingAnim;
        public Action<float, Monster> DamageCalculationPrint;
        public Action<float, Monster> DamageReducedPrint;
        public Action<Monster> PrintIsStunned;
        public Action<Monster, int> PrintPetrificationState;
        public Action<Monster> PrintIsPetrified;
        public Action<Monster> PrintIsFeared;
        public Action<Monster> PrintIsCharmed;

        protected int petrifiedCounter;
        public int PetrifiedCounter => petrifiedCounter;
        private bool startPetrified;
        public bool StartPetrified => startPetrified;

        private bool isCreated;
        protected bool hasAttacked;
        protected bool isStunned;
        protected bool hasDisadvantage;
        protected bool isFeared;
        protected bool isCharmed;


        private int maxPetrifiedCount = 10;
        public bool HasAttacked => hasAttacked;

        public Monster(float _strength, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue)
        {
            strength = _strength;
            dexterity = _dexterity;
            constitution = _constitution;
            intelligence = _intelligence;
            wisdom = _wisdom;
            charisma = _charisma;
            maxDiceValue = _maxDiceValue;
            initiative = _dexterity;
            usedAttackDiceAmount = 5;
        }
        public virtual void Attack(Monster _creatureToHit)
        {
            if (startPetrified)
            {
                petrifiedCounter++;
                PrintPetrificationState.Invoke(this, maxPetrifiedCount);
            }
            if (petrifiedCounter >= maxPetrifiedCount)
            {
                HP = 0;
                PrintIsPetrified.Invoke(this);
                return;
            }
            if (isStunned)
            {
                if (isStunned) PrintIsStunned.Invoke(this);
                RemoveConditions();
            }
            else
            {
                RemoveConditions();
                if (!hasAttacked) hasAttacked = true;
                var damage = MathF.Max(0, RollMonsterDice(MathF.Max(usedAttackDiceAmount - Game.RoundCount,1), maxDiceValue) + CalculateModifier(mainUsedStatValue));
                if (isCharmed)
                {
                    PrintIsCharmed.Invoke(this);
                    DamageCalculationPrint.Invoke(MathF.Floor(damage / 2), this);
                    _creatureToHit.TakeDamage(MathF.Floor(damage / 2) , this);
                    isCharmed = false;
                }
                else
                {
                    DamageCalculationPrint.Invoke(damage, this);
                    _creatureToHit.TakeDamage(damage, this);
                }
            }
        }

        public virtual void TakeDamage(float _damageTaken, Monster _attackingMonster, bool _isCritical = false)
        {
            float actualDamage;
            actualDamage = MathF.Max(_damageTaken - armor, 0);
            if (_damageTaken > 0) DamageReducedPrint.Invoke(armor, this);
            DamagePrint.Invoke(actualDamage, this);
            HP = MathF.Max(0, HP - actualDamage);
        }

        public float RollMonsterDice(float _diceAmount, int _maxDiceValue)
        {
            rolledValue = 0;
            for (int i = 0; i < _diceAmount; i++)
            {
                if (hasDisadvantage)
                {
                    rolledValue += MathF.Min(monsterDice.Next(1, _maxDiceValue + 1), monsterDice.Next(1, _maxDiceValue + 1));
                    hasDisadvantage = false;
                }
                else
                {
                    rolledValue += monsterDice.Next(1, _maxDiceValue + 1);
                }
                if (isCreated && Game.ShowDiceRolling) PrintDiceRollingAnim.Invoke(rolledValue, this);
            }
            return rolledValue;
        }

        public float CalculateModifier(float _flatValue)
        {
            return MathF.Floor((_flatValue - 10f) / 2);
        }

        public float RollMonsterHP(int _diceAmount, int _maxDiceValue, float _flatConstitution)
        {
            float rolledValue = RollMonsterDice(_diceAmount, _maxDiceValue);
            for (int i = 0; i < _diceAmount; i++)
            {
                rolledValue += CalculateModifier(_flatConstitution);
            }
            this.isCreated = true;
            return MathF.Max(rolledValue, (((_maxDiceValue / 2) + 1) * _diceAmount) + _diceAmount * CalculateModifier(_flatConstitution));
        }

        public virtual void ChangeMainStat(float _mainUsedStatChange)
        {
            mainUsedStatValue -= _mainUsedStatChange;
        }

        public void GetStunned()
        {
            isStunned = true;
        }

        public void RemoveConditions()
        {
            isStunned = false;
            isFeared = false;
        }

        public void HealToFull()
        {
            hp = maxHP;
        }
        public void StartPetrify()
        {
            startPetrified = true;
        }

        public void SetDisadvantage()
        {
            hasDisadvantage = true;
        }
        public void SetFear()
        {
            isFeared = true;
        }
        public void SetCharm()
        {
            isCharmed = true;
        }
    }
}
