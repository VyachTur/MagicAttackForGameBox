using System;

namespace Player.Input
{
    public interface IInputAttack
    {
        event Action OnPlayerFirstAttackStartEvent;
        event Action OnPlayerFirstAttackEndEvent;
        event Action OnPlayerSecondAttackStartEvent;
        event Action OnPlayerSecondAttackEndEvent;
    }
}

