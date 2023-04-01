using System;
using UnityEngine;

namespace Player.Input
{
    public interface IInputMove
    {
        event Action<Vector2> OnPlayerMoveStartEvent;
        event Action<Vector2> OnPlayerMoveEndEvent;
    }
}

