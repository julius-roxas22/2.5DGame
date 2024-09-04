using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/GrounDetector")]
    public class GroundDEtector : StateData
    {
        [Range(0.01f, 1)]
        public float CheckTime;
        public float Distance;

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= CheckTime)
            {
                if (IsGrounded(characterControl))
                {
                    animator.SetBool(TransitionParameters.Grounded.ToString(), true);
                }
                else
                {
                    animator.SetBool(TransitionParameters.Grounded.ToString(), false);
                }
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        bool IsGrounded(CharacterControl control)
        {
            if (control.RIGID_BODY.velocity.y <= 0f && control.RIGID_BODY.velocity.y > -0.001f)
            {
                return true;
            }

            if (control.RIGID_BODY.velocity.y < 0f)
            {
                foreach (GameObject o in control.BottomSpheres)
                {
                    RaycastHit hit;
                    Debug.DrawRay(o.transform.position, Vector3.down * Distance, Color.red);
                    if (Physics.Raycast(o.transform.position, Vector3.down, out hit, Distance))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

