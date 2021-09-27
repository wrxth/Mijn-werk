using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering
{
    public interface Ibehavior 
    {

        void Starts(BehaviorContext Context);

        Vector3 CalculateSteeringForce(float dt, BehaviorContext Context);

        void OndrawGizmos(BehaviorContext Context);
    } 
}
