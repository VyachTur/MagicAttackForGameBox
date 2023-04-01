using UnityEngine;
using System;

namespace Environments
{
    public class PlusExp : Loot
    {
        [SerializeField] private float _timeDestroyed = 7f;
        public static event Action<Transform> OnPlusExpCreateEvent;

        private void Start()
        {
            OnPlusExpCreateEvent?.Invoke(transform);
            Destroy(gameObject, _timeDestroyed);
        }
    }
}

