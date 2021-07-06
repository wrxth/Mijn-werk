using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] m_Run;
    [SerializeField] private Sprite[] m_Jump;
    [SerializeField] private Sprite m_Idle;
    [SerializeField] private CharacterController2D Cc;
    [SerializeField] private PlayerMovement Pm;

    private SpriteRenderer m_Spriterend;
    private float m_Timer;
    private float m_Timer2;

    //float rotz;

    private float m_Framespeed = 10f;
    private float m_Framespeed2 = 10f;
    private int m_SpriteFrame;
    private int m_SpriteFrame2;


    void Start()
    {
        m_Spriterend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Cc.m_Grounded == true && Input.GetButton(Pm.m_Movename))
        {
            m_Timer += Time.deltaTime;

            if (m_Timer > (1f / m_Framespeed))
            {
                m_Spriterend.sprite = m_Run[m_SpriteFrame];
                m_SpriteFrame++;
                if (m_SpriteFrame == m_Run.Length)
                {
                    m_SpriteFrame = 0;
                }
                m_Timer = 0;
            }
        }
        else if (Cc.m_Grounded == false)
        {
            m_Timer2 += Time.deltaTime;
            if (m_Timer2 > (1f / m_Framespeed2))
            {
                m_Spriterend.sprite = m_Jump[m_SpriteFrame2];
                m_SpriteFrame2++;
                if (m_SpriteFrame2 == m_Jump.Length)
                {
                    m_SpriteFrame2 = 0;
                }
                m_Timer2 = 0;
            }
        }
        else
        {
            m_Spriterend.sprite = m_Idle;
        }
    }
}
