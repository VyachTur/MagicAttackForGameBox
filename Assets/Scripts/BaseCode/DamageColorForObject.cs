using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

namespace BaseCode
{
    public class DamageColorForObject : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Color _damageColor;
        private Color _startColor;

        private void Start()
        {
            _startColor = Color.black;
            _renderer.sharedMaterial.SetColor("_EmissionColor", _startColor);
        }

        public void SetColor(float seconds) =>
            SetDamageColorFromSecond(seconds).Forget();

        private async UniTaskVoid SetDamageColorFromSecond(float time)
        {
            _renderer.sharedMaterial.SetColor("_EmissionColor", _damageColor);

            await UniTask.Delay(TimeSpan.FromSeconds(time));

            if (_renderer != null)
                _renderer.sharedMaterial.SetColor("_EmissionColor", _startColor);
        }
    }
}

