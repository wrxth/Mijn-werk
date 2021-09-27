using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsWanderNode : Node
{
    private steering.PlayUnit Pu;

    public IsWanderNode(steering.PlayUnit _pu)
    {
        Pu = _pu;
    }
    public override NodeState Evalute()
    {
        return Pu.bs == steering.BehaviorStatus.WANDER ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
