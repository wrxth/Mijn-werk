using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering
{
    // gemaakt door: gabriel
    public class AgrresiveNode : Node
    {
        private BehaviorStatus Bs;

        private AgrresiveNode(BehaviorStatus _bs)
        {
            Bs = _bs;
        }
        public override NodeState Evalute()
        {
            // returnt succes succes als de behavior status aggressive anders returnt hij failure
            return Bs == BehaviorStatus.AGGRESSIVE ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    } 
}
