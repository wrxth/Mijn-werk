using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealNode : Node
{
    private steering.PlayUnit Pu;
    public HealNode(steering.PlayUnit _pu)
    {
        Pu = _pu;
    }
    public override NodeState Evalute()
    {
        Debug.Log("heal node");

        if (Pu.CurrentMoveType != "hn")
        {
            Pu.Behaviors.Clear();
            Pu.Behaviors.Add(new steering.Seek(Pu.HealLocation));
            Pu.Behaviors.Add(new steering.AvoidObj(Pu.gameObject));
            Pu.Steerings.SetBehaviors(Pu.Behaviors, "Fleeing");
            Pu.CurrentMoveType = "hn";

            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }   
}
