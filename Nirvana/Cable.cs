using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour, Iinteract
{
    public enum CableState
    {
        NOT_CONNECTED,
        CONNECTING,
        CONNECTED,
        WRONG_SEQUENCE
    }

    public CableState CS;

    [SerializeField] private LineRenderer CableVisual;

    [Tooltip("the connection point that this cable should be connected to")]
    [SerializeField] private GameObject CorrectObj;                        // the gameobject that should be connected to
    [SerializeField] private GameObject ConnectedObj;                                       // the gameobject that you are connected to


    [SerializeField] private GameObject Player;                                             // the player
    [Tooltip("the place where the player holds the cable, should be visible in the screen")]
    [SerializeField] private GameObject HoldPoint;                                          // the place where the player holds the cable
    [SerializeField] private GameObject StartPoint;

    private GameObject Pin;
    void Start()
    {
        Invoke("DelayedStart", 1f);
    }

    void Update()
    {
        if (CS == CableState.CONNECTING)
        {
            // Cable connecting code
            RaycastHit hit;
            if (Physics.Raycast(Player.transform.position, Player.transform.forward, out hit, Mathf.Infinity) && Input.GetKeyDown(KeyCode.E))
            {
                ConnectedObj = hit.collider.gameObject;

                // check if obj is a connection point
                IsConnectionPoint isp = hit.collider.GetComponent<IsConnectionPoint>();

                if (ConnectedObj.name == CorrectObj.name && isp != null)
                {
                    // the cable has been connected to the correct port
                    CS = CableState.CONNECTED;
                }
                else if (isp != null)
                {
                    // the cable has been connected to the wrong port
                    CS = CableState.WRONG_SEQUENCE;
                }
            }
            // visualize holding the cable 
            CableVisual.SetPosition(1, HoldPoint.transform.position);
        }

        if (CS == CableState.CONNECTED)
        {
            // the cable is connected
            Pin = ConnectedObj.transform.GetChild(0).gameObject;
            GameObject endPoint = ConnectedObj.transform.GetChild(1).gameObject;

            Pin.SetActive(true);
            CableVisual.SetPosition(1, endPoint.transform.position);
        }
        else if (CS == CableState.WRONG_SEQUENCE)
        {
            Pin = ConnectedObj.transform.GetChild(0).gameObject;
            GameObject endPoint = ConnectedObj.transform.GetChild(1).gameObject;

            Pin.SetActive(true);
            //Debug.Log("fail");
            CableVisual.SetPosition(1, endPoint.transform.position);
        }
    }

    public void Interact()
    {
        Debug.Log("has been interacted with");

        StartCoroutine(Interactablity());
    }

    private IEnumerator Interactablity()
    {
        // activate light renderer
        CableVisual.positionCount = 2;
        CableVisual.SetPosition(0, StartPoint.transform.position);

        CS = CableState.CONNECTING;
        yield return new WaitForSeconds(1f);
    }

    public void DelayedStart()
    {
        Player = GameObject.Find("Main Camera");
        HoldPoint = GameObject.Find("Holdpoint");
    }

    public void Reset()
    {
        Pin.SetActive(false);
        ConnectedObj = null;
        CableVisual.positionCount = 0;
        CS = CableState.NOT_CONNECTED;
    }
}
