using System.Buffers.Text;

namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Troll : Monster
    {
        private float maxHP;
        public Action<Monster> ActivateHealSkill;
        private float baseArmor = 5;
        public Troll(float _strenght, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue) : base(_strenght, _dexterity, _constitution, _intelligence, _wisdom, _charisma, _maxDiceValue)
        {
            monsterName = "Der Troll";
            MonsterRace = Game.EMonsterRace.Troll;
            hp = base.RollMonsterHP(4, 12, _constitution);
            maxHP = hp;
            mainUsedStatValue = _strenght;
            armor = baseArmor;
            monsterColor = ConsoleColor.DarkGreen;
        }
        public override void Attack(Monster _creatureToHit)
        {
            base.Attack(_creatureToHit);
        }
        public override void TakeDamage(float _damageTaken, Monster _attackingMonster, bool _isCritical = false)
        {
            base.TakeDamage(_damageTaken, this);
            if (HP > 0 && HP < maxHP)
            {
                ActivateHealSkill.Invoke(this);
                SelfHeal();
            }
        }

        private void SelfHeal()
        {
            float healValue = maxHP * 0.05f;
            HP = MathF.Min(maxHP, HP + healValue);
        }
    }
}
