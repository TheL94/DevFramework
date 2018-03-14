using UnityEngine;
using System.Collections;

namespace UnityFramework.Pool
{
    public class ScriptableObjectPool<PoolType> : Pool<PoolType> where PoolType : ScriptableObject
    {
        public ScriptableObjectPool(PoolType _objectToPool, int _initialQuantity)
        {
            InitializeObjectPool(_objectToPool, _initialQuantity);
        }

        protected override void ChangeObjectState(PoolType item, bool toState) { }

        protected override PoolType InstantiatePoolObject()
        {
            return ScriptableObject.Instantiate(objectToPool);
        }

        protected override bool IsObjectActive(PoolType item)
        {
            return true;
        }
    }
}