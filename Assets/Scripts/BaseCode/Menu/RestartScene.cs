using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaseCode.Menu
{
    public class RestartScene : MonoBehaviour
    {
        public void ActiveSceneRestart() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
