using UnityEngine;
using Player;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class ActiveResumeGameButton : MonoBehaviour
    {
        private PlayerDie _playerDie;
        private PlayerWin _playerWin;
        private Button _resumeGameButton;

        private void Awake()
        {
            _playerDie = FindObjectOfType<PlayerDie>();
            _playerWin = FindObjectOfType<PlayerWin>();
            _resumeGameButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _resumeGameButton.interactable = !(_playerDie.IsPlayerDie || _playerWin.IsPlayerWin);
        }
    }
}

