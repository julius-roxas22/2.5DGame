using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class CharacterStateBase : StateMachineBehaviour
    {
        private CharacterControl characterControl;
        public List<StateData> AbilityDatas = new List<StateData>();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach(StateData d in AbilityDatas)
            {
                d.OnEnterAbility(Control(animator), animator, stateInfo);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in AbilityDatas)
            {
                d.OnUpdateAbility(Control(animator), animator, stateInfo);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in AbilityDatas)
            {
                d.OnExitAbility(Control(animator), animator, stateInfo);
            }
        }

        public CharacterControl Control(Animator animator)
        {
            if (null == characterControl)
            {
                characterControl = animator.GetComponentInParent<CharacterControl>();
            }
            return characterControl;
        }
    }
}


