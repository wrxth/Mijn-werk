using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceNode : Node
{
    // gemaakt door gabriel
    private GameObject DefenseTarget;
    private float MaxTravelRange;
    private GameObject Me;
    public DistanceNode(GameObject _defenseTarget, float _maxTravelRange , GameObject _me)
    {
        DefenseTarget = _defenseTarget;
        MaxTravelRange = _maxTravelRange;
        Me = _me;
    }
    public override NodeState Evalute()
    {
        // checkt hoe ver de unit is van het punt dat hij moet defenden
        float distance = Vector3.Distance(Me.transform.position, DefenseTarget.transform.position);
        return distance > MaxTravelRange ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
