
using System;

namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Goblin : Monster
    {
        Random random = new Random();
        public Action<Monster> ActivateDodgeSkill;
        private float baseArmor = 2;
        public Goblin(float _strenght, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue) : base(_strenght, _dexterity, _constitution, _intelligence, _wisdom, _charisma, _maxDiceValue)
        {
            monsterName = "Der Goblin";
            hp = base.RollMonsterHP(4,6,_constitution);
            MonsterRace = Game.EMonsterRace.Goblin;
            mainUsedStatValue = _dexterity;
            armor = baseArmor;
            monsterColor = ConsoleColor.DarkYellow;
        }
        public override void Attack(Monster _creatureToHit)
        {
            base.Attack(_creatureToHit);
        }
        public override void TakeDamage(float _damageTaken, Monster _attackingMonster, bool _isCritical = false)
        {
            int triggerChance = random.Next(1, 11);
            if (triggerChance <= 2 && !_isCritical)
            {
                ActivateDodgeSkill.Invoke(this);
            }
            else
            {
                base.TakeDamage(_damageTaken, this);
            }
        }
    }
}
