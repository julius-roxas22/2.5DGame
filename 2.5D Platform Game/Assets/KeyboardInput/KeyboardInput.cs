using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class KeyboardInput : MonoBehaviour
    {
        void Update()
        {
            VirtualInputManager.Instance.MoveRight = Input.GetKey(KeyCode.D) ? true : false;
            VirtualInputManager.Instance.MoveLeft = Input.GetKey(KeyCode.A) ? true : false;
            VirtualInputManager.Instance.Jump = Input.GetKey(KeyCode.Space) ? true : false;
            VirtualInputManager.Instance.Attack = Input.GetKey(KeyCode.Return) ? true : false;
        }
    }
}


