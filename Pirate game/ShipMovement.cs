using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{

    public enum Sink
    {
        FLOATING,
        SINKING
    }
    public enum SailStatus
    {
        FULL_STOP,
        HALF_SAIL,
        FULL_SAIL
    }
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float BaseSpeed, BaseRotationSpeed;
    private float Speed, RotationSpeed;
    private float Direction;
    [SerializeField] private GameObject[] sails;

    [SerializeField] private float HalfSailMultiR, FullSailMultiR, FullSailMultiS;
    private SailStatus ss;

    public Sink S;
    void Update()
    {
        if (PlayerMode.Instance.pm == PlayerMode.PlayerModus.SHIP)
        {


            if (S == Sink.SINKING)
            {
                rb.AddForceAtPosition(Physics.gravity * 10, transform.position, ForceMode.Acceleration);

                Invoke("Sinking", 5);
            }
            if (Input.GetKeyDown(KeyCode.W) && ss <= SailStatus.FULL_SAIL)
            {
                ss++;
            }
            if (Input.GetKeyDown(KeyCode.S) && ss >= SailStatus.FULL_STOP)
            {
                ss--;
            }
            Direction = Input.GetAxis("Horizontal");

            switch (ss)
            {
                case SailStatus.FULL_STOP:
                    FullStop();
                    break;
                case SailStatus.HALF_SAIL:
                    HalfSail();
                    break;
                case SailStatus.FULL_SAIL:
                    FullSail();
                    break;


            }
            transform.Rotate(new Vector3(0f, 45 * Direction, 0f), 10 * Time.deltaTime * RotationSpeed);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * Speed * Time.deltaTime;
    }

    private void FullStop() 
    {
        Speed = 0;
        RotationSpeed = BaseRotationSpeed;
        sails[0].SetActive(true);
        sails[1].SetActive(false);
        sails[2].SetActive(false);
    }
    private void HalfSail() 
    {
        Speed = BaseSpeed;
        RotationSpeed = BaseRotationSpeed * HalfSailMultiR;
        sails[0].SetActive(false);
        sails[1].SetActive(true);
        sails[2].SetActive(false);
    }  
    private void FullSail() 
    {
        Speed = BaseSpeed * FullSailMultiS;
        RotationSpeed = BaseRotationSpeed * FullSailMultiR;
        sails[0].SetActive(false);
        sails[1].SetActive(false);
        sails[2].SetActive(true);
    }

    private void Sinking()
    {
        Win.Instance.Lose();
    }
  
}
