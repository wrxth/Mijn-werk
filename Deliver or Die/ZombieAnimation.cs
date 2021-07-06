using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    [SerializeField] private float m_Framespeed = 10f;
    private float m_Timer;
    private int m_SpriteFrame;    

    private SpriteRenderer m_Spriterend;
    [SerializeField] private Sprite[] m_Run;
    void Start()
    {
        m_Spriterend = GetComponent<SpriteRenderer>();
    }

    void Update()
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
}
