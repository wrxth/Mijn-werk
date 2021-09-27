using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering  
{
    public class Pursue : Behavior
    {
        private Vector3 CurrentTargetPos;
        private Vector3 PrevTargetPos;
        private GameObject Target;

        [SerializeField] private Vector3 Speed;
        public Pursue(GameObject target)
        {
            this.Target = target;
        }

        public override void Starts(BehaviorContext _context)
        {
            base.Starts(_context);
            CurrentTargetPos = Target.transform.position;
        }

        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            PrevTargetPos = CurrentTargetPos;
            CurrentTargetPos = Target.transform.position;

            Speed = (CurrentTargetPos - PrevTargetPos) / dt;

            Vector3 futurePos = CurrentTargetPos + Speed * _context.sd.LookAheadTime;

            PositionTarget = futurePos;
            
            VelocityDesired = (PositionTarget - _context.Position).normalized * _context.sd.MaxDesiredVelocity;
            return VelocityDesired - _context.Velocity;
        }
        public override void OndrawGizmos(BehaviorContext _context)
        {
            base.OndrawGizmos(_context);
            //UnityEditor.Handles.color = Color.yellow;
            //UnityEditor.Handles.DrawSolidDisc(PositionTarget, Vector3.up, 0.25f);
        }
    } 
}
