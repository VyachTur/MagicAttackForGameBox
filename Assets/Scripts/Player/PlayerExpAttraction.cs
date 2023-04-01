using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Environments;
using StaticData;

namespace Player
{
    public class PlayerExpAttraction : MonoBehaviour
    {
        [SerializeField] private float _attractionRadius;
        [SerializeField] private float _attractionSpeed;
        public event Action<float> OnPlayerExpAttractionEvent;
        private static List<Transform> _plusExpTransforms;

        private void Awake()
        {
            _plusExpTransforms = new();
            PlusExp.OnPlusExpCreateEvent += AddPlusExpTransform;
        }

        private void AddPlusExpTransform(Transform plusTransform)
        {
            _plusExpTransforms.Add(plusTransform);
        }

        private void Update()
        {
            FindAndPullTransformPlusExp();
        }

        private void FindAndPullTransformPlusExp()
        {
            if (_plusExpTransforms == null || _plusExpTransforms.Count == 0)
                return;

            for (int i = 0; i < _plusExpTransforms.Count; i++)
            {
                if (_plusExpTransforms[i] == null) continue;

                float distance = Vector3.Distance(transform.position, _plusExpTransforms[i].position);

                if (distance < _attractionRadius)
                {
                    PullPlusExp(_plusExpTransforms[i]);

                    if (distance < Constants.DeltaAttraction)
                    {
                        float heal = _plusExpTransforms[i].GetComponent<PlusExp>().LootValue;
                        OnPlayerExpAttractionEvent?.Invoke(heal);

                        _plusExpTransforms[i].gameObject.SetActive(false);
                        _plusExpTransforms.Remove(_plusExpTransforms[i]);
                    }
                }
            }
        }

        private void PullPlusExp(Transform plusTransform) =>
            plusTransform.position = Vector3.MoveTowards(plusTransform.position, transform.position, _attractionSpeed * Time.deltaTime);


        private void OnDestroy()
        {
            PlusExp.OnPlusExpCreateEvent -= AddPlusExpTransform;
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