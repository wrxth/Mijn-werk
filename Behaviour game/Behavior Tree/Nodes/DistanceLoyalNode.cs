using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceLoyalNode : Node
{
    private steering.PlayUnit Pu;
    private float MaxTravelRange;
    public DistanceLoyalNode(steering.PlayUnit _pu, float _maxTravelRange)
    {
        Pu = _pu;
        MaxTravelRange = _maxTravelRange;
    }
    public override NodeState Evalute()
    {
        Vector3 loyalTarget = Pu.FriendColliders[0].gameObject.transform.position;
        float distance = Vector3.Distance(Pu.gameObject.transform.position, loyalTarget);
        Debug.Log(distance);
        return distance > MaxTravelRange ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
