using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameDev
{
    public enum PoolObjectType
    {
        ATTACKINFO,
    }
    public class PoolObjectLoader : MonoBehaviour
    {
        public static PoolObject InstantiateObject(PoolObjectType objectType)
        {
            GameObject obj = null;
            switch (objectType)
            {
                case PoolObjectType.ATTACKINFO:
                    {
                        obj = Instantiate(Resources.Load("AttackInfo", typeof(GameObject))) as GameObject;
                        break;
                    }
            }
            return obj.GetComponent<PoolObject>();
        }
    }

}
