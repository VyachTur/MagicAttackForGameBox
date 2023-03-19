using UnityEngine;
using EmptyMarkers;

namespace Environments.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private CameraTargetPointMarker _targetPoint;
        [SerializeField] private float _followSpeed;

        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, _targetPoint.transform.position, Time.fixedDeltaTime * _followSpeed);
        }
    }
}
