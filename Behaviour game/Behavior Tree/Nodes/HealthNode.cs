using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNode : Node
{
    private Health Ai;
    private float ThresHold;

    public HealthNode(Health _ai, float _thresHold)
    {
        Ai = _ai;
        ThresHold = _thresHold;
    }

    public override NodeState Evalute()
    {
        // checkt of de unit low health is
        return Ai.CurrentHealth <= ThresHold ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
