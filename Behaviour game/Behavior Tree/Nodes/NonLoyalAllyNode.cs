using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonLoyalAllyNode : Node
{
    private steering.PlayUnit Pu;

    private int SquadThreshold;
    public NonLoyalAllyNode(steering.PlayUnit _pu , int _squadThreshold)
    {
        Pu = _pu;
        SquadThreshold = _squadThreshold;
    }
    public override NodeState Evalute()
    {
        return Pu.FriendColliders.Length > SquadThreshold ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
