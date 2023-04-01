using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Effects;

namespace Player.UI
{
    public class AreaAttackViewUpdater : MonoBehaviour
    {
        [SerializeField] private Image _areaAttackImage;
        [SerializeField] private AreaEffectInstantiate _effect;

        private void Awake()
        {
            _effect.OnEffectReloadEvent += OnAreaAttackReload;
        }

        private async void OnAreaAttackReload()
        {
            float fill = _effect.TimeToEffectReload;

            for (float time = 0f; time < fill; time += Time.deltaTime)
            {
                Fill(time / fill);
                await UniTask.Yield();
            }

            Fill(1);
        }

        private void Fill(float amount)
        {
            if (_areaAttackImage != null)
                _areaAttackImage.fillAmount = amount;
        }

        private void OnDestroy()
        {
            _effect.OnEffectReloadEvent += OnAreaAttackReload;
        }
    }
}
