using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/Attack")]
    public class Attack : StateData
    {
        public float StartAttackTime;
        public float EndTimeAttack;
        public List<string> ColliderNames = new List<string>();
        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int Maxhits;
        public List<RuntimeAnimatorController> DeathAnimators = new List<RuntimeAnimatorController>();

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameters.Attack.ToString(), false);

            GameObject obj = Instantiate(Resources.Load("AttackInfo", typeof(GameObject))) as GameObject;
            AttackInfo info = obj.GetComponent<AttackInfo>();

            info.ResetInfo(this, characterControl);

            if (!AttackManager.Instance.CurrentAttacks.Contains(info))
            {
                AttackManager.Instance.CurrentAttacks.Add(info);
            }
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            RegisteredAttack(characterControl, stateInfo);
            DeRegistered(characterControl, stateInfo);
        }

        public void RegisteredAttack(CharacterControl characterControl, AnimatorStateInfo stateInfo)
        {
            if (StartAttackTime <= stateInfo.normalizedTime && EndTimeAttack > stateInfo.normalizedTime)
            {
                foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
                {
                    if (null == info)
                    {
                        continue;
                    }

                    if (this == info.AttackAbility && !info.isRegistered)
                    {
                        info.RegisterAttack(this);
                    }
                }
            }
        }

        public void DeRegistered(CharacterControl characterControl, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= EndTimeAttack)
            {
                foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
                {
                    if (null == info)
                    {
                        continue;
                    }

                    if (this == info.AttackAbility && !info.isFinished)
                    {
                        info.isFinished = true;
                        Destroy(info.gameObject);
                    }
                }
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            ClearAttack();
        }

        public void ClearAttack()
        {
            for (int i = 0; i < AttackManager.Instance.CurrentAttacks.Count; i++)
            {
                if (null == AttackManager.Instance.CurrentAttacks[i] || AttackManager.Instance.CurrentAttacks[i].isFinished)
                {
                    AttackManager.Instance.CurrentAttacks.RemoveAt(i);
                }
            }
        }

        public RuntimeAnimatorController GetDeathAnimatorController()
        {
            int index = Random.Range(0, DeathAnimators.Count);
            return DeathAnimators[index];
        }
    }
}

