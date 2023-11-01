﻿namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Centaur : Monster
    {
        Random random = new Random();
        private float baseArmor = 1;
        public Action ActivateKickSkill;
        public Centaur(float _strenght, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue) : base(_strenght, _dexterity, _constitution, _intelligence, _wisdom, _charisma, _maxDiceValue)
        {
            initiative = _dexterity * 2;
            monsterName = "Centaur";
            hp = base.RollMonsterHP(4, 8, _constitution);
            MonsterRace = Game.EMonsterRace.Centaur;
            mainUsedStatValue = _dexterity;
            armor = baseArmor;
        }
        public override void Attack(Monster _creatureToHit)
        {
            if (!_creatureToHit.HasAttacked)
            {
                base.Attack(_creatureToHit);
                base.Attack(_creatureToHit);

            }
            else
            {
                base.Attack(_creatureToHit);
            }
        }
        public override void TakeDamage(float _damageTaken, Monster _attackingMonster, bool _isCritical = false)
        {
            var triggerChance = random.Next(1, 21);
            if (triggerChance <= 3)
            {
                base.TakeDamage(_damageTaken, this);
                Kick(_attackingMonster);
            }
            else
            {
                base.TakeDamage(_damageTaken, this);

            }

        }

        public void Kick(Monster _creatureToHit)
        {
            var damage = (RollMonsterDice(1, 4) + CalculateModifier(mainUsedStatValue));
            DamageCalculationPrint.Invoke(damage, this);
            _creatureToHit.TakeDamage(damage, this);
        }
    }
}