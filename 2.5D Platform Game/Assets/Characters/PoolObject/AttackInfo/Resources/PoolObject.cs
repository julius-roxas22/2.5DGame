using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class PoolObject : MonoBehaviour
    {
        public PoolObjectType objectType;
        public float ScheduledTime;
        public Coroutine routine;

        private void OnEnable()
        {
            if (null != routine)
            {
                StopCoroutine(routine);
            }

            if (ScheduledTime > 0f)
            {
                routine = StartCoroutine(ScheduledOff());
            }
        }

        public void TurnOff()
        {
            PoolManager.Instance.AddObject(this);
        }

        IEnumerator ScheduledOff()
        {
            yield return new WaitForSeconds(ScheduledTime);

            if (!PoolManager.Instance.PoolDictionary[objectType].Contains(gameObject))
            {
                TurnOff();
            }
        }
    }
}

