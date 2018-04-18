using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.Pool
{
    public class PoolData : ScriptableObject
    {
        public string ID;
        public GameObject Graphic;
        public int Quantity;
    }
}