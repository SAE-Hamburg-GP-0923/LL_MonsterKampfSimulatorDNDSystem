﻿namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Hag : Monster
    {
        Random random = new Random();
        private float baseArmor = 0;
        private int currentMirrorImages;
        private int maxMirrorImages = 4;
        public Action ActivateMirrorImageSkill;
        public Hag(float _strenght, float _dexterity, float _constitution, float _intelligence, float _wisdom, float _charisma, int _maxDiceValue) : base(_strenght, _dexterity, _constitution, _intelligence, _wisdom, _charisma, _maxDiceValue)
        {
            monsterName = "Hag";
            hp = base.RollMonsterHP(4, 8, _constitution);
            MonsterRace = Game.EMonsterRace.Hag;
            mainUsedStatValue = _wisdom;
            armor = baseArmor;
        }

        public override void Attack(Monster _creatureToHit)
        {
            var triggerChance = random.Next(1, 21);
            if (triggerChance <= 5 && currentMirrorImages == 0)
            {
                CastMirrorImage();
            }
            else
            {
                base.Attack(_creatureToHit);
            }
        }
        public override void TakeDamage(float _damageTaken, Monster _attackingMonster, bool _isCritical = false)
        {
            if (currentMirrorImages > 0 && random.Next(1, currentMirrorImages + 2) != currentMirrorImages + 1)
            {
                currentMirrorImages--;
            }
            else
            {
                base.TakeDamage(_damageTaken, this);
            }
        }

        public void CastMirrorImage()
        {
            ActivateMirrorImageSkill.Invoke();
            currentMirrorImages = maxMirrorImages;
        }
    }
}