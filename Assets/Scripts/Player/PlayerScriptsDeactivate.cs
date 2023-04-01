using UnityEngine;

namespace Player
{
    public class PlayerScriptsDeactivate : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _scripts;

        public void DisabledScripts(bool isDisable)
        {
            foreach (MonoBehaviour script in _scripts)
            {
                script.enabled = isDisable;
            }
        }
    }
}

