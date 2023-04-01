using System;
using UnityEngine;

namespace Player.Input
{
    public interface IInputLook
    {
        event Action<Vector2> OnPlayerLookEvent;
    }
}

