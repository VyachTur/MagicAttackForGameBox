using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class LoopSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioStart;
        private AudioSource _audioLoop;

        private void Start()
        {
            _audioLoop = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!(_audioStart.isPlaying || _audioLoop.isPlaying))
            {
                _audioLoop.Play();
            }

        }
    }
}

