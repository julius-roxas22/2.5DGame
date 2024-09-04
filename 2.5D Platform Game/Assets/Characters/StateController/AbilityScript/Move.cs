using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/Move")]
    public class Move : StateData
    {
        [SerializeField] private float BlockDistance;
        [SerializeField] private AnimationCurve SpeedGraph;
        [SerializeField] private float Speed;

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterControl.Jump)
            {
                animator.SetBool(TransitionParameters.Jump.ToString(), true);
            }

            if (characterControl.MoveRight && characterControl.MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (!characterControl.MoveRight && !characterControl.MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (characterControl.MoveRight)
            {
                if (!CheckFront(characterControl))
                {
                    characterControl.transform.Translate(Vector3.forward * Speed * SpeedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime);
                }
                characterControl.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            if (characterControl.MoveLeft)
            {
                if (!CheckFront(characterControl))
                {
                    characterControl.transform.Translate(Vector3.forward * Speed * SpeedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime);
                }
                characterControl.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        private bool CheckFront(CharacterControl control)
        {
            foreach (GameObject o in control.FrontSpheres)
            {
                RaycastHit hit;
                Debug.DrawRay(o.transform.position, o.transform.forward * BlockDistance, Color.red);
                if (Physics.Raycast(o.transform.position, o.transform.forward, out hit, BlockDistance))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

