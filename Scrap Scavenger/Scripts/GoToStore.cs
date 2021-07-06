using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToStore : MonoBehaviour
{
    [SerializeField] private LayerMask Stores;
    [SerializeField] private Collider[] store;

    [SerializeField] private GameObject StoreUI;

    private Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Search for nearby stores
        store = Physics.OverlapSphere(gameObject.transform.position, 20, Stores); 
        if (store.Length > 0)
        {
            StoreVisited sv = store[0].gameObject.GetComponent<StoreVisited>();

            // Is in position to open store menu
            if (Vector3.Distance(transform.position, sv.LocationPoint.transform.position) < 1 && sv.Visited == false)
            {
                PlayerState.Instance.Cs = PlayerState.CurrentState.AT_STORE;
            }
            else if (sv.Visited == false)
            {
                // Stop asteroid generation and start moving towards store
                AsteroidGen.Instance.stopGen = true;
                PlayerState.Instance.Cs = PlayerState.CurrentState.GOING_TO_STORE;
            }
            else if (sv.Visited == true)
            {
                PlayerState.Instance.Cs = PlayerState.CurrentState.MOVING;
            }

            if (PlayerState.Instance.Cs == PlayerState.CurrentState.GOING_TO_STORE)
            {
                // Turn off collision
                rb.isKinematic = true;

                // Get Direction
                Vector3 dir = new Vector3(transform.position.x - sv.LocationPoint.transform.position.x, transform.position.y - sv.LocationPoint.transform.position.y, transform.position.z - sv.LocationPoint.transform.position.z).normalized;
                
                // Move towards store
                transform.position += -dir * 5 * Time.deltaTime;
            }
            else if (PlayerState.Instance.Cs == PlayerState.CurrentState.AT_STORE)
            {
                // Activate Ui
                sv.Camera.SetActive(true);
                StoreUI.SetActive(true);
            }
        }
    }


    public void LeaveStore()
    {
        StoreVisited sv = store[0].gameObject.GetComponent<StoreVisited>();

        AsteroidGen.Instance.stopGen = false;
        rb.isKinematic = false;
        sv.Visited = true;
        sv.Camera.SetActive(false);
        StoreUI.SetActive(false);
    }
}
