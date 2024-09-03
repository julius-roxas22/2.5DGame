using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public abstract class StateData : ScriptableObject
    {
       public abstract void OnEnterAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo);
       public abstract void OnUpdateAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo);
       public abstract void OnExitAbility(CharacterControl characterControl, Animator animator, AnimatorStateInfo stateInfo);
    }
}


