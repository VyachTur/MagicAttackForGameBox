using UnityEngine;
using Player.Input;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerLook : MonoBehaviour
    {
        private IInputLook _inputLook;
        private int _groundLayerMask;

        private void Awake()
        {
            _inputLook = GetComponent<PlayerInput>();
            _inputLook.OnPlayerLookEvent += SetLookDirection;

            _groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
        }

        private void SetLookDirection(Vector2 mousePosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _groundLayerMask))
            {
                Vector3 targetPoint = new Vector3(hit.point.x, 0f, hit.point.z);
                transform.LookAt(targetPoint);
                transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w); // reset XZ-rotation
            }
        }

        private void OnDestroy()
        {
            _inputLook.OnPlayerLookEvent -= SetLookDirection;
        }
    }
}

