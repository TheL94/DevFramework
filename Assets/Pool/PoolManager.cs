using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.Pool
{
    /// <summary>
    /// Classe che si occupa della gestione dei Pool.
    /// </summary>
    public class PoolManager : MonoBehaviour
    {
        public List<PoolData> Datas = new List<PoolData>();
        List<PoolStruct> pools = new List<PoolStruct>();

        #region API
        /// <summary>
        /// Funzione che inizializza il PoolManager. 
        /// </summary>
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

        /// <summary>
        /// Funzione che dato un id ritorna un uno degli oggetti del Pool associato a quell'id.
        /// </summary>
        /// <param name="_id">L'id dell'oggetto che si vuole richiedere</param>
        /// <returns>Ritorna il GameObject associato all'_id</returns>
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

        /// <summary>
        /// Funzione che chiama l'update del Pool associato all'id.
        /// </summary>
        /// <param name="_id"></param>
        public void ResetObject(string _id)
        {
            foreach (PoolStruct pool in pools)
            {
                if (_id == pool.Data.ID)
                {
                    pool.ObjectPool.UpdatePool();
                    break;
                }
            }
        }

        /// <summary>
        /// Funzione che forza il reset di tutti i Pool.
        /// </summary>
        public void ForcePoolReset()
        {
            foreach (PoolStruct pool in pools)
            {
                pool.ObjectPool.ForcePoolReset();
            }
        }
        #endregion

        /// <summary>
        /// Funzione che crea le istanze che vengono salvate in una nuova PoolStuct.
        /// </summary>
        /// <param name="_data"></param>
        void CreateNewPool(PoolData _data)
        {
            PoolData Data = GameObject.Instantiate(_data);
            PoolStruct tempStruct = new PoolStruct(Data, new GameObjectPool(Data.Graphic, Instantiate(new GameObject(Data.ID + "Pool"), transform).transform, Data.Quantity));
            pools.Add(tempStruct);
        }

        /// <summary>
        /// Struttura che viene utilizzata per associare il Pool al PoolData associato. (Solo per uso interno)
        /// </summary>
        struct PoolStruct
        {
            public PoolData Data;
            public GameObjectPool ObjectPool;

            public PoolStruct(PoolData _data, GameObjectPool _objectPool)
            {
                Data = _data;
                ObjectPool = _objectPool;
            }
        }
    }
}