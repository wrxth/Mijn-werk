using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController CharControl;
    [SerializeField] private float MoveSpeed = 10f;
    [SerializeField] private float TurnSmoothTime = 0.1f;
    [SerializeField] private float SmoothVelocity;
    [SerializeField] private Transform Cam;

    [SerializeField] private Animator Ani;
    private bool Moving;
    [SerializeField] private float gravity = 9.81f;
    private bool m_IsGrounded;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private LayerMask GroundMask;


    [SerializeField] private Vector3 gravMove;
    void Start()
    {
        CharControl = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        m_IsGrounded = Physics.CheckSphere(m_GroundCheck.position, 1, GroundMask);

        
        if (m_IsGrounded && direction.y < 0)
        {
            gravMove.y = -2;
        }

        if (!m_IsGrounded)
        {
            gravMove.y += -gravity * Time.deltaTime;
            CharControl.Move(gravMove * Time.deltaTime);
        }
        //direction.y += -gravity * Time.deltaTime;
        if (Ani.GetCurrentAnimatorStateInfo(0).IsName("arthur_idle_01") || Ani.GetCurrentAnimatorStateInfo(0).IsName("arthur_walk_01"))
        {
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref SmoothVelocity, TurnSmoothTime);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                transform.rotation = Quaternion.Euler(0, angle, 0);

                CharControl.Move(moveDir.normalized * MoveSpeed * Time.deltaTime);

                Debug.Log("mag is grott");
            }

        }

        if (horizontal != 0 || vertical != 0)
        {
            Moving = true;
        }
        else
        {
            Moving = false;
        }

        Ani.SetBool("moving", Moving);
    }
}
