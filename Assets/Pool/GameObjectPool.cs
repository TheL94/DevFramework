using UnityEngine;

namespace UnityFramework.Pool
{
    public class GameObjectPool : Pool<GameObject>
    {
        Transform parentObject;

        public GameObjectPool(GameObject _prefabReference, Transform _parent, int _initialQuantity)
        {
            parentObject = _parent;
            InitializeObjectPool(_prefabReference, _initialQuantity);
        }

        protected override void ResetPoolObject(GameObject _item)
        {
            _item.transform.position = parentObject.transform.position;
            _item.transform.rotation = parentObject.transform.rotation;
            _item.transform.parent = parentObject.transform;
        }

        protected override void ChangeObjectState(GameObject item, bool toState)
        {
            item.SetActive(toState);
        }

        protected override bool IsObjectActive(GameObject item)
        {
            return item.activeInHierarchy;
        }

        protected override GameObject InstantiatePoolObject(bool _debug = false)
        {
            if(_debug)
                Debug.Log("A new " + objectToPool.name + " has been instantiated. Add more initial quantity to this pool.");

            return GameObject.Instantiate(objectToPool, parentObject.position, Quaternion.identity, parentObject);
        }
    }
}