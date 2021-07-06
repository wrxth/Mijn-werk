using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    [SerializeField] private float FlySpeed;
   
    void Update()
    {
        transform.position += new Vector3(0, 0, FlySpeed) * Time.deltaTime;
    }
}
