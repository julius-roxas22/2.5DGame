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
        [SerializeField] private bool IsConstantMove;

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterControl.Jump)
            {
                animator.SetBool(TransitionParameters.Jump.ToString(), true);
            }

            if (IsConstantMove)
            {
                ConstantMove(characterControl, stateInfo);
            }
            else
            {
                ControlledMove(characterControl, animator, stateInfo);
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        private void ConstantMove(CharacterControl characterControl, AnimatorStateInfo stateInfo)
        {
            if (!CheckFront(characterControl))
            {
                characterControl.MoveAbleCharacter(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }

        private void ControlledMove(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
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
                    characterControl.MoveAbleCharacter(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }
                characterControl.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            if (characterControl.MoveLeft)
            {
                if (!CheckFront(characterControl))
                {
                    characterControl.MoveAbleCharacter(Speed, SpeedGraph.Evaluate(stateInfo.normalizedTime));
                }
                characterControl.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }


        private bool CheckFront(CharacterControl control)
        {
            foreach (GameObject o in control.FrontSpheres)
            {
                RaycastHit hit;
                Debug.DrawRay(o.transform.position, o.transform.forward * BlockDistance, Color.red);
                if (Physics.Raycast(o.transform.position, o.transform.forward, out hit, BlockDistance))
                {
                    if (!control.RagdollParts.Contains(hit.collider))
                    {
                        if (!IsBodyPart(hit.collider))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        bool IsBodyPart(Collider col)
        {
            CharacterControl control = col.transform.root.GetComponent<CharacterControl>();

            if (null == control)
            {
                return false;
            }

            if (control.gameObject == col.gameObject)
            {
                return false;
            }

            if (control.RagdollParts.Contains(col))
            {
                return true;
            }

            return false;
        }
    }
}

