using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class ManualInput : MonoBehaviour
    {
        private CharacterControl control;

        private void Awake()
        {
            control = GetComponent<CharacterControl>();
        }

        void Update()
        {
            control.MoveRight = VirtualInputManager.Instance.MoveRight ? true : false;
            control.MoveLeft = VirtualInputManager.Instance.MoveLeft ? true : false;
            control.Jump = VirtualInputManager.Instance.Jump ? true : false;
            control.Attack = VirtualInputManager.Instance.Attack ? true : false;
        }
    }
}

