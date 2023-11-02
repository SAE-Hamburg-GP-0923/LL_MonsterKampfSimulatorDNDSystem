namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Beholder : Monster
    {
        Random random = new Random();
        private float maxHP;
        private float baseArmor = 0;

        /* NOTES for skills:
            Charm Ray = ??
            Paralyzing = Stun for one round
            Fear Ray = ?? disadvantage on attack rolls?
            Slowing Ray = can only basic attack?
            enervation Ray = Big boom 
            telekinetic ray = ?? 
            sleep ray = stunned till hit, hit does double damage?
            petrification ray = dies in 3 rounds?
            disintegration ray = really big boom
            death ray = medium big boom
          */
        public Beholder(float _strenght, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue) : base(_strenght, _dexterity, _constitution, _intelligence, _wisdom, _charisma, _maxDiceValue)
        {
            monsterName = "Der Beholder";
            hp = base.RollMonsterHP(4, 20, _constitution);
            maxHP = hp;
            BossRace = Game.EBossRace.Beholder;
            mainUsedStatValue = _intelligence;
            armor = baseArmor;
            monsterColor = ConsoleColor.DarkRed;
        }

        public override void Attack(Monster _creatureToHit)
        {
            base.Attack(_creatureToHit);
        }

        public override void TakeDamage(float _damageTaken, Monster _attackingMonster, bool _isCritical = false)
        {
            base.TakeDamage(_damageTaken, _attackingMonster);
        }

        private void PlaceHolderRay1()
        {

        }
        private void PlaceHolderRay2()
        {

        }
        private void PlaceHolderRay3()
        {

        }
        private void PlaceHolderRay4()
        {

        }
        private void PlaceHolderRay5()
        {

        }
        private void PlaceHolderRay6()
        {

        }
        private void PlaceHolderRay7()
        {

        }
        private void PlaceHolderRay8()
        {

        }
        private void PlaceHolderRay9()
        {

        }
        private void PlaceHolderRay10()
        {

        }
    }
}
