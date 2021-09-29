using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapplingHook : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    private Vector3 GrapplePoint;
    [SerializeField] private LayerMask WhatIsGrappleAble;
    [SerializeField] private Transform Gunpos, Camera, player;
    [SerializeField] private float MaxDistance;
    private SpringJoint Joint;

    [SerializeField] private float JointSpring, JointDamper, JointMassScale;

    private void Update()
    {
        DrawRope();
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }
    private void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.position,Camera.forward,out hit, MaxDistance,WhatIsGrappleAble))
        {
            GrapplePoint = hit.point;
            Joint = player.gameObject.AddComponent<SpringJoint>();
            Joint.autoConfigureConnectedAnchor = false;
            Joint.connectedAnchor = GrapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, GrapplePoint);

            //Distance the grapple can go
            Joint.maxDistance = distanceFromPoint * 0.8f;
            Joint.minDistance = distanceFromPoint * 0.25f;

            Joint.spring = JointSpring;
            Joint.damper = JointDamper;
            Joint.massScale = JointMassScale;

            lr.positionCount = 2;
        }
    }
    private void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(Joint);
    }

    private void DrawRope()
    {
        if (Joint == null)
        {
            return;
        }
        lr.SetPosition(0, Gunpos.position);
        lr.SetPosition(1, GrapplePoint);
    }

    public bool IsGrappling()
    {
        return Joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return GrapplePoint;
    }
}
