using Player;
using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 2.2f;
    private PlayerHealth _target;

    private void OnEnable() => 
        _target = FindObjectOfType<PlayerHealth>();

    private void Update() => 
        RotateToPlayer();

    private void RotateToPlayer()
    {
        Vector3 direction = GetDirection();
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
    }

    private Vector3 GetDirection() => 
        (_target.transform.position - transform.position).normalized;
}
