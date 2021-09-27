using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    // gemaakt door: gabriel
    protected List<Node> Nodes = new List<Node>();

    public Selector(List<Node> _nodes)
    {
        this.Nodes = _nodes;
    }
    public override NodeState Evalute()
    {
        // dit returnt succes voor de eerste succes die hij tegenkomt
        foreach(var node in Nodes)
        {
            switch (node.Evalute())
            {
                case NodeState.RUNNING:
                    ns = NodeState.RUNNING;
                    return ns;
                case NodeState.SUCCESS:
                    ns = NodeState.SUCCESS;
                    return ns;
                case NodeState.FAILURE:

                default:
                    break;
            }
        }
        ns = NodeState.FAILURE;
        return ns;
    }
}
