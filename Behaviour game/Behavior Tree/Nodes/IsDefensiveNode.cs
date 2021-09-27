using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDefensiveNode : Node
{
    private steering.PlayUnit Pu;

    public IsDefensiveNode(steering.PlayUnit _pu)
    {
        Pu = _pu;
    }
    public override NodeState Evalute()
    {
        return Pu.bs == steering.BehaviorStatus.DEFENSIVE ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
