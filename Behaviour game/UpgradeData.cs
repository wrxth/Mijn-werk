using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeData : MonoBehaviour
{
    public static UpgradeData Instance;
    private void Awake()
    {
        Instance = this;
    }

    public float AtkInterval;
    public int DamageBuff;
    public float AtkRange;
    public float MaxhealthBuff;
}
