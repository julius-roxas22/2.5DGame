using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Death Data" , menuName = "IndieGameDev/DeathAnimationData/DefaultDeathData")]
    public class DeathAnimationData : ScriptableObject
    {
        public List<GeneralBodyParts> DamageBodyParts = new List<GeneralBodyParts>();
        public RuntimeAnimatorController AnimatorController;
        public bool IsFacing;
    }
}


