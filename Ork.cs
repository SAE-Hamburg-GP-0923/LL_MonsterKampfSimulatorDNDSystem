using System;

namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Ork : Monster
    {
        Random random = new Random();
        private float baseArmor = 0;
        public Action<Monster> ActivateCriticalSkill;
        public Ork(float _strength, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue) : base(_strength, _dexterity, _constitution, _intelligence, _wisdom, _charisma, _maxDiceValue)
        {
            monsterName = "Der Ork";
            mainUsedStatValue = _strength;
            MonsterRace = Game.EMonsterRace.Ork;
            hp = base.RollMonsterHP(4, 10, _constitution);
            armor = baseArmor;
            monsterColor = ConsoleColor.Green;
            maxHP = hp;
        }

        public override void Attack(Monster _creatureToHit)
        {
            int triggerChance = random.Next(1, 21);
            if (triggerChance == 1 && !isStunned && !isFeared)
            {
                if (!hasAttacked) hasAttacked = true;
                ActivateCriticalSkill.Invoke(this);
                var damage = MathF.Max(0, (RollMonsterDice(1, maxDiceValue) + CalculateModifier(mainUsedStatValue)) * 2);
                DamageCalculationPrint.Invoke(damage, this);
                _creatureToHit.TakeDamage(damage, this);
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
