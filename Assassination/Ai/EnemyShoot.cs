using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [SerializeField] private GameObject m_Player;

    private enemyState Es;

    [SerializeField] private float m_TimeBetweenShots;
    private float m_ShotTimer;

    [SerializeField] private int m_HitChance;
    [SerializeField] private float m_AttackRange;
    [SerializeField] private float m_Damage = 10;

    [SerializeField] private bool Attackable;
    [SerializeField] private ParticleSystem m_MuzzleFlash;
    [SerializeField] private AudioSource ShotSound;
    void Start()
    {
        m_Player = GameObject.Find("player");
        Es = GetComponent<enemyState>();
    }

    void Update()
    {
        if (Es.dead)
        {
            this.enabled = false;
        }
        Vector3 dir = m_Player.transform.position - transform.position;

        //Debug.DrawLine(transform.position, dir, Color.red);
        Debug.DrawRay(transform.position, dir, Color.red);
        RaycastHit hit;
       
        if (Es.hunting == true)
        {
            m_ShotTimer += Time.deltaTime;
            if (m_ShotTimer > m_TimeBetweenShots && Vector3.Distance(transform.position, m_Player.transform.position) < m_AttackRange)
            {
                if (Physics.Raycast(transform.position, dir, out hit, m_AttackRange))
                {
                    int Hitchance;
                    Hitchance = Random.Range(1, m_HitChance + 1);

                    PlayerController pl = hit.collider.GetComponent<PlayerController>();
                    //Debug.Log("de hit: " + hit.collider.name + "player: " + m_Player.name);
                    if (pl != null)
                    {
                        //Debug.Log("ping1");
                        Attackable = true;
                        if (Hitchance == 1)
                        {
                            m_Player.GetComponent<PlayerHealth>().TakeDamage(m_Damage);
                            Debug.Log("ping");
                        }
                    }
                }
                ShotSound.Play();
                m_MuzzleFlash.Play();
           

               
                m_ShotTimer = 0;
            }
        }
    }
}
