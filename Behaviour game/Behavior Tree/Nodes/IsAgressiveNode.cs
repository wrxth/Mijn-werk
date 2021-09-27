using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAgressiveNode : Node
{
    private steering.PlayUnit Pu;

    public IsAgressiveNode(steering.PlayUnit _pu)
    {
        Pu = _pu;
    }
    public override NodeState Evalute()
    {
        return Pu.bs == steering.BehaviorStatus.AGGRESSIVE ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
