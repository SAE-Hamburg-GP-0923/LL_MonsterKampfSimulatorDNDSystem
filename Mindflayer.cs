﻿namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Mindflayer : Monster
    {

        Random random = new Random();
        private float baseArmor = 3;

        #region States
        private bool hasGrappledEnemy;
        #endregion

        #region Actions
        public Action<Monster> ActivateDrainStatSkill;
        public Action<Monster> ActivateGrappleSkill;
        #endregion

        public Mindflayer(float _strength, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue) : base(_strength, _dexterity, _constitution, _intelligence, _wisdom, _charisma, _maxDiceValue)
        {
            monsterName = "Der Mindflayer";
            hp = base.RollMonsterHP(12, 6, _constitution);
            MonsterRace = Game.EMonsterRace.Mindflayer;
            mainUsedStatValue = _intelligence;
            armor = baseArmor;
            monsterColor = ConsoleColor.Magenta;
            maxHP = hp;
        }

        public override void Attack(Monster _creatureToHit)
        {
            if (hasGrappledEnemy && !isStunned && !isFeared)
            {
                DrainStat(_creatureToHit);
                hasGrappledEnemy = false;
            }
            else
            {
                base.Attack(_creatureToHit);
                var triggerChance = random.Next(1, 21);
                if (triggerChance <= 6 && !isFeared)
                {
                    ActivateGrappleSkill.Invoke(this);
                    hasGrappledEnemy = true;
                }
            }
        }

        public override void TakeDamage(float _damageTaken, Monster _attackingMonster, bool _isCritical = false)
        {
            base.TakeDamage(_damageTaken, this);
        }

        public void DrainStat(Monster _creatureToHit)
        {
            ActivateDrainStatSkill.Invoke(this);
            _creatureToHit.ChangeMainStat(RollMonsterDice(MathF.Max(usedAttackDiceAmount - Game.RoundCount, 1), maxDiceValue));
        }
    }
}
