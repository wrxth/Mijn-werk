using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    // gemaakt door: gabriel
    protected List<Node> Nodes = new List<Node>();

    public Sequence(List<Node> _nodes)
    {
        this.Nodes = _nodes;
    }
    public override NodeState Evalute()
    {
        // dit returnt alleen succes als alle nodes ook succes returnen
        bool isAnyNodeRunning = false;
        foreach(var node in Nodes)
        {
            switch (node.Evalute())
            {
                case NodeState.RUNNING:
                    isAnyNodeRunning = true;
                    break;
                case NodeState.SUCCESS:
                    break;
                case NodeState.FAILURE:
                    ns = NodeState.FAILURE;
                    return ns;
                default:
                    break;
            }
        }
        ns = isAnyNodeRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return ns;
    }
}
