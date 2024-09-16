using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public enum GeneralBodyParts
    {
        UPPER,
        LOWER,
        ARM,
        LEG
    }
    public class TriggerDetector : MonoBehaviour
    {
        private CharacterControl owner;
        public List<Collider> CollidingParts = new List<Collider>();
        public GeneralBodyParts BodyPart;

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

            if (!CollidingParts.Contains(col))
            {
                CollidingParts.Add(col);
            }

        }

        private void OnTriggerExit(Collider attacker)
        {
            if (CollidingParts.Contains(attacker))
            {
                CollidingParts.Remove(attacker);
            }
        }
    }
}

