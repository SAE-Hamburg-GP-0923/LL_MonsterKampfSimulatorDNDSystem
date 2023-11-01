
namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Lich : Monster
    {
        Random random = new Random();
        private float baseArmor = 0;
        private float maxHP;
        public Action ActivateReviveSkill;
        private bool cursedEnemy;
        public Lich(float _strenght, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue) : base(_strenght, _dexterity, _constitution, _intelligence, _wisdom, _charisma, _maxDiceValue)
        {
            monsterName = "Lich";
            hp = base.RollMonsterHP(4, 6, _constitution);
            maxHP = hp;
            MonsterRace = Game.EMonsterRace.Lich;
            mainUsedStatValue = _wisdom;
            armor = baseArmor;
        }

        public override void Attack(Monster _creatureToHit)
        {
            var triggerChance = random.Next(1, 21);
            if (!cursedEnemy && triggerChance <= 11)
            {
                cursedEnemy = true;
            }
            else
            {
                base.Attack(_creatureToHit);
            }
        }

        public override void TakeDamage(float _damageTaken, Monster _attackingMonster, bool _isCritical = false)
        {
            if (cursedEnemy)
            {
                base.TakeDamage(_damageTaken / 2, _attackingMonster);
                cursedEnemy = false;
            }
            else
            {
                base.TakeDamage(_damageTaken, _attackingMonster);
                var triggerChance = random.Next(1, 21);
                if (HP == 0 && triggerChance <= 5)
                {
                    Revive();
                }
            }
        }
        public void Revive()
        {
            HP = MathF.Floor(maxHP / 4);
        }
    }
}
