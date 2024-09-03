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
        }
    }
}


