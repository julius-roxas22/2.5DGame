using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class DeathAnimationManager : Singleton<DeathAnimationManager>
    {
        private DeathAnimationLoader loader;
        private List<RuntimeAnimatorController> Candidates = new List<RuntimeAnimatorController>();

        private void SetUpDeathAnimationLoader()
        {
            if (null == loader)
            {
                GameObject obj = Instantiate(Resources.Load("DeathAnimationLoader", typeof(GameObject))) as GameObject;
                loader = obj.GetComponent<DeathAnimationLoader>();
            }
        }

        public RuntimeAnimatorController GetDeathAnimation(GeneralBodyParts generalBodyParts)
        {
            Candidates.Clear();
            SetUpDeathAnimationLoader();

            foreach (DeathAnimationData data in loader.DeathData)
            {
                foreach (GeneralBodyParts body in data.DamageBodyParts)
                {
                    if (body == generalBodyParts)
                    {
                        Candidates.Add(data.AnimatorController);
                        break;
                    }
                }
            }
            return Candidates[Random.Range(0, Candidates.Count)];
        }
    }
}

