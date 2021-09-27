using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering  
{
    public class Seek : Behavior
    {
        private GameObject Target;

        public Seek(GameObject target)
        {
           Target = target;
        }

        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            PositionTarget = Target.transform.position;
            

            VelocityDesired = (PositionTarget - _context.Position).normalized * _context.sd.MaxDesiredVelocity;
            return VelocityDesired - _context.Velocity;
        }
        public override void OndrawGizmos(BehaviorContext _context)
        {
            base.OndrawGizmos(_context);
            //UnityEditor.Handles.color = Color.cyan;
            //UnityEditor.Handles.DrawSolidDisc(PositionTarget, Vector3.up, 0.25f);
        }
    } 
}
