using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.red;

        UnitCommand.Instance.UnitsSelected++;
    }

    private void OnDestroy()
    {
        UnitCommand.Instance.UnitsSelected--;

        GetComponent<Renderer>().material.color = Color.white;
    }
}
