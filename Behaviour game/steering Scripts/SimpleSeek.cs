using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSteering
{


    public class SimpleSeek : DebugGizmos
    {
        public enum npcMode
        {
            SEEK = 0,
            FLEE,
            WANDER
        }
        [Header("steering settings")]
        [SerializeField] private SteeringData sd;

        [SerializeField] private Transform PersonalTarget;
        [Header("runtime information")]
        public Vector3 Velocity = Vector3.zero;
        public Vector3 VelocityDesired = Vector3.zero;
        public Vector3 Position = Vector3.zero;
        public Vector3 PositionTarget = Vector3.zero;
        public Vector3 Steering = Vector3.zero;

        public Vector3 target = Vector3.zero;

        [SerializeField] private npcMode nm;

        [Header("Wander Settings")]

        [SerializeField] private float MinX, MaxX;
        [SerializeField] private float MinZ, MaxZ;
        private void Start()
        {
            FindNewTarget();
            Position = transform.position;
        }

        private void Update()
        {

            if (nm == npcMode.SEEK)
            {
                target = PersonalTarget.position;
                PositionTarget = target;
                PositionTarget.y = Position.y;
            }
            if (nm == npcMode.FLEE)
            {
                PositionTarget = -target;
                PositionTarget.y = Position.y;
            }

            if (nm == npcMode.WANDER)
            {
                Debug.Log(Vector3.Distance(transform.position, target));
                if (Vector3.Distance(transform.position, target) < 1)
                {
                    Debug.Log("ping");
                    FindNewTarget();
                }
                PositionTarget = target;
                PositionTarget.y = Position.y;
            }

        }


        private void FindNewTarget()
        {
            target = new Vector3(Random.Range(MinX, MaxX), 0, Random.Range(MinZ, MaxZ));
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
    }
}
