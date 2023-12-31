﻿
using System;

namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Goblin : Monster
    {
        Random random = new Random();
        public Action activateDodgeSkill;
        private float baseHP;
        private float baseArmor = 2;
        public Goblin(float _strenght, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue) : base(_strenght, _dexterity, _constitution, _intelligence, _wisdom, _charisma, _maxDiceValue)
        {
            hp = baseHP * _constitution;
            mainUsedStatValue = _dexterity;
            armor = baseArmor;
        }
        public override void Attack(Monster _creatureToHit)
        {
            base.Attack(_creatureToHit);
        }
        public override void TakeDamage(float _damageTaken, bool _isCritical = false)
        {
            int triggerChance = random.Next(1, 11);
            if (triggerChance == 1 && !_isCritical)
            {
                activateDodgeSkill.Invoke();
            }
            else
            {
                base.TakeDamage(_damageTaken);
            }
        }
    }
}
