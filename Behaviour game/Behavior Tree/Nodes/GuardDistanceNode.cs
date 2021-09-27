using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardDistanceNode : Node
{
    private GameObject[] GuardPath;
    private float MaxTravelRange;
    private GameObject Me;
    public GuardDistanceNode(GameObject[] _guardPath, float _maxTravelRange, GameObject _me)
    {
        GuardPath = _guardPath;
        MaxTravelRange = _maxTravelRange;
        Me = _me;
    }
    public override NodeState Evalute()
    {
        Vector3 pathCenter = GuardPath[0].transform.position - GuardPath[1].transform.position;
        float distance = Vector3.Distance(Me.transform.position, pathCenter); ;
        return distance > MaxTravelRange ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
