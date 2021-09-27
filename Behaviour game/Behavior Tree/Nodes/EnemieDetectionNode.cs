using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieDetectionNode : Node
{
    // gemaakt door: gabriel
    private steering.PlayUnit Pu;
    public EnemieDetectionNode(steering.PlayUnit _pu)
    {
        Pu = _pu;
    }
    public override NodeState Evalute()
    {
        // checkt of er enemies gedetect zijn
        return Pu.EnemieColliders.Length != 0 ? NodeState.SUCCESS : NodeState.FAILURE;
    }

   
}
