using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Beholder : Monster
    {
        Random random = new Random();
        private float baseArmor = 0;
        //This determines after how many rounds the beholder does full damage
        private float phase2Threshhold = 3;

        #region Actions
        public Action<Monster> ActivateParalysingRay;
        public Action<Monster> ActivateEnervationRay;
        public Action<Monster> ActivateDisintegrationRay;
        public Action<Monster> ActivateDeathRay;
        public Action<Monster> ActivatePetrificationRay;
        public Action<Monster> ActivateSlowRay;
        public Action<Monster> ActivateFearRay;
        public Action<Monster> ActivateCharmRay;
        private List<Action<Monster>> possibleAttacks = new List<Action<Monster>>();
        #endregion


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

        /// <summary>
        /// This ray stuns for one round
        /// </summary>
        /// <param name="_creatureToHit"></param>
        private void ParalysingRay(Monster _creatureToHit)
        {
            ActivateParalysingRay.Invoke(this);
            _creatureToHit.GetStunned();
        }
        /// <summary>
        /// This ray does a small amount of damage
        /// </summary>
        /// <param name="_creatureToHit"></param>
        private void EnervationRay(Monster _creatureToHit)
        {
            ActivateEnervationRay.Invoke(this);
            DamageRay(_creatureToHit, 2);
        }
        /// <summary>
        /// This ray does a huge amount of damage
        /// </summary>
        /// <param name="_creatureToHit"></param>
        private void DisintegrationRay(Monster _creatureToHit)
        {
            ActivateDisintegrationRay.Invoke(this);
            DamageRay(_creatureToHit, 6);
        }
        /// <summary>
        /// This ray does a medium amount of damage
        /// </summary>
        /// <param name="_creatureToHit"></param>
        private void DeathRay(Monster _creatureToHit)
        {
            ActivateDeathRay.Invoke(this);
            DamageRay(_creatureToHit, 4);
        }
        /// <summary>
        /// This ray starts the petrification of the enemy, resulting in certain death after a fixed number of rounds
        /// </summary>
        /// <param name="_creatureToHit"></param>
        private void PetrificationRay(Monster _creatureToHit)
        {
            ActivatePetrificationRay.Invoke(this);
            _creatureToHit.StartPetrify();
            possibleAttacks.Remove(PetrificationRay);
        }
        /// <summary>
        /// This ray gives the other creature disadvantage on its next attack roll
        /// </summary>
        /// <param name="_creatureToHit"></param>
        private void SlowingRay(Monster _creatureToHit)
        {
            ActivateSlowRay.Invoke(this);
            _creatureToHit.SetDisadvantage();
        }
        /// <summary>
        /// This ray prevents the other creature to use any attack other than default attack
        /// </summary>
        /// <param name="_creatureToHit"></param>
        private void FearRay(Monster _creatureToHit)
        {
            ActivateFearRay.Invoke(this);
            _creatureToHit.SetFear();
        }
        /// <summary>
        /// This ray reduces the damage the other creature deals on its next attack
        /// </summary>
        /// <param name="_creatureToHit"></param>
        private void CharmRay(Monster _creatureToHit)
        {
            ActivateCharmRay.Invoke(this);
            _creatureToHit.SetCharm();
        }

        /// <summary>
        /// Function to streamline all 3 damage rays
        /// </summary>
        /// <param name="_creatureToHit"></param>
        /// <param name="_diceAmount"></param>
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
