using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OddsCheckNode : Node
{
    steering.PlayUnit Pu;
    public OddsCheckNode(steering.PlayUnit _pu)
    {
        Pu = _pu;
    }
    public override NodeState Evalute()
    {
        return (Pu.FriendColliders.Length + 1) * 3 < Pu.EnemieColliders.Length  ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
