using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPoint : Node
{
    private GameObject DefenseTarget;
    private steering.PlayUnit Pu;
    public ReturnToPoint(GameObject _defenseTarget, steering.PlayUnit _pu)
    {
        DefenseTarget = _defenseTarget;
        Pu = _pu;
    }
    public override NodeState Evalute()
    {
        if (Pu.CurrentMoveType != "rtp")
        {
            Debug.Log("ping");
            Pu.Behaviors.Clear();
            Pu.Behaviors.Add(new steering.Seek(DefenseTarget));
            Pu.Behaviors.Add(new steering.AvoidObj(Pu.gameObject));
            Pu.Steerings.SetBehaviors(Pu.Behaviors, "Defensive");
            Pu.CurrentMoveType = "rtp";
            //Invoke("ReturnToBase",2f);
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;

        }
    }

    public void ReturnToBase()
    {
      
    }

   
}
