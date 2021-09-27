using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsMirror))]
public class LightBeam : MonoBehaviour
{
    [SerializeField] private LineRenderer LightVis;
    [SerializeField] private bool Origin;
    [SerializeField] private bool TurnedOn;
    [SerializeField] private LightBeam Lb;

    private string Progenitor;

    [SerializeField] private Vector3 OriginPoint;

    private List<GameObject> BeingHitBy = new List<GameObject>();
    private void Start()
    {
        OriginPoint = gameObject.transform.position;
    }
    void Update()
    {
        // Check if the beam can shoot
        if (Origin || TurnedOn)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                IsMirror im = hit.collider.GetComponent<IsMirror>();
                Target t = hit.collider.GetComponent<Target>();

                // Check if target
                if (t != null)
                {
                    t.MirrorsAlligned();
                }

                // Hitting another mirror logic
                if (im != null)
                {
                    Lb = hit.collider.GetComponent<LightBeam>();
                    Lb.HitByRay(hit.point);
                }
                else if (Lb != null)
                {
                    Lb.NotBeingHit();
                }
            }
           
            // Create the light beam
            LightVis.positionCount = 2;
            LightVis.SetPosition(0, OriginPoint);
            LightVis.SetPosition(1, hit.point);
        }
        else if (Lb != null)
        {
            Lb.NotBeingHit();
        }
    }
    
    public void HitByRay(Vector3 _hitLocation)
    {
        TurnedOn = true;
        OriginPoint = _hitLocation;
    }
    public void NotBeingHit()
    {
        TurnedOn = false;
        LightVis.positionCount = 0;
    }
}
