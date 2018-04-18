using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.Pool
{
    public class PoolManager : MonoBehaviour
    {
        public List<PoolData> Datas = new List<PoolData>();
        List<PoolStruct> pools = new List<PoolStruct>();

        public void Init()
        {
            if(Datas.Count > 0)
            {
                foreach (PoolData data in Datas)
                {
                    CreateNewPool(data);
                }
            }
        }

        public GameObject GetObject(string _id)
        {
            foreach (PoolStruct pool in pools)
            {
                if(_id == pool.Data.ID)
                {
                    return pool.ObjectPool.Get();
                }
            }
            return null;

        }

        void CreateNewPool(PoolData _data)
        {
            PoolStruct tempStruct = new PoolStruct { Data = GameObject.Instantiate(_data), ObjectPool = new GameObjectPool(_data.Graphic, Instantiate(new GameObject(_data.ID + "Pool"), transform).transform, _data.Quantity) };
            pools.Add(tempStruct);
        }
    }

    public struct PoolStruct
    {
        public PoolData Data;
        public GameObjectPool ObjectPool;
    }
}