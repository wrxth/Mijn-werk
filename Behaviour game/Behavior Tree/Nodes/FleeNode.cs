using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeNode : Node
{
    steering.PlayUnit Pu;

    public FleeNode(steering.PlayUnit _pu)
    {
        Pu = _pu;
    }
    public override NodeState Evalute()
    {
        if (Pu.CurrentMoveType != "fn")
        {

            Pu.Behaviors.Clear();
            Pu.Behaviors.Add(new steering.Flee(Pu.EnemieColliders[0].gameObject));
            Pu.Behaviors.Add(new steering.AvoidObj(Pu.gameObject));
            Pu.Steerings.SetBehaviors(Pu.Behaviors, "Aggresive");
            Pu.CurrentMoveType = "fn";
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }

  
}
