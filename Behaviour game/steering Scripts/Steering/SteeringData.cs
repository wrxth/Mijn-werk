using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering
{



    [CreateAssetMenu(fileName = "steering settings", menuName = "steering/steering settings", order = 1)]
    public class SteeringData : ScriptableObject
    {
        [Header("steering settings")]
        public float MaxSpeed = 3f;
        public float Mass = 70f;
        public float MaxSteeringForce = 3f;
        public float MaxDesiredVelocity = 3f;

        [Header("arrive")]
        public float ArriveDistance = 1f;

        [Header("Wander")]
        public float MinX, MaxX;
        public float MinZ, MaxZ;

        public float WanderingCircleDistance = 5f;
        public float WanderingCircleRadius = 5f;
        public float WanderingNoiseAngle = 5f;

        [Header("Pursuit and Evade")]
        public float LookAheadTime = 1f;

        [Header("Avoid obstacle")]
        public string[] AvoidLayer;
        public float AvoidDistance;
        public float AvoidForce; 
        [Header("Avoid obstacle")]
        public string HideLayer = "obstacle";
        public float HideOffSet;
        

    }
}
