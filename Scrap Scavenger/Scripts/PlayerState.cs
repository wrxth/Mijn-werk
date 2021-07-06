using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : Singleton<PlayerState>
{
    public enum CurrentState
    {
        MOVING,
        GOING_TO_STORE,
        AT_STORE
    }
    public CurrentState Cs;
}
