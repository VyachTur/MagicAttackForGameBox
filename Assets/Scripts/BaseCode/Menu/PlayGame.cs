using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaseCode.Menu
{
    public class PlayGame : MonoBehaviour
    {
        public void Play() =>
            SceneManager.LoadScene(1);
    }
}

