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
                    hp = value;
                    HPPrint.Invoke(this);
                }

            }
        }
        protected float strength;
        public float Strength => strength;
        protected float dexterity;
        public float Dexterity => dexterity;
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


        
        protected int rolledValue;
        protected int maxDiceValue;
        protected float mainUsedStatValue;
        protected Random attackDice = new Random();

        public Game.EMonsterRace MonsterRace;
        protected string monsterName;
        public string MonsterName => monsterName;
        public delegate void DamagePrintHandler(Monster _monster, float _actualDamage);
        public event DamagePrintHandler DamagePrint;
        public delegate void HPPrintHandler(Monster _monster);
        public event HPPrintHandler HPPrint;

        public Monster(float _strenght, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue)
        {
            strength = _strenght;
            dexterity = _dexterity;
            constitution = _constitution;
            intelligence = _intelligence;
            wisdom = _wisdom;
            charisma = _charisma;
            maxDiceValue = _maxDiceValue;
        }

        public Monster()
        {
        }

        public virtual void Attack(Monster _creatureToHit)
        {
            _creatureToHit.TakeDamage(RollAttackDice(1, maxDiceValue) + mainUsedStatValue);
        }

        public virtual void TakeDamage(float _damageTaken, bool _isCritical = false)
        {
            float actualDamage;
            actualDamage = MathF.Max(_damageTaken - armor, 0);
            DamagePrint.Invoke(this, actualDamage);
            HP = MathF.Max(0, HP - actualDamage);
        }

        public int RollAttackDice(int _diceAmount, int _maxDiceValue)
        {
            for (int i = 0; i < _diceAmount; i++)
            {
                rolledValue += attackDice.Next(1, _maxDiceValue + 1);
            }
            return rolledValue;
        }
    }
}
