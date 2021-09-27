using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering  
{

    using ColliderList = List<Collider>;
    using HideList = List<Vector3>;
    public class Hide : Behavior
    {
        private GameObject Target;


        private ColliderList Colliders;
        private HideList HidePlaces;
        private Vector3 HidingPlace;
        public Hide(GameObject target)
        {
            this.Target = target;
        }

        public override void Starts(BehaviorContext _context)
        {
            base.Starts(_context);
            Colliders = FindCollidersWithLayer(_context.sd.HideLayer);
        }

        static public List<Collider> FindCollidersWithLayer(string _layerName)
        {
            int colliderLayer = LayerMask.NameToLayer(_layerName);

            Collider[] allColliders = GameObject.FindObjectsOfType(typeof(Collider)) as Collider[];
            List<Collider> colliders = new List<Collider>();
            foreach (Collider gameObject in allColliders)
            {
                if (gameObject.gameObject.layer == colliderLayer)
                {
                    colliders.Add(gameObject);
                }
            }

            return colliders;
        }

        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            PositionTarget = CalculateHidingPlace(_context, Target.transform.position);
            

            VelocityDesired = (PositionTarget - _context.Position).normalized * _context.sd.MaxDesiredVelocity;
            return VelocityDesired - _context.Velocity;
        }

        public Vector3 CalculateHidingPlace(BehaviorContext _context, Vector3 _enemyPosition)
        {
            float ClosestDistanceSqr = float.MaxValue;
            HidingPlace = _context.Position;
            HidePlaces = new HideList();

            for (int i = 0; i < Colliders.Count; i++)
            {
                Vector3 hidingPlace = CalcHidePlace(_context, Colliders[i],_enemyPosition);    // moet hier iets

                HidePlaces.Add(hidingPlace);

                float distanceToHidingPlaceSqr = (_context.Position - hidingPlace).sqrMagnitude;
                if (distanceToHidingPlaceSqr < ClosestDistanceSqr)
                {
                    ClosestDistanceSqr = distanceToHidingPlaceSqr;
                    HidingPlace = hidingPlace;
                }
            }

            return HidingPlace;
        }

        private Vector3 CalcHidePlace(BehaviorContext _context, Collider _collider, Vector3 _enemyPosition)
        {
            Vector3 obstacleDir = (_collider.transform.position - -_enemyPosition).normalized;
            Vector3 pointOtherSide = _collider.transform.position + obstacleDir;
            Vector3 hidingPlace = _collider.ClosestPointOnBounds(pointOtherSide) + (obstacleDir * _context.sd.HideOffSet);

            return hidingPlace;
        }

        public override void OndrawGizmos(BehaviorContext _context)
        {
            base.OndrawGizmos(_context);

            foreach (Vector3 hidingPlace in HidePlaces)
            {
                //UnityEditor.Handles.color = Color.cyan;
                //UnityEditor.Handles.DrawSolidDisc(hidingPlace, Vector3.up, 0.25f);
            }
            //UnityEditor.Handles.color = Color.cyan;
            //UnityEditor.Handles.DrawSolidDisc(HidingPlace, Vector3.up, 0.25f);
        }
    } 
}
