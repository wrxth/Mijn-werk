using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering  
{
    public class Flee : Behavior
    {
        private GameObject Target;

        public Flee(GameObject target)
        {
            this.Target = target;
        }

        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            PositionTarget = Target.transform.position;
            

            VelocityDesired = -(PositionTarget - _context.Position).normalized * _context.sd.MaxDesiredVelocity;
            return VelocityDesired - _context.Velocity;
        }
    } 
}
