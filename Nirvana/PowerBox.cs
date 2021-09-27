using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerBox : MonoBehaviour
{
    public int PowerOutput;
    [SerializeField] private int RequiredPower;

    [SerializeField] private TMP_Text PowerVis;

    private void Update()
    {
        PowerVis.text = PowerOutput.ToString() + "A";
    }

    public bool CorrectPower()
    {
        if (PowerOutput == RequiredPower)
        {
            return true;
        }
        return false;
    }
}
