using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToFriendNode : Node
{
    private steering.PlayUnit Pu;
    public ReturnToFriendNode(steering.PlayUnit _pu)
    {
        Pu = _pu;
    }
    public override NodeState Evalute()
    {
        if (Pu.CurrentMoveType != "rtfn")
        {
            Pu.Behaviors.Clear();
            Pu.Behaviors.Add(new steering.Seek(Pu.FriendColliders[0].gameObject));
            Pu.Behaviors.Add(new steering.AvoidObj(Pu.gameObject));
            Pu.Steerings.SetBehaviors(Pu.Behaviors, "Defensive");
            Pu.CurrentMoveType = "rtp";
            //Invoke("ReturnToBase", 2f);
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;

        }
    }

    private void ReturnToBase()
    {
        
    }

   
}
