using UnityEngine;
using TMPro;

namespace UI
{
    public class FpsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _fpsCounterText;
        [SerializeField] private float _unpdateDelay;
        [SerializeField] private int _maximumFpsView = 300;

        private float _time;
        private string[] _fpsStringNumbers;

        private void Start()
        {
            _fpsStringNumbers = new string[_maximumFpsView + 1];

            for (int i = 0; i < _fpsStringNumbers.Length; i++)
            {
                _fpsStringNumbers[i] = i.ToString();
            }

            _fpsCounterText.text = $"FPS: {_fpsStringNumbers[0]}";
        }

        private void Update()
        {
            _time += Time.unscaledDeltaTime;

            if (_time > _unpdateDelay)
            {
                int currentFps = (int)(1 / Time.unscaledDeltaTime);
                currentFps = Mathf.Clamp(currentFps, 0, _maximumFpsView);

                _fpsCounterText.text = $"FPS: {_fpsStringNumbers[currentFps]}";

                _time = 0f;
            }
        }
    }
}