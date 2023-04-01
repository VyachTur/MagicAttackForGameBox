using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Environments;
using StaticData;

namespace Player
{
    public class PlayerHealthAttraction : MonoBehaviour
    {
        [SerializeField] private float _attractionRadius;
        [SerializeField] private float _attractionSpeed;
        public event Action<float> OnPlayerHealthAttractionEvent;
        private static List<Transform> _plusHealthTransforms;

        private void Awake()
        {
            _plusHealthTransforms = new();
            PlusHealth.OnPlusHealthCreateEvent += AddPlusHealthTransform;
        }

        private void AddPlusHealthTransform(Transform plusTransform)
        {
            _plusHealthTransforms.Add(plusTransform);
        }

        private void Update()
        {
            FindAndPullTransformPlusHealth();
        }

        private void FindAndPullTransformPlusHealth()
        {
            if (_plusHealthTransforms == null || _plusHealthTransforms.Count == 0)
                return;

            for (int i = 0; i < _plusHealthTransforms.Count; i++)
            {
                if (_plusHealthTransforms[i] == null) continue;

                float distance = Vector3.Distance(transform.position, _plusHealthTransforms[i].position);

                if (distance < _attractionRadius)
                {
                    PullPlusHealth(_plusHealthTransforms[i]);

                    if (distance < Constants.DeltaAttraction)
                    {
                        float heal = _plusHealthTransforms[i].GetComponent<PlusHealth>().LootValue;
                        OnPlayerHealthAttractionEvent?.Invoke(heal);

                        _plusHealthTransforms[i].gameObject.SetActive(false);
                        _plusHealthTransforms.Remove(_plusHealthTransforms[i]);
                    }
                }
            }
        }

        private void PullPlusHealth(Transform plusTransform) =>
            plusTransform.position = Vector3.MoveTowards(plusTransform.position, transform.position, _attractionSpeed * Time.deltaTime);


        private void OnDestroy()
        {
            PlusHealth.OnPlusHealthCreateEvent -= AddPlusHealthTransform;
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, _attractionRadius);
        }

#endif

    }
}
