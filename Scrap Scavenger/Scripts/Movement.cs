using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    [Header("movement vars")]
    [SerializeField] private float SpeedGain;
    [SerializeField] private float SpeedInterval;
    [SerializeField] private float DirectionalSpeed;
    [SerializeField] private float BaseFlySpeed;
    [SerializeField] private float BuffSpeedTime;
    [SerializeField] private float RollCooldown = 5;
    
    public float DirectionalSpeedBuff;



    [Header("barrel roll")]
    public bool RolAcquired;
    private bool Rolling;
    private bool RollingOnCooldown;

    [SerializeField] private float DashPower;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement

        // Smoothed input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        // Raw input
        float horizontalRaw = Input.GetAxisRaw("Horizontal");
        float verticalRaw = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal * (LevelManger.Instance.currentActive.DirectionalSpeed + DirectionalSpeedBuff), vertical * (LevelManger.Instance.currentActive.DirectionalSpeed + DirectionalSpeedBuff), LevelManger.Instance.currentActive.BaseFlySpeed) * Time.deltaTime;
        Vector3 restriction = transform.position + movement;

        // Disable movespeed buff
        if (DirectionalSpeedBuff > 0)
        {
            Invoke("DisableSpeed", BuffSpeedTime);
        }

        if (PlayerState.Instance.Cs == PlayerState.CurrentState.MOVING)
        {
            // Apply movement
            transform.position = restriction;

            if (Input.GetButtonDown("Jump") && RolAcquired == true && RollingOnCooldown == false)
            {
                // Dash in Direction
                rb.AddForce(movement.normalized * DashPower, ForceMode.Impulse);
                StartCoroutine(StartRoll(0.9f * verticalRaw, 0.9f * horizontalRaw, movement));
            }
            else if (Rolling == false)
            {
                // Tilt ship
                transform.rotation = Quaternion.Euler(15 * -vertical, 0, 15 * -horizontal);
            }
        }
    }

    // Start rolling
    private IEnumerator StartRoll(float _vertical, float _horizontal, Vector3 _movement)
    {
        Rolling = true;
        RollingOnCooldown = true;
        SoundEffect.Instance.playSound(SoundEffect.Instance.doge);

        // Start rolling in increments Change waitforseconds to change smoothing. Done like this because due to other mehods breaking movement.
        for (int i = 0; i < 5; i++)
        {
            transform.position += new Vector3(_movement.x,_movement.y,0) * 20;
            transform.rotation = Quaternion.Euler(360 * (i *_vertical ), 0, 360 * (i * _horizontal));
            yield return new WaitForSeconds(0.04f);
        }
        Rolling = false;
        yield return new WaitForSeconds(RollCooldown);
        RollingOnCooldown = false;

    }
    private void DisableSpeed()
    {
        DirectionalSpeedBuff = 0;
    }
}
