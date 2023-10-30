using System;

namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Ork : Monster
    {
        Random random = new Random();
        private float baseHP;
        private float baseArmor = 0;
        public Ork(float _strenght, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue) : base(_strenght, _dexterity, _constitution, _intelligence, _wisdom, _charisma, _maxDiceValue)
        {
            mainUsedStatValue = _strenght;
            hp = baseHP * _constitution;
            armor = baseArmor;
        }

        public override void Attack(Monster _creatureToHit)
        {
            int triggerChance = random.Next(1, 21);
            if (triggerChance == 1)
            {
                _creatureToHit.TakeDamage(RollAttackDice(1, maxDiceValue) + mainUsedStatValue * 2);
            }
            else
            {
                base.Attack(_creatureToHit);
            }
        }
        public override void TakeDamage(float _damageTaken, bool _isCritical = false)
        {
            base.TakeDamage(_damageTaken);
        }
    }
}
