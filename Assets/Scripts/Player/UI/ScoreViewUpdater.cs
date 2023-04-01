using TMPro;
using UnityEngine;

namespace Player.UI
{
    public class ScoreViewUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreValueText;
        private PlayerScore _playerScore;

        private void Awake()
        {
            _playerScore = FindObjectOfType<PlayerScore>();
            _playerScore.OnPlayerScoreChangeEvent += OnPlayerScoreChange;
        }

        private void OnPlayerScoreChange() => 
            _scoreValueText.text = $"Прогресс: {_playerScore.Score}";

        private void OnDestroy() => 
            _playerScore.OnPlayerScoreChangeEvent -= OnPlayerScoreChange;
    }
}
