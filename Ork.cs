using System;

namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Ork : Monster
    {
        Random random = new Random();
        private float baseArmor = 0;
        public Action ActivateCriticalSkill;
        public Ork(float _strenght, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue) : base(_strenght, _dexterity, _constitution, _intelligence, _wisdom, _charisma, _maxDiceValue)
        {
            monsterName = "Ork";
            mainUsedStatValue = _strenght;
            MonsterRace = Game.EMonsterRace.Ork;
            hp = base.RollMonsterHP(4, 10, _constitution);
            armor = baseArmor;
        }

        public override void Attack(Monster _creatureToHit)
        {
            int triggerChance = random.Next(1, 21);
            if (triggerChance == 1)
            {
                if (!hasAttacked) hasAttacked = true;
                ActivateCriticalSkill.Invoke();
                var damage = (RollMonsterDice(1, maxDiceValue) + CalculateModifier(mainUsedStatValue));
                DamageCalculationPrint.Invoke(damage, this);
                _creatureToHit.TakeDamage(damage * 2, this);
            }
            else
            {
                base.Attack(_creatureToHit);
            }
        }
        public override void TakeDamage(float _damageTaken, Monster _attackingMonster, bool _isCritical = false)
        {
            base.TakeDamage(_damageTaken, this);
        }
    }
}
