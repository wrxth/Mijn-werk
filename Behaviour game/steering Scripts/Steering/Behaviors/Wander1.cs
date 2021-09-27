using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering  
{
    public class Wander2 : Behavior
    {
        [SerializeField] private float WanderAngle;
      

        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            WanderAngle += Random.Range(0.5f * _context.sd.WanderingNoiseAngle * Mathf.Deg2Rad, -0.5f * _context.sd.WanderingNoiseAngle * Mathf.Deg2Rad);

            Vector3 centerOfCircle = _context.Position + _context.Velocity.normalized * _context.sd.WanderingCircleDistance;

            Vector3 offset = new Vector3(_context.sd.WanderingCircleRadius * Mathf.Cos(WanderAngle), 0, _context.sd.WanderingCircleRadius * Mathf.Sin(WanderAngle));

            PositionTarget = centerOfCircle + offset;

            VelocityDesired = (PositionTarget - _context.Position).normalized * _context.sd.MaxDesiredVelocity;
            return VelocityDesired - _context.Velocity;
        }



        public override void OndrawGizmos(BehaviorContext _context)
        {
            base.OndrawGizmos(_context);
            //UnityEditor.Handles.color = Color.cyan;
            //UnityEditor.Handles.DrawSolidDisc(PositionTarget, Vector3.up, 0.25f);

            

            Vector3 centerOfCircle = _context.Position + _context.Velocity.normalized * _context.sd.WanderingCircleDistance;
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(centerOfCircle, _context.sd.WanderingCircleRadius);
            float a = _context.sd.WanderingNoiseAngle * Mathf.Deg2Rad;

            Vector3 rangeMin = new Vector3(_context.sd.WanderingCircleRadius * Mathf.Cos(WanderAngle - a), 0, _context.sd.WanderingCircleRadius * Mathf.Sin(WanderAngle - a));
            Vector3 rangeMax = new Vector3(_context.sd.WanderingCircleRadius * Mathf.Cos(WanderAngle + a), 0, _context.sd.WanderingCircleRadius * Mathf.Sin(WanderAngle + a));

            Debug.DrawLine(centerOfCircle, centerOfCircle + rangeMin, Color.black);
            Debug.DrawLine(centerOfCircle, centerOfCircle + rangeMax, Color.black);

        }
    } 
}
