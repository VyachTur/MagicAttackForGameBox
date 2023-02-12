using UnityEngine;
using EmptyMarkers;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private CameraTargetPointMarker _targetPoint;
    [SerializeField] private float _followSpeed;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPoint.transform.position, Time.deltaTime * _followSpeed);
    }
}
