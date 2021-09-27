using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering  
{
    public class Mouse : Behavior
    {
        private GameObject Me;
        public Mouse(GameObject _me)
        {
            Me = _me;
        }

        public override void Starts(BehaviorContext _context)
        {
            base.Starts(_context);
            PositionTarget = Me.transform.position;
        }

        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            //Debug.Log(Me);

            if (Me.GetComponent<SelectionObj>() != null)
            {
                Debug.Log("ping");
            }
            if (Input.GetMouseButtonDown(1) && Me.GetComponent<SelectionObj>() != null)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
                {
                    PositionTarget = hit.point;
                    
                }
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
