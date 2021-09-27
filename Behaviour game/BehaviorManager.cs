using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorManager : MonoBehaviour
{

    public static BehaviorManager Instance;

    public void Awake()
    {
        Instance = this;
    }
    public event Action OnAggresive;
    public event Action OnDefensive;


    public void OnAggresiveTrigger()
    {
        if (OnAggresive != null)
        {
            OnAggresive();
        }
    }
    public void OnDefensiveTrigger()
    {
        if (OnDefensive != null)
        {
            OnDefensive();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
