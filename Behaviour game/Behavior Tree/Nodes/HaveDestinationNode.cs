using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveDestinationNode : Node
{
    private steering.PlayUnit Pu;
    public HaveDestinationNode(steering.PlayUnit _pu)
    {
        Pu = _pu;
    }
    public override NodeState Evalute()
    {
        return Pu.Destinations.Length != 0 ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    

}
