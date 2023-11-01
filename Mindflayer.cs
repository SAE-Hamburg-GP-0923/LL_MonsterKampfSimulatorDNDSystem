namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Mindflayer : Monster
    {

        Random random = new Random();
        private float baseArmor = 3;
        public Action ActivateDrainStatSkill;
        private bool hasGrappledEnemy;
        public Action ActivateGrappleSkill;
        public Mindflayer(float _strenght, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue) : base(_strenght, _dexterity, _constitution, _intelligence, _wisdom, _charisma, _maxDiceValue)
        {
            monsterName = "Mindflayer";
            hp = base.RollMonsterHP(4, 6, _constitution);
            MonsterRace = Game.EMonsterRace.Mindflayer;
            mainUsedStatValue = _intelligence;
            armor = baseArmor;
        }

        public override void Attack(Monster _creatureToHit)
        {
            if (hasGrappledEnemy)
            {
                DrainStat(_creatureToHit);
                hasGrappledEnemy = false;
            }
            else
            {
                base.Attack(_creatureToHit);
                var triggerChance = random.Next(1, 21);
                if (triggerChance <= 6)
                {
                    ActivateGrappleSkill.Invoke();
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
            ActivateDrainStatSkill.Invoke();
            _creatureToHit.ChangeMainStat(RollMonsterDice(1, maxDiceValue));
        }
    }
}
