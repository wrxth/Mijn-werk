using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering
{
    public abstract class Behavior : MonoBehaviour, Ibehavior
    {
        [Header("behavior runtime")]
        public Vector3 VelocityDesired = Vector3.zero;
        public Vector3 PositionTarget = Vector3.zero;


        public virtual void Starts(BehaviorContext _context)
        {
            PositionTarget = _context.Position;
        }

        public abstract Vector3 CalculateSteeringForce(float dt, BehaviorContext _context);


        public virtual void OndrawGizmos(BehaviorContext _context)
        {
            DebugGizmos.DrawRay(_context.Position, VelocityDesired, Color.blue);
        }
    
    }

   
}
