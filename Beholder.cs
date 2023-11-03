using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Beholder : Monster
    {
        Random random = new Random();
        private float baseArmor = 0;
        public Action<Monster> ActivateParalysingRay;
        public Action<Monster> ActivateEnervationRay;
        public Action<Monster> ActivateDisintegrationRay;
        public Action<Monster> ActivateDeathRay;
        public Action<Monster> ActivatePetrificationRay;
        public Action<Monster> ActivateSlowRay;
        public Action<Monster> ActivateFearRay;
        public Action<Monster> ActivateCharmRay;

        private List<Action<Monster>> possibleAttacks = new List<Action<Monster>>();
        private float phase2Threshhold = 3;

        /* NOTES for  boss skills:
            [DONE]Charm Ray = reduce damage value
            [DONE]Paralyzing = Stun for one round
            [DONE]Fear Ray =  can only basic attack
            [DONE]Slowing Ray = Disadvatnage on attack rolls
            [DONE]enervation Ray = Big boom 
            [DONE]petrification ray = dies in 3 rounds?
            [DONE]disintegration ray = really big boom
            [DONE]death ray = medium big boom

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
            possibleAttacks.Add(ParalysingRay);
            possibleAttacks.Add(EnervationRay);
            possibleAttacks.Add(DisintegrationRay);
            possibleAttacks.Add(DeathRay);
            possibleAttacks.Add(PetrificationRay);
            possibleAttacks.Add(SlowingRay);
            possibleAttacks.Add(FearRay);
            possibleAttacks.Add(CharmRay);

        }
        public override void Attack(Monster _creatureToHit)
        {
            this.hasAttacked = true;
            var attackChoice = random.Next(0, possibleAttacks.Count);
            possibleAttacks[attackChoice].Invoke(_creatureToHit);
        }
        public override void TakeDamage(float _damageTaken, Monster _attackingMonster, bool _isCritical = false)
        {
            base.TakeDamage(_damageTaken, _attackingMonster);
        }

        private void ParalysingRay(Monster _creatureToHit)
        {
            ActivateParalysingRay.Invoke(this);
            _creatureToHit.GetStunned();
        }
        private void EnervationRay(Monster _creatureToHit)
        {
            ActivateEnervationRay.Invoke(this);
            DamageRay(_creatureToHit, 2);
        }
        private void DisintegrationRay(Monster _creatureToHit)
        {
            ActivateDisintegrationRay.Invoke(this);
            DamageRay(_creatureToHit, 6);
        }
        private void DeathRay(Monster _creatureToHit)
        {
            ActivateDeathRay.Invoke(this);
            DamageRay(_creatureToHit, 4);
        }
        private void PetrificationRay(Monster _creatureToHit)
        {
            ActivatePetrificationRay.Invoke(this);
            _creatureToHit.StartPetrify();
            possibleAttacks.Remove(PetrificationRay);
        }
        private void SlowingRay(Monster _creatureToHit)
        {
            ActivateSlowRay.Invoke(this);
            _creatureToHit.SetDisadvantage();
        }
        private void FearRay(Monster _creatureToHit)
        {
            ActivateFearRay.Invoke(this);
            _creatureToHit.SetFear();
        }
        private void CharmRay(Monster _creatureToHit)
        {
            ActivateCharmRay.Invoke(this);
            _creatureToHit.SetCharm();
        }

        private void DamageRay(Monster _creatureToHit, int _diceAmount)
        {
            float damage;
            if (Game.RoundCount <= phase2Threshhold)
            {
                damage = MathF.Max(0, RollMonsterDice(_diceAmount / 2, maxDiceValue) + CalculateModifier(mainUsedStatValue));
            }
            else
            {
                damage = MathF.Max(0, RollMonsterDice(_diceAmount, maxDiceValue) + CalculateModifier(mainUsedStatValue));
                
            }
            DamageCalculationPrint.Invoke(damage, this);
            _creatureToHit.TakeDamage(damage, this);
        }
    }
}
