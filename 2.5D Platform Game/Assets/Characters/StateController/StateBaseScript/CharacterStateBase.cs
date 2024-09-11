using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class CharacterStateBase : StateMachineBehaviour
    {
        private CharacterControl characterControl;
        public List<StateData> abilityDatas = new List<StateData>();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach(StateData d in abilityDatas)
            {
                d.OnEnterAbility(Control(animator), animator, stateInfo);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in abilityDatas)
            {
                d.OnUpdateAbility(Control(animator), animator, stateInfo);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in abilityDatas)
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


