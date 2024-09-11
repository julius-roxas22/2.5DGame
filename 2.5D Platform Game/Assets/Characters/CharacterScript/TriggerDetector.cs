using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class TriggerDetector : MonoBehaviour
    {
        private CharacterControl owner;

        private void Awake()
        {
            owner = GetComponentInParent<CharacterControl>();
        }

        private void OnTriggerEnter(Collider col)
        {
            if (owner.RagdollParts.Contains(col))
            {
                return;
            }

            CharacterControl attacker = col.transform.root.GetComponent<CharacterControl>();

            if (null == attacker)
            {
                return;
            }

            if (col.gameObject == attacker.gameObject)
            {
                return;
            }

            if (!owner.CollidingParts.Contains(col))
            {
                owner.CollidingParts.Add(col);
            }

        }

        private void OnTriggerExit(Collider attacker)
        {
            if (owner.CollidingParts.Contains(attacker))
            {
                owner.CollidingParts.Remove(attacker);
            }
        }
    }
}

