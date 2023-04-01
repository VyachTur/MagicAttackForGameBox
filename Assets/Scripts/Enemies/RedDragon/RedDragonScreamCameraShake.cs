using UnityEngine;
using BaseCode;

namespace Enemies.RedDragon
{
    public class RedDragonScreamCameraShake : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _magnitude;

        private CameraShake _cameraShake;

        private void Start()
        {
            _cameraShake = Camera.main.GetComponent<CameraShake>();
        }

        public void MainCameraShake()
        {
            if (_cameraShake != null)
            {
                StartCoroutine(_cameraShake.Shake(_duration, _magnitude));
            }
        }
    }
}
