using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_Jumpforce = 400f;
    [Range(0, 1f)] [SerializeField] private float m_Crouchspeed;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing;
    [SerializeField] private Rigidbody2D m_Rigid;
    [SerializeField] private int m_Playermovementspeed;

    [SerializeField] private bool m_Aircontrol = false;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_CeilingCheck;
    [SerializeField] private Collider2D m_CrouchDisableCollider;
    const float m_GroundedRadius = .2f;
    public bool m_Grounded;

    const float m_CeilingRadius = .2f;


    private bool m_FacingRight = true;
    private Vector3 m_Speed = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;


    [System.Serializable]
    public class BoolEvent : UnityEvent<bool>
    {

    }

    public BoolEvent OnCrouchEvent;

    public bool m_wascrouching = false;


    private void Awake()
    {
        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
        if (OnCrouchEvent == null)
        {
            OnCrouchEvent = new BoolEvent();
        }
    }
    private void FixedUpdate()
    {
        bool wasgrounded = m_Grounded;
        m_Grounded = false;


        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, m_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasgrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }

    public void Move(float move, bool crouch, bool jump)
    {
        if (!crouch)
        {
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, m_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
            //float rotz;

            //Quaternion rotation_enemy;

            //transform.rotation.z 
        }

        if (m_Grounded || m_Aircontrol)
        {
            if (crouch)
            {
                if (!m_wascrouching)
                {
                    m_wascrouching = true;
                    OnCrouchEvent.Invoke(true);
                }
            }
            else
            {
                if (m_CrouchDisableCollider != null)
                {
                    m_CrouchDisableCollider.enabled = true;
                }

                if (m_wascrouching)
                {
                    m_wascrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigid.velocity.y);

            m_Rigid.velocity = Vector3.SmoothDamp(m_Rigid.velocity, targetVelocity, ref m_Speed, m_MovementSmoothing);

            if (move > 0 && !m_FacingRight)
            {
                //Flip();
            }

            if (move < 0 && m_FacingRight)
            {
                //Flip();
            }

        }
        if (m_Grounded && jump)
        {
            m_Grounded = false;
            m_Rigid.AddForce(new Vector2(0f, m_Jumpforce));
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 thescale = transform.localScale;
        thescale.x *= -1;
        transform.localScale = thescale;
    }

}
