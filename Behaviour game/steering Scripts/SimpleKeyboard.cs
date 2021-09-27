using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SimpleSteering
{


    public class SimpleKeyboard : DebugGizmos
    {

        public enum ControlMode
        {
            KEYBOARD_MODE = 0,
            MOUSE_MODE
        }
        [Header("steering settings")]
        [SerializeField] private SteeringData sd;


        [Header("runtime information")]
        public Vector3 Velocity = Vector3.zero;
        public Vector3 VelocityDesired = Vector3.zero;
        public Vector3 Position = Vector3.zero;
        public Vector3 PositionTarget = Vector3.zero;
        public Vector3 Steering = Vector3.zero;


        [SerializeField] private ControlMode cm; // changing control mode;
        private void Start()
        {
            Position = transform.position;
        }

        private void Update()
        {
            if (cm == ControlMode.KEYBOARD_MODE)
            {
                //Keyboard: get requested dir from input
                Vector3 requestedDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));


                //Keyboard: determine target pos
                if (requestedDirection != Vector3.zero)
                {
                    PositionTarget = Position + requestedDirection.normalized * sd.MaxDesiredVelocity;
                }
                else
                {
                    PositionTarget = Position;
                }
            }
            else if (cm == ControlMode.MOUSE_MODE)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
                    {
                        PositionTarget = hit.point;
                        PositionTarget.y = Position.y;
                    }
                }
            }

        }

        private void FixedUpdate()
        {
            // Steering: calculate desired velocity and steering force for this behavior
            VelocityDesired = (PositionTarget - Position).normalized * sd.MaxDesiredVelocity;
            Vector3 steeringForce = VelocityDesired - Velocity;

            //niet duidelijk gemaakt of dit in update of fixed update moet mogelijk error(

            //Steering general: calculate steering force
            Steering = Vector3.zero;
            Steering += steeringForce;

            //Steering general: clamp steering force to max steering force and apply mass
            Steering = Vector3.ClampMagnitude(Steering, sd.MaxSteeringForce);
            Steering /= sd.Mass;

            //)

            //steering general: updat velocity with steering force, and update pos

            Velocity = Vector3.ClampMagnitude(Velocity + Steering, sd.MaxSpeed);
            Position += Velocity * Time.fixedDeltaTime;

            // update object with new pos
            transform.position = Position;
            transform.LookAt(Position + Time.fixedDeltaTime * Velocity);

        }

        private void OnDrawGizmos()
        {

            DrawRay(transform.position, Velocity, Color.red);
            DrawRay(transform.position, VelocityDesired, Color.blue);
            DrawLabel(transform.position, name, Color.red);

            //UnityEditor.Handles.color = Color.cyan;
            //UnityEditor.Handles.DrawSolidDisc(PositionTarget, Vector3.up, 0.25f);
        }

        //static void DrawRay(Vector3 _pos, Vector3 _dir, Color _color)
        //{
        //    if (_dir.sqrMagnitude > 0.001f)
        //    {
        //        Debug.DrawRay(_pos, _dir, _color);
        //        UnityEditor.Handles.color = _color;
        //        UnityEditor.Handles.DrawSolidDisc(_pos + _dir, Vector3.up, 0.25f);
        //    }
        //}
        //static void DrawLabel(Vector3 _pos, string _label, Color _color)
        //{
        //    UnityEditor.Handles.BeginGUI();
        //    UnityEditor.Handles.color = _color;
        //    UnityEditor.Handles.Label(_pos, _label);
        //    UnityEditor.Handles.EndGUI();
        //}
    }
}
