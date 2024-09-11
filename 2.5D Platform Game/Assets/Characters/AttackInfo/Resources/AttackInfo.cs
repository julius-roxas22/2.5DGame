using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class AttackInfo : MonoBehaviour
    {
        public CharacterControl Attacker = null;
        public Attack AttackAbility;
        public List<string> CollidingNames = new List<string>();
        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;
        public int CurrentHits;
        public bool isRegistered;
        public bool isFinished;

        public void ResetInfo(Attack attack)
        {
            isRegistered = false;
            isFinished = false;
            AttackAbility = attack;
        }

        public void RegisterAttack(Attack attack, CharacterControl attacker)
        {
            isRegistered = true;
            Attacker = attacker;
            AttackAbility = attack;
            CollidingNames = attack.ColliderNames;
            MustCollide = attack.MustCollide;
            MustFaceAttacker = attack.MustFaceAttacker;
            LethalRange = attack.LethalRange;
            MaxHits = attack.Maxhits;
            CurrentHits = 0;
        }

    }
}
