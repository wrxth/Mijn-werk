using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLoyalNode : Node
{
    private steering.PlayUnit Pu;

    public IsLoyalNode(steering.PlayUnit _pu)
    {
        Pu = _pu;
    }
    public override NodeState Evalute()
    {
        return Pu.bs == steering.BehaviorStatus.LOYAL ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
