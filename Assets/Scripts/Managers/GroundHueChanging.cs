using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Managers
{
    public class GroundHueChanging : MonoBehaviour
    {
        [SerializeField] private Renderer _groundRenderer;
        private const float CountColors = 360f;
        private float _sat;
        private float _val;

        private void Awake() => 
            Color.RGBToHSV(_groundRenderer.material.color, out _, out _sat, out _val);

        private async void Start() => 
            await CicleChangingHue();

        private async UniTask CicleChangingHue()
        {
            for (float hue = 0f; hue <= 1.01f; hue += 0.01f)           
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(0.1), ignoreTimeScale: true);
                
                hue = Mathf.Clamp(hue, 0f, 1f);

                if (_groundRenderer != null)
                    _groundRenderer.sharedMaterial.SetColor("_BaseColor", Color.HSVToRGB(hue, _sat, _val));

                if (hue == 1f)
                {
                    hue = 0f;
                    continue;
                }
            }
        }

        public void DestroyHueChanging() =>
            Object.Destroy(this);
    }
}