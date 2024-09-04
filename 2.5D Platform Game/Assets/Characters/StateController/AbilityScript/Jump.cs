using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGameDev/Ability/Jump")]
    public class Jump : StateData
    {
        [SerializeField] private AnimationCurve GravityMultiplierGraph;
        [SerializeField] private AnimationCurve PullMultiplierGraph;
        [SerializeField] private float jumpForce;

        public override void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterControl.RIGID_BODY.AddForce(Vector3.up * jumpForce);
            animator.SetBool(TransitionParameters.Grounded.ToString(), false);
        }

        public override void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterControl.GravityMultiplier = GravityMultiplierGraph.Evaluate(stateInfo.normalizedTime);
            characterControl.PullMultiplier = PullMultiplierGraph.Evaluate(stateInfo.normalizedTime);
        }

        public override void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}

