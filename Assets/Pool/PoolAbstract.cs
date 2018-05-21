using System.Collections.Generic;

namespace UnityFramework.Pool
{
    public abstract class Pool<PoolType>
    {
        public List<PoolType> inactivePool = new List<PoolType>();
        public List<PoolType> activePool = new List<PoolType>();

        protected PoolType objectToPool;

        #region API
        /// <summary>
        /// Ritorna il primo oggetto della lista degli inattivi, se non ne ha, ne crea uno.
        /// </summary>
        /// <returns></returns>
        public virtual PoolType Get()
        {
            UpdatePool();

            if (inactivePool.Count > 0)
            {
                return GetInactiveObject();
            }
            else
            {
                PoolType item = InstantiatePoolObject();
                activePool.Add(item);
                return item;
            }
        }

        /// <summary>
        /// Controlla se c'è qualche oggetto inattivo nella lista di quelli attivi, e lo riassegna alla lista giusta.
        /// </summary>
        public virtual void UpdatePool()
        {
            for (int i = 0; i < activePool.Count; i++)
            {
                PoolType item = activePool[i];
                if (!IsObjectActive(item))
                {
                    ChangeObjectState(item, false);
                    activePool.Remove(item);
                    inactivePool.Add(item);
                    ResetPoolObject(item);
                }
            }
        }

        /// <summary>
        /// Riassegna tutti gli elementi della lista degli attivi alla lista degli inattivi.
        /// </summary>
        public void ForcePoolReset()
        {
            for (int i = 0; i < activePool.Count; i++)
            {
                PoolType item = activePool[i];
                ChangeObjectState(item, false);
                inactivePool.Add(item);
                ResetPoolObject(item);
            }
            activePool.Clear();
        }
        #endregion

        /// <summary>
        /// Inizializza il pool con il tipo passato come parametro e la quantità.
        /// </summary>
        /// <param name="_objectToPool">Il tipo che deve instanziare</param>
        /// <param name="_initialQuantity">La quantità iniziale di oggetti</param>
        protected void InitializeObjectPool(PoolType _objectToPool, int _initialQuantity)
        {
            objectToPool = _objectToPool;

            for (int i = 0; i < _initialQuantity; i++)
            {
                PoolType item = InstantiatePoolObject();
                inactivePool.Add(item);
                ChangeObjectState(item, false);
            }
        }

        /// <summary>
        /// Restituisce il primo oggetto dalla lista di inattivi.
        /// </summary>
        /// <returns></returns>
        protected PoolType GetInactiveObject()
        {
            PoolType item = inactivePool[0];
            inactivePool.RemoveAt(0);
            activePool.Add(item);
            ChangeObjectState(item, true);
            return item;
        }

        /// <summary>
        /// Resetta i valori di default dell'oggetto che viene ripreso dal Pool.
        /// </summary>
        /// <param name="_item">Oggetto da resettare</param>
        protected virtual void ResetPoolObject(PoolType _item) { }
        /// <summary>
        /// Ritorna l'oggetto base del Pool
        /// </summary>
        /// <returns></returns>
        protected virtual PoolType GetPoolType() { return objectToPool; }

        /// <summary>
        /// Instanzia l'oggetto
        /// </summary>
        /// <returns></returns>
        protected abstract PoolType InstantiatePoolObject();
        /// <summary>
        /// Cambia lo stato dell'oggetto passato come parametro.
        /// </summary>
        /// <param name="item">L'oggetto a cui viene cambiato stato</param>
        /// <param name="toState">Lo stato che viene settato all'oggetto</param>
        protected abstract void ChangeObjectState(PoolType item, bool toState);
        /// <summary>
        /// Ritorna lo stato dell'oggetto
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected abstract bool IsObjectActive(PoolType item);
    }
}