using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaseCode.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void MainMenuLoad() =>
                SceneManager.LoadScene(0);
    }
}

