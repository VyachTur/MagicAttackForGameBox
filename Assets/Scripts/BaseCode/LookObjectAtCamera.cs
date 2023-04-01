using UnityEngine;

namespace BaseCode
{
    public class LookObjectAtCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Start() =>
            _mainCamera = Camera.main;

        private void Update() =>
            transform.rotation = Quaternion.AngleAxis(_mainCamera.transform.rotation.eulerAngles.x, Vector3.right);
    }
}

