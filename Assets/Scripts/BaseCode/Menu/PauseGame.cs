using UnityEngine;
using UnityEngine.Audio;

namespace BaseCode.Menu
{
    public class PauseGame : MonoBehaviour
    {
        [SerializeField] private AudioMixerSnapshot _snapshotMain;
        [SerializeField] private AudioMixerSnapshot _snapshotPaused;

        public void PauseOn()
        {
            Time.timeScale = 0f;
            MusicTurn(false);
        }

        public void PauseOff()
        {
            Time.timeScale = 1f;
            MusicTurn(true);
        }

        private void MusicTurn(bool isOn)
        {
            if (isOn)
                _snapshotMain.TransitionTo(1f);
            else
                _snapshotPaused.TransitionTo(1f);
        }
    }
}

