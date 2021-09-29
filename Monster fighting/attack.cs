using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    [SerializeField] private Animator Ani;
    private bool Attacking;
    [SerializeField] private ParticleSystem Lazer;
    [SerializeField] private float LazerSpeed;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attacking = true;
        }
        else
        {
            Attacking = false;
        }
        Ani.SetBool("attack", Attacking);
        bool lazer;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            lazer = true;
            Lazer.Play();
        }
        else
        {
            lazer = false;
        }

        Ani.SetBool("lazer", lazer);

        
    }
}
