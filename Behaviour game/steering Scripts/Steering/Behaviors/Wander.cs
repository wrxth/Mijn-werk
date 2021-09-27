using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering  
{
    public class Wander : Behavior
    {
        private Vector3 Target;
        private float MinZ;
        private float MaxZ;
        private float MinX;
        private float MaxX;

        public override void Starts(BehaviorContext _context)
        {
            base.Starts(_context);
            Target = new Vector3(Random.Range(_context.sd.MinX, _context.sd.MaxX), _context.Position.y, Random.Range(_context.sd.MinZ, _context.sd.MaxZ));
        }

        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            MinZ = _context.sd.MinZ;
            MinX = _context.sd.MinX;
            MaxZ = _context.sd.MaxZ;
            MaxX = _context.sd.MaxX;
            PositionTarget = Target;

            //Debug.Log(Vector3.Distance(_context.Position, Target));
            if (Vector3.Distance(_context.Position, Target) < 3)
            {
                Debug.Log("ping");
                FindNewTarget();
            }
            PositionTarget = Target;

            VelocityDesired = (PositionTarget - _context.Position).normalized * _context.sd.MaxDesiredVelocity;
            return VelocityDesired - _context.Velocity;
        }

        private void FindNewTarget()
        {
            Target = new Vector3(Random.Range(MinX, MaxX), 0, Random.Range(MinZ, MaxZ));
        }

        public override void OndrawGizmos(BehaviorContext _context)
        {
            base.OndrawGizmos(_context);
            //UnityEditor.Handles.color = Color.cyan;
            //UnityEditor.Handles.DrawSolidDisc(PositionTarget, Vector3.up, 0.25f);
        }
    } 
}
