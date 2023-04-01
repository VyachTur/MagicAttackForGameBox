using UnityEngine;
using System;

namespace Environments
{
    public class AnimatorEventsReporter : MonoBehaviour
    {
        public event Action OnMakeAttackEvent;

        private void MakeAttack() => 
            OnMakeAttackEvent?.Invoke();
    }
}

