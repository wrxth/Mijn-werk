using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseNode : Node
{
    // gemaakt door: gabriel
    // script zorgt voor het chase van enemies
    private Transform Origin;
    private steering.PlayUnit Pu;

    public ChaseNode(steering.PlayUnit _pu)
    {
        Pu = _pu;
    }

    public override NodeState Evalute()
    {
        if (Pu.CurrentMoveType != "cn")       // zodat de behavior niet raar gaat wordt hij alleen een keer toegevoegd
        {
            Pu.Behaviors.Clear();
            Pu.Behaviors.Add(new steering.Seek(Pu.EnemieColliders[0].gameObject));
            Pu.Behaviors.Add(new steering.AvoidObj(Pu.gameObject));
            Pu.Steerings.SetBehaviors(Pu.Behaviors, "Fleeing");
            Pu.CurrentMoveType = "cn";
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
