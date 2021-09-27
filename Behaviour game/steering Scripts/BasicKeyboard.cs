using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSteering
{


    public class BasicKeyboard : MonoBehaviour
    {
        [Header("steering settings")]
        public float MaxSpeed = 3f;
        public float Mass = 70f;
        public float MaxSteeringForce = 3f;
        public float MaxDesiredVelocity = 3f;


        [Header("runtime information")]
        public Vector3 Velocity = Vector3.zero;
        public Vector3 VelocityDesired = Vector3.zero;
        public Vector3 Position = Vector3.zero;
        public Vector3 PositionTarget = Vector3.zero;
        public Vector3 Steering = Vector3.zero;

        private void Start()
        {
            Position = transform.position;
        }


        private void FixedUpdate()
        {

            Vector3 requestedDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            Velocity = requestedDirection.normalized * MaxSpeed;
            Position = Position + Velocity * Time.fixedDeltaTime;

            transform.position = Position;

            transform.LookAt(transform.position + Velocity.normalized);

            Debug.DrawRay(transform.position, Velocity, Color.red);
        }

        private void OnDrawGizmos()
        {
            DrawRay(transform.position, Velocity, Color.red);
            DrawLabel(transform.position, name, Color.red);
        }

        static void DrawRay(Vector3 _pos, Vector3 _dir, Color _color)
        {
            if (_dir.sqrMagnitude > 0.001f)
            {
                Debug.DrawRay(_pos, _dir, _color);
                //UnityEditor.Handles.color = _color;
                //UnityEditor.Handles.DrawSolidDisc(_pos + _dir, Vector3.up, 0.25f);
            }
        }
        static void DrawLabel(Vector3 _pos, string _label, Color _color)
        {
        //    UnityEditor.Handles.BeginGUI();
        //    UnityEditor.Handles.color = _color;
        //    UnityEditor.Handles.Label(_pos, _label);
        //    UnityEditor.Handles.EndGUI();
        }
    }
}
