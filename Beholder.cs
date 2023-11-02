namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Beholder : Monster
    {
        Random random = new Random();
        private float maxHP;
        private float baseArmor = 0;

        /* NOTES for  boss skills:
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

        public Beholder(float _strength, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue) : base(_strength, _dexterity, _constitution, _intelligence, _wisdom, _charisma, _maxDiceValue)
        {
            monsterName = "Der Beholder";
            hp = base.RollMonsterHP(19, 10, _constitution);
            maxHP = hp;
            BossRace = Game.EBossRace.Beholder;
            mainUsedStatValue = _intelligence;
            armor = baseArmor;
            monsterColor = ConsoleColor.DarkRed;
        }

        //TODO: Implement all rays
        public override void Attack(Monster _creatureToHit)
        {
            var chooseRay = random.Next(1, 5);
            switch (chooseRay)
            {
                case 1:
                    ParalysingRay(_creatureToHit);
                    break;
                case 2:
                    EnervationRay(_creatureToHit);
                    break;
                case 3:
                    DisintegrationRay(_creatureToHit);
                    break;
                case 4:
                    DeathRay(_creatureToHit);
                    break;
            }
        }

        //TODO: DamageRays => Method with dice amount parameter
        public override void TakeDamage(float _damageTaken, Monster _attackingMonster, bool _isCritical = false)
        {
            base.TakeDamage(_damageTaken, _attackingMonster);
        }

        private void ParalysingRay(Monster _creatureToHit)
        {
            _creatureToHit.GetStunned();
        }
        private void EnervationRay(Monster _creatureToHit)
        {
            var damage = MathF.Max(0, RollMonsterDice(2, maxDiceValue) + CalculateModifier(mainUsedStatValue));
            DamageCalculationPrint.Invoke(damage, this);
            _creatureToHit.TakeDamage(damage, this);
        }
        private void DisintegrationRay(Monster _creatureToHit)
        {
            var damage = MathF.Max(0, RollMonsterDice(8, maxDiceValue) + CalculateModifier(mainUsedStatValue));
            DamageCalculationPrint.Invoke(damage, this);
            _creatureToHit.TakeDamage(damage, this);
        }
        private void DeathRay(Monster _creatureToHit)
        {
            var damage = MathF.Max(0, RollMonsterDice(4, maxDiceValue) + CalculateModifier(mainUsedStatValue));
            DamageCalculationPrint.Invoke(damage, this);
            _creatureToHit.TakeDamage(damage, this);
        }
        private void PlaceHolderRay5(Monster _creatureToHit)
        {

        }
        private void PlaceHolderRay6(Monster _creatureToHit)
        {

        }
        private void PlaceHolderRay7(Monster _creatureToHit)
        {

        }
        private void PlaceHolderRay8(Monster _creatureToHit)
        {

        }
        private void PlaceHolderRay9(Monster _creatureToHit)
        {

        }
        private void PlaceHolderRay10(Monster _creatureToHit)
        {

        }
    }
}
