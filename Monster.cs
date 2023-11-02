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

        protected int rolledValue;
        protected int maxDiceValue;
        protected float mainUsedStatValue;
        public float MainUsedStatValue => mainUsedStatValue;
        protected Random monsterDice = new Random();

        public Game.EMonsterRace MonsterRace;
        protected string monsterName;
        public Game.EBossRace BossRace;
        public string MonsterName => monsterName;
        public Action<float, Monster> DamagePrint;
        public Action<Monster> HPPrint;
        public Action<float, Monster> PrintDiceRollingAnim;
        public Action<float, Monster> DamageCalculationPrint;
        public Action<float, Monster> DamageReducedPrint;

        private bool isCreated;
        protected bool hasAttacked;
        public bool HasAttacked => hasAttacked;

        public Monster(float _strenght, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue)
        {
            strength = _strenght;
            dexterity = _dexterity;
            constitution = _constitution;
            intelligence = _intelligence;
            wisdom = _wisdom;
            charisma = _charisma;
            maxDiceValue = _maxDiceValue;
            initiative = _dexterity;
        }
        public virtual void Attack(Monster _creatureToHit)
        {
            if (!hasAttacked) hasAttacked = true;
            var damage = MathF.Max(0,RollMonsterDice(1, maxDiceValue) + CalculateModifier(mainUsedStatValue));
            DamageCalculationPrint.Invoke(damage, this);
            _creatureToHit.TakeDamage(damage, this);
        }

        public virtual void TakeDamage(float _damageTaken, Monster _attackingMonster, bool _isCritical = false)
        {
            float actualDamage;
            actualDamage = MathF.Max(_damageTaken - armor, 0);
            if (_damageTaken > 0) DamageReducedPrint.Invoke(armor, this);
            DamagePrint.Invoke(actualDamage, this);
            HP = MathF.Max(0, HP - actualDamage);
        }

        public int RollMonsterDice(int _diceAmount, int _maxDiceValue)
        {
            rolledValue = 0;
            for (int i = 0; i < _diceAmount; i++)
            {
                rolledValue += monsterDice.Next(1, _maxDiceValue + 1);
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
    }
}
