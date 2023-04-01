using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BaseCode
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        public T Obj { get; private set; }
        public bool AutoExpand { get; set; }
        public Transform Container { get; }

        private List<T> _pool;

        public ObjectPool(T obj, int count, Transform container)
        {
            Obj = obj;
            Container = container;

            CreatePool(count);
        }

        private void CreatePool(int count)
        {
            _pool = new List<T>();

            for (int i = 0; i < count; i++)
            {
                CreateObject(Obj);
            }
        }

        public T CreateObject(T element, bool isActiveByDefault = false)
        {
            if (element != Obj) Obj = element;

            T createdObject = Object.Instantiate(Obj, Container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdObject);

            return createdObject;
        }

        public int Count => _pool.Count;

        public int ActiveElementsCount =>
            _pool.Where(elem => elem.isActiveAndEnabled == true).Count();

        public T GetFreeElement(T obj)
        {
            if (TryGetFreeElement(out T element, obj))
                return element;

            if (AutoExpand) return CreateObject(obj, true);

            throw new System.Exception($"There is no free elements in pool of type {typeof(T)}");
        }

        private bool TryGetFreeElement(out T element, T obj)
        {
            var foundConditionElement = _pool.Where(elem => !elem.gameObject.activeInHierarchy)
                                             .Where(elem => elem == obj).FirstOrDefault();

            if (foundConditionElement != null)
            {
                element = foundConditionElement;
                element.gameObject.SetActive(true);
                return true;
            }

            element = null;
            return false;
        }
    }
}
