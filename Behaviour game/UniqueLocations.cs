using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueLocations : MonoBehaviour
{
    [SerializeField] private LayerMask Units;

    [SerializeField] private bool IsHealingTower;

    private float Timer;
    private float HealInterval;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] unitCollider = Physics.OverlapSphere(gameObject.transform.position, 10f, Units);

        if (unitCollider.Length != 0)
        {
            if (IsHealingTower == true)
            {

                Timer += Time.deltaTime;

                if (Timer > HealInterval)
                {
                    for (int i = 0; i < unitCollider.Length; i++)
                    {
                        Health h = unitCollider[i].GetComponent<Health>();

                        if (h != null)
                        {
                            h.IncreaseHealth(5);
                        }
                    }
                }

            }
            else
            {
                for (int i = 0; i < unitCollider.Length; i++)
                {
                    

                    steering.PlayUnit pu = unitCollider[i].GetComponent<steering.PlayUnit>();

                    if (pu != null)
                    {
                        pu.BuffDamage();
                    }
                }
            }
        }
        
    }
}
