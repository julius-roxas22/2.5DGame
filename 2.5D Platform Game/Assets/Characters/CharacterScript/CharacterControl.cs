using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public enum TransitionParameters
    {
        Move,
    }

    public class CharacterControl : MonoBehaviour
    {
        public float movementSpeed;
        public Animator skinnedMesh;
        public Material mat;
        public bool MoveRight;
        public bool MoveLeft;

        public void changeMaterial()
        {
            if (null == mat)
            {
                Debug.LogError("No Material Specified");
            }

            Renderer[] arr = GetComponentsInChildren<Renderer>();

            foreach (Renderer r in arr)
            {
                if (this.gameObject != r.gameObject)
                {
                    r.material = mat;
                }
            }
        }
    }
}

