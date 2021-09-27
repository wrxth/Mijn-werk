using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Node
{
    protected Node Node;

    public Inverter(Node _node)
    {
        this.Node = _node;
    }
    public override NodeState Evalute()
    {

        switch (Node.Evalute())
        {
            case NodeState.RUNNING:
                ns = NodeState.RUNNING;
                break;
            case NodeState.SUCCESS:
                ns = NodeState.FAILURE;
                break;
            case NodeState.FAILURE:
                ns = NodeState.SUCCESS;
                break;
            default:
                break;
        }
        return ns;
    }
}
