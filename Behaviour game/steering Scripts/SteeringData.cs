using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSteering
{


    [CreateAssetMenu(fileName = "steering settings", menuName = "steering/Simple steering settings", order = 2)]
    public class SteeringData : ScriptableObject
    {
        [Header("steering settings")]
        public float MaxSpeed = 3f;
        public float Mass = 70f;
        public float MaxSteeringForce = 3f;
        public float MaxDesiredVelocity = 3f;

        [Header("arrive")]
        public float ArriveDistance;

    }
}
