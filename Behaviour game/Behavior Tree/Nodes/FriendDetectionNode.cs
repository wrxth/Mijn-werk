using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendDetectionNode : Node
{
    private steering.PlayUnit Pu;

    private int SquadThreshold;
    private FriendDetectionNode(steering.PlayUnit _pu , int _squadThreshold)
    {
        Pu = _pu;
        SquadThreshold = _squadThreshold;
    }
    public override NodeState Evalute()
    {
        Debug.Log("friend detect");

        return Pu.FriendColliders.Length > SquadThreshold ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
