using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGuardBNode : Node
{
    private steering.PlayUnit Pu;

    public IsGuardBNode(steering.PlayUnit _pu)
    {
        Pu = _pu;
    }
    public override NodeState Evalute()
    {
        return Pu.bs == steering.BehaviorStatus.GUARD_B ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
