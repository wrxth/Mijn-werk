using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering
{
    using BehaviorList = List<Ibehavior>;
    public class Steering : DebugGizmos, Ipooled
    {
        [Header("steering settings")]
        public SteeringData sd;
        public SteeringData privatesd;
        [SerializeField] private string Label;



        [Header("runtime information")]
        public Vector3 Velocity = Vector3.zero;
        //public Vector3 VelocityDesired = Vector3.zero;
        public Vector3 Position = Vector3.zero;
        //public Vector3 PositionTarget = Vector3.zero;
        public Vector3 Steerings = Vector3.zero;

        public BehaviorList Behaviors = new BehaviorList();


        public float MaxSpeed;
        public float MaxDesiredVelocity;
        public float MaxSteeringForce;
        public PlayUnit Pu;

        public void OnObjectSpawn()
        {
            Debug.Log("in on obj spawn");

            Pu = gameObject.GetComponent<PlayUnit>();

            if (Pu.IsPlayerUnit == true)
            {
                Debug.Log("isplayunit");
                MaxSteeringForce = sd.MaxSteeringForce + UpgradeMenu.instance.MaxSteeringForce;
                MaxSpeed = sd.MaxSpeed + UpgradeMenu.instance.MaxSpeed;
            }
            else
            {
                Debug.Log("isnietplayunit");
                MaxSteeringForce = sd.MaxSteeringForce;
                MaxSpeed = sd.MaxSpeed;
            }
        }


        private void Start()
        {
            Position = transform.position;
        }

        private void Awake()
        {
            sd = Instantiate(privatesd);
        }


        private void FixedUpdate()
        {
            //Steering general: calculate steering force
            Steerings = Vector3.zero;
            foreach (Ibehavior behavior in Behaviors)
            {
                Steerings += behavior.CalculateSteeringForce(Time.fixedDeltaTime, new BehaviorContext(Position, Velocity, sd));
            }

            Steerings.y = 0.0f;
            //Steering general: clamp steering force to max steering force and apply mass
            Steerings = Vector3.ClampMagnitude(Steerings, MaxSteeringForce);
            Steerings /= sd.Mass;

            //steering general: updat velocity with steering force, and update pos

            Velocity = Vector3.ClampMagnitude(Velocity + Steerings, MaxSpeed);
            Position += Velocity * Time.fixedDeltaTime;

            // update object with new pos
            transform.position = Position;
            transform.LookAt(Position + Time.fixedDeltaTime * Velocity);

        }

        private void OnDrawGizmos()
        {

            DrawRay(transform.position, Velocity, Color.red);

            DrawLabel(transform.position, Label, Color.red);

            //UnityEditor.Handles.color = Color.cyan;
            //UnityEditor.Handles.DrawSolidDisc(PositionTarget, Vector3.up, 0.25f);

            foreach (Ibehavior behavior in Behaviors)
            {
                behavior.OndrawGizmos(new BehaviorContext(Position, Velocity, sd));
            }
        }

        public void SetBehaviors(BehaviorList _behaviors, string _label = "")
        {
            Label = _label;
            Behaviors = _behaviors;

            foreach (Ibehavior behavior in Behaviors)
            {
                behavior.Starts(new BehaviorContext(Position, Velocity, sd));
            }
        }
    }
}

