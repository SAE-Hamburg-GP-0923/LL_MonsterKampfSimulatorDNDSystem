
namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Lich : Monster
    {
        Random random = new Random();
        private float baseArmor = 0;
        private float maxHP;
        public Action<Monster> ActivateReviveSkill;
        public Action<Monster> ActivateCurseSkill;
        public Action<Monster> CurseEffectPrint;
        private bool cursedEnemy;
        public Lich(float _strength, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue) : base(_strength, _dexterity, _constitution, _intelligence, _wisdom, _charisma, _maxDiceValue)
        {
            monsterName = "Der Lich";
            hp = base.RollMonsterHP(4, 6, _constitution);
            maxHP = hp;
            MonsterRace = Game.EMonsterRace.Lich;
            mainUsedStatValue = _wisdom;
            armor = baseArmor;
            monsterColor = ConsoleColor.DarkGray;
        }

        public override void Attack(Monster _creatureToHit)
        {
            var triggerChance = random.Next(1, 21);
            if (!cursedEnemy && triggerChance <= 5 && !isStunned)
            {
                ActivateCurseSkill.Invoke(this);
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
                float actualDamage;
                actualDamage = MathF.Max(MathF.Ceiling(_damageTaken / 2 - armor), 0);
                if (_damageTaken > 0) CurseEffectPrint.Invoke(this);
                DamagePrint.Invoke(actualDamage, this);
                HP = MathF.Max(0, HP - actualDamage);
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
            ActivateReviveSkill.Invoke(this);
            HP = MathF.Floor(maxHP / 4);
        }
    }
}
