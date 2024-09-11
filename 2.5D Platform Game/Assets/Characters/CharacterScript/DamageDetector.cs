using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{

    public class DamageDetector : MonoBehaviour
    {
        private CharacterControl control;

        private void Awake()
        {
            control = GetComponent<CharacterControl>();
        }

        void Update()
        {
            if (AttackManager.Instance.CurrentAttacks.Count > 0)
            {
                CheckAttack();
            }
        }

        private void CheckAttack()
        {
            foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
            {
                if (null == info)
                {
                    continue;
                }

                if (!info.isRegistered)
                {
                    continue;
                }

                if (info.isFinished)
                {
                    continue;
                }

                if (info.CurrentHits >= info.MaxHits)
                {
                    continue;
                }

                if (info.Attacker == control)
                {
                    continue;
                }

                if (info.MustCollide)
                {
                    if (IsCollided(info))
                    {
                        TakeDamage(info);
                    }
                }
            }
        }

        private bool IsCollided(AttackInfo info)
        {
            foreach (Collider col in control.CollidingParts)
            {
                foreach (string colName in info.CollidingNames)
                {
                    if (colName.Equals(col.gameObject.name))
                    {
                        //if (info.Attacker != control)
                        //{
                        //}
                        return true;
                    }
                }
            }
            return false;
        }

        private void TakeDamage(AttackInfo info)
        {
            //Debug.Log(info.Attacker.name + " hits " + control.name);
            control.skinnedMeshAnimator.runtimeAnimatorController = info.AttackAbility.GetDeathAnimatorController();
            info.CurrentHits++;
        }
    }

}
