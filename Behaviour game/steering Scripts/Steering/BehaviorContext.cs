using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering
{


    public class BehaviorContext : MonoBehaviour
    {
        public Vector3 Position;
        public Vector3 Velocity;
        public SteeringData sd;

        public BehaviorContext(Vector3 _pos, Vector3 _velocity, SteeringData _data)
        {
            Position = _pos;
            Velocity = _velocity;
            sd = _data;
        }
    } 
}
