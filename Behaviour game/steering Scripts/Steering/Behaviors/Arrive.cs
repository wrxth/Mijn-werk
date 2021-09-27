using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering  
{
    public class Arrive : Behavior
    {

        public enum TargetReached
        {
            TARGET_NOT_REACHED = 0,
            TARGET_REACHED
        }
        private GameObject Target;

        public Arrive(GameObject target)
        {
            this.Target = target;
        }


        private TargetReached tr;
        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            

            if (Vector3.Distance(_context.Position,Target.transform.position) < _context.sd.ArriveDistance)
            {
                tr = TargetReached.TARGET_REACHED;
            }

            if (tr == TargetReached.TARGET_NOT_REACHED)
            {
                PositionTarget = Target.transform.position;
            }

            VelocityDesired = (PositionTarget - _context.Position).normalized * _context.sd.MaxDesiredVelocity;
            return VelocityDesired - _context.Velocity;
        }
    } 
}
