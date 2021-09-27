using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToDesNode : Node
{
    steering.PlayUnit Pu;
    public GoToDesNode(steering.PlayUnit _pu) 
    {
        Pu = _pu;
    }
    public override NodeState Evalute()
    {

        if (Pu.CurrentMoveType != "gtdn")
        {

            Pu.Behaviors.Clear();
            Pu.Behaviors.Add(new steering.FollowPath(Pu.Destinations,Random.Range(0,Pu.Destinations.Length)));
            Pu.Behaviors.Add(new steering.AvoidObj(Pu.gameObject));
            Pu.Steerings.SetBehaviors(Pu.Behaviors, "Aggresive");
            Pu.CurrentMoveType = "gtdn";
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }

    
}
