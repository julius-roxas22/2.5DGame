using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public class PoolManager : Singleton<PoolManager>
    {
        public Dictionary<PoolObjectType, List<GameObject>> PoolDictionary = new Dictionary<PoolObjectType, List<GameObject>>();

        public void SetUpDictionary()
        {
            PoolObjectType[] arr = System.Enum.GetValues(typeof(PoolObjectType)) as PoolObjectType[];

            foreach (PoolObjectType p in arr)
            {
                if (!PoolDictionary.ContainsKey(p))
                {
                    PoolDictionary.Add(p, new List<GameObject>());
                }
            }
        }

        public GameObject InstantiatePrefab(PoolObjectType objectType)
        {
            if (PoolDictionary.Count == 0)
            {
                SetUpDictionary();
            }

            List<GameObject> list = PoolDictionary[objectType];
            GameObject obj = null;
            if (list.Count > 0)
            {
                obj = list[0];
                list.RemoveAt(0);
            }
            else
            {
                obj = PoolObjectLoader.InstantiateObject(objectType).gameObject;
            }
            return obj;
        }

        public void AddObject(PoolObject poolObject)
        {
            List<GameObject> list = PoolDictionary[poolObject.objectType];
            list.Add(poolObject.gameObject);
            poolObject.gameObject.SetActive(false);
        }
    }
}

