using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardnode : Node
{
    private steering.PlayUnit Pu;
    private GameObject[] GuardPath;
    private GameObject Me;

    public Guardnode(steering.PlayUnit _pu, GameObject[] _guardPath, GameObject _me) 
    {
        Pu = _pu;
        GuardPath = _guardPath;
        Me = _me;
    }
    public override NodeState Evalute()
    {
        if (Pu.CurrentMoveType != "gn")
        {
            Pu.Behaviors.Clear();
            Pu.Behaviors.Add(new steering.FollowPath(GuardPath, 0));
            Pu.Behaviors.Add(new steering.AvoidObj(Me));
            Pu.Steerings.SetBehaviors(Pu.Behaviors, "Aggresive");
            Pu.CurrentMoveType = "gn";
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }

    
}
