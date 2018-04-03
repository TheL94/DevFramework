using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.Data
{
    /// <summary>
    /// Called to recive instances of ScriptableObjects. It also manages who need those Datas and eventually create new instances
    /// </summary>
    public static class DataManager
    {
        /// <summary>
        /// All the MonoBehaviour that have asked for a ScriptableObject Instance
        /// </summary>
        static List<MonoBehaviourData> MonoBehaviourDatas = new List<MonoBehaviourData>();

        /// <summary>
        /// Retrive a ScriptableObject's instance for a specific MonoBehaviour
        /// </summary>
        /// <param name="_monoBehaviour"></param>
        /// <param name="_originalData"></param>
        /// <returns></returns>
        public static ScriptableObject GetData(MonoBehaviour _monoBehaviour, ScriptableObject _originalData)
        {
            MonoBehaviourData controllerData = GetMonoBehaviourData(_monoBehaviour);
            ScriptableObject stateInstance = controllerData.GetDataInstance(_originalData);

            return stateInstance;
        }

        /// <summary>
        /// Check if a ControllerData for a specific MonoBehaviour is already in the MonoBehaviourData list
        /// otherwise it creates a new one
        /// </summary>
        /// <param name="_monoBehaviour">MonoBehaviour to  verify</param>
        /// <returns>Relative MonoBehaviourData</returns>
        static MonoBehaviourData GetMonoBehaviourData(MonoBehaviour _monoBehaviour)
        {
            MonoBehaviourData monoBehaviourData = null;

            foreach (MonoBehaviourData monoData in MonoBehaviourDatas)
            {
                if (monoData.DataOwner == _monoBehaviour)
                    monoBehaviourData = monoData;
            }

            if (monoBehaviourData == null)
            {
                monoBehaviourData = new MonoBehaviourData(_monoBehaviour);
                MonoBehaviourDatas.Add(monoBehaviourData);
            }

            return monoBehaviourData;
        }

        /// <summary>
        /// Class that hold the instances of data relative to a specific MonoBehaviour
        /// </summary>
        class MonoBehaviourData
        {
            public MonoBehaviour DataOwner;
            List<ScriptableObject> dataInstances = new List<ScriptableObject>();

            public MonoBehaviourData(MonoBehaviour _dataOwner)
            {
                DataOwner = _dataOwner;
            }

            /// <summary>
            /// Get the instance of the Data required. It creates a new one if missing.
            /// </summary>
            /// <param name="_data">Original Data</param>
            /// <returns>Instance of the original Data</returns>
            public ScriptableObject GetDataInstance(ScriptableObject _data)
            {
                ScriptableObject dataInstance = null;
                foreach (ScriptableObject instance in dataInstances)
                {
                    if (instance.name == _data.name)
                        dataInstance = instance;
                }

                if (dataInstance == null)
                {
                    dataInstance = ScriptableObject.Instantiate(_data);
                    dataInstances.Add(dataInstance);
                }

                return dataInstance;
            }
        }
    }
}

