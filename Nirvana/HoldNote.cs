using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNote : MonoBehaviour , Iinteract
{
    public enum NoteState
    {
        NOT_BEING_HELD,
        BEING_HELD
    }

    [SerializeField] private NoteState NS;
    [SerializeField] private GameObject ReturnPoint;
    [SerializeField] private GameObject Holdpoint;
    void Start()
    {
        Holdpoint = GameObject.Find("Holdpoint");
        ReturnPoint = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (NS == NoteState.NOT_BEING_HELD)
        {
            gameObject.transform.position = ReturnPoint.transform.position;
            gameObject.transform.rotation = ReturnPoint.transform.rotation;
        }
        else
        {
            gameObject.transform.position = Holdpoint.transform.position;
            gameObject.transform.rotation = Holdpoint.transform.rotation;
        }
    }

    public void Interact()
    {
        Debug.Log("click on note");
        if (NS == NoteState.NOT_BEING_HELD)
        {
            NS = NoteState.BEING_HELD;
        }
        else
        {
            NS = NoteState.NOT_BEING_HELD;
        }
    }
}
