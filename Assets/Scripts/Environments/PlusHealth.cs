using UnityEngine;
using System;

namespace Environments
{
    public class PlusHealth : Loot
    {
        [SerializeField] private float _timeDestroyed = 7f;
        public static event Action<Transform> OnPlusHealthCreateEvent;

        private void Start()
        {
            OnPlusHealthCreateEvent?.Invoke(transform);
            Destroy(gameObject, _timeDestroyed);
        }
    }
}
