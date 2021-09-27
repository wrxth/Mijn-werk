using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorSelector : MonoBehaviour
{
    public static BehaviorSelector Instance;
    private void Awake()
    {
        Instance = this;
    }

    public string CurrentBehavior = "agro";
  

    public void AggresiveOn()
    {
        CurrentBehavior = "agro";
    }
    public void DefensiveOn()
    {
        CurrentBehavior = "def";
    }
    public void LoyalOn()
    {
        CurrentBehavior = "loyal";
    }
    public void GuardAOn()
    {
        CurrentBehavior = "GuardA";
    }
    public void GuardBOn()
    {
        CurrentBehavior = "GuardB";
    }
    public void WanderOn()
    {
        CurrentBehavior = "wander";
    }
}
