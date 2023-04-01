using UnityEngine;
using UnityEngine.UI;
using Player;
using Cysharp.Threading.Tasks;

namespace Player.UI
{
    public class DashViewUpdater : MonoBehaviour
    {
        [SerializeField] private Image _dashImage;
        [SerializeField] private PlayerDash _playerDash;

        private void Awake()
        {
            _playerDash.OnDashReloadEvent += OnDashReload;
        }

        private async void OnDashReload()
        {
            float fill = _playerDash.TimeToDashReload;

            for (float time = 0f; time < fill; time += Time.deltaTime)
            {
                Fill(time / fill);
                await UniTask.Yield();
            }

            Fill(1);
        }

        private void Fill(float amount)
        {
            if (_dashImage != null)
                _dashImage.fillAmount = amount;
        }

        private void OnDestroy()
        {
            _playerDash.OnDashReloadEvent -= OnDashReload;
        }
    }
}
