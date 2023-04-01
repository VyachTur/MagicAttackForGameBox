using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    public class StartSound : MonoBehaviour
    {
        [SerializeField] private AudioMixerSnapshot _snapshotMain;

        private void Awake()
        {
            _snapshotMain.TransitionTo(0f);
        }
    }
}

