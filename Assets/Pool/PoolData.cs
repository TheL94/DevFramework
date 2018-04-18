using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.Pool
{
    /// <summary>
    /// Classe che contiene i dati utilizzati dal PoolManager.
    /// </summary>
    [CreateAssetMenu(fileName = "PoolData", menuName = "Pool")]
    public class PoolData : ScriptableObject
    {
        public string ID;
        public GameObject Graphic;
        public int Quantity;
    }
}