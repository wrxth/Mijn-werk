using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wanderNode : Node
{
    steering.PlayUnit Pu;
    public wanderNode(steering.PlayUnit _pu) 
    {
        Pu = _pu;
    }
    public override NodeState Evalute()
    {
        if (Pu.CurrentMoveType != "wn")
        {
            Pu.Behaviors.Clear();
            Pu.Behaviors.Add(new steering.Wander());
            Pu.Behaviors.Add(new steering.AvoidObj(Pu.gameObject));
            Pu.Steerings.SetBehaviors(Pu.Behaviors, "wander");
            Pu.CurrentMoveType = "wn";
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }

    
}
