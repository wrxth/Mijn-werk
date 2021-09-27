using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour ,Iinteract
{
    public enum ButtonType
    {
        POWER_UP,
        POWER_DOWN
    }

    [SerializeField] private ButtonType Bt;

    [SerializeField] private PowerBox Pb;

    public void Interact()
    {
        if (Bt == ButtonType.POWER_UP)
        {
            Pb.PowerOutput++;
        }
        else if (Bt == ButtonType.POWER_DOWN)
        {
            Pb.PowerOutput--;
        }
    }
}
