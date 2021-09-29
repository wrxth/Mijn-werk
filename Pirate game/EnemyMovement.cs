using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private NavMeshAgent NavMesh;

    [SerializeField] private Transform[] PatrolPoints;
    [SerializeField] private Transform[] PlayerPoints;

    [SerializeField] private Transform Player;

    [SerializeField] private EnemyData ed;

    [SerializeField] private bool AttackLoopHasStarted;

    [SerializeField] float DistanceLeft, DistanceRight;


    public enum ShipStatus
    {
        PATROLING,
        FIGHTING
    }


    private ShipStatus ss;
    void Start()
    {
        StartCoroutine(ChooseNewDestination());
    }

    
    void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        

        if (distance <= ed.MaxDistanceFromPlayer)
        {
            ss = ShipStatus.FIGHTING;

            

            if (AttackLoopHasStarted == false)
            {
                NavMesh.SetDestination(PlayerPoints[0].position);
                AttackLoopHasStarted = true;
            }

            DistanceLeft = Vector3.Distance(PlayerPoints[0].position, transform.position);
            DistanceRight = Vector3.Distance(PlayerPoints[1].position, transform.position);
            if (DistanceLeft <= 5)
            {
                
                NavMesh.SetDestination(PlayerPoints[1].position);
            }
            else if (DistanceRight <= 5)
            {
                

                NavMesh.SetDestination(PlayerPoints[0].position);
            }
        }
        else
        {
            //ss = ShipStatus.PATROLING;
        }
    }

    private IEnumerator ChooseNewDestination()
    {
        yield return new WaitForSeconds(10f);
        if (ss == ShipStatus.PATROLING)
        {
            FindNewPoint();
        }
        
    }

    private void FindNewPoint()
    {
        NavMesh.SetDestination(PatrolPoints[Random.Range(0,PatrolPoints.Length)].position);

        StartCoroutine(ChooseNewDestination());
    }
}
