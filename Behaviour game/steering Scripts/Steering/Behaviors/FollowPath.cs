using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering  
{
    public class FollowPath : Behavior
    {
        private GameObject[] Target;

        private int CurrentTarget;

        public FollowPath(GameObject[] target,int _firstTarget)
        {
            this.Target = target;
            CurrentTarget = _firstTarget;
        }

        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            PositionTarget = Target[CurrentTarget].transform.position;

            if (Vector3.Distance(_context.Position, Target[CurrentTarget].transform.position) < 3)
            {
                CurrentTarget = Random.Range(0, Target.Length);
            }

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
