using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.Pool
{
    [CreateAssetMenu(fileName = "PoolData",menuName = "Pool")]
    public class PoolData : ScriptableObject
    {
        public string ID;
        public GameObject Graphic;
        public int Quantity;
    }
}