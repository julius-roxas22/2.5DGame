using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/Move")]
    public class Move : StateData
    {
        [SerializeField] private float Speed;

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
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
                characterControl.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                characterControl.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            if (characterControl.MoveLeft)
            {
                characterControl.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                characterControl.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}

