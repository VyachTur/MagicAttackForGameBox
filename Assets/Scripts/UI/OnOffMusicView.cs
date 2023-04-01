using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OnOffMusicView : MonoBehaviour
    {
        [SerializeField] private GameObject _musicGameObject;
        [SerializeField] private Image _onView;
        [SerializeField] private Image _offView;

        public void SwitchPlayMusic()
        {
            bool isActiveMusicObject = _musicGameObject.activeInHierarchy;

            _musicGameObject.SetActive(!isActiveMusicObject);
            _onView.enabled = !isActiveMusicObject;
            _offView.enabled = isActiveMusicObject;

        }
    }
}

