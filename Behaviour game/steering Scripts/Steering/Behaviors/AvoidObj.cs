using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering  
{
    public class AvoidObj :  Behavior
    {
        private LayerMask AvoidLayer;
        private bool DoAvoidObj;

        private Vector3 HitPoint;

        private GameObject Me;

        

        public override void Starts(BehaviorContext _context)
        {
            base.Starts(_context);
            AvoidLayer = LayerMask.GetMask(_context.sd.AvoidLayer);
        }

        public AvoidObj(GameObject me)
        {
            Me = me;
            
        }

        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            

            Ray ray = new Ray(Me.transform.position, Me.transform.forward);

            RaycastHit hit;

            DoAvoidObj = Physics.Raycast(ray, out hit ,_context.sd.AvoidDistance, AvoidLayer);

            if (DoAvoidObj == false)
            {
                return Vector3.zero;
            }
            else
            {

            }

            HitPoint = hit.point;

            VelocityDesired = (hit.point - hit.collider.transform.position).normalized * _context.sd.AvoidForce;

            float angle = Vector3.Angle(VelocityDesired, _context.Velocity);
            if (angle > 179)
            {
                VelocityDesired = Vector3.Cross(Vector3.up, _context.Velocity);
            }

            PositionTarget = _context.Position + VelocityDesired;
            return VelocityDesired - _context.Velocity;
        }



        public override void OndrawGizmos(BehaviorContext _context)
        {
            //UnityEditor.Handles.color = Color.cyan;
            //UnityEditor.Handles.DrawSolidDisc(PositionTarget, Vector3.up, 0.25f);



            if (_context.sd.AvoidForce <= 0f)
            {
                return;
            }

            DebugGizmos.DrawRay(_context.Position, _context.Velocity.normalized * _context.sd.AvoidDistance, DoAvoidObj ? Color.black : Color.grey);

            if (DoAvoidObj)
            {
                
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(HitPoint, 0.25f);
                DebugGizmos.DrawRay(HitPoint, VelocityDesired,Color.green);

            }

        }
    } 
}
