using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGuardANode : Node
{
    private steering.PlayUnit Pu;

    public IsGuardANode(steering.PlayUnit _pu)
    {
        Pu = _pu;
    }
    public override NodeState Evalute()
    {
        return Pu.bs == steering.BehaviorStatus.GUARD_A ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
