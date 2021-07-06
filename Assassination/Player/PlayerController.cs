using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("references")]
    [SerializeField] private CharacterController m_Control;
    private Vector3 m_Velocity;

    [SerializeField] private Transform Cam;

    [Header("movement variables")]
    [SerializeField] private float m_Movespeed;
    [SerializeField] private float m_Gravity = 9.81f;
    [SerializeField] private float m_JumpHeight;
    [SerializeField] private float m_BunnyHopSpeedMulti = 2f;

    private float m_BaseMoveSpeed;

    [Header("stamina variables")]
    [SerializeField] private bool m_IsRunning;
    [SerializeField] private float m_Stamina;
    [SerializeField] private float m_MaxStamina;
    [SerializeField] private float m_StaminaDrain = 1f;
    [SerializeField] private float m_StaminaGain = 1f;
    [SerializeField] private Slider m_StaminaBar;

    [Header("Groundcheck")]
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private float m_GroundDistance;
    [SerializeField] private LayerMask m_GroundMask;
    [SerializeField] private bool m_IsGrounded;


    [Header("Crouch")]
    [SerializeField] private Vector3 CurrentHeight;


    [Header("Wall Climb vars")]
    [SerializeField] private LayerMask m_ClimbAbleWallmask;
    [SerializeField] private float CLimbSpeed;

    [SerializeField] private bool m_WasOnWall;
    [SerializeField] private float m_PushOffOffSet = 5f;
    [SerializeField] private Transform HeadCheck;

    [Header("sneak vars")]
    [SerializeField] private float m_SoundRange;
    [SerializeField] private Collider[] m_EnemiesInRange;
    [SerializeField] private LayerMask m_EnemyLayer;

    [SerializeField] private float m_BaseSoundRange;
    [Header("health vars")]
    
    [SerializeField] private AudioSource WalkSound;
    [SerializeField] private AudioSource RunSound;

    private void Start()
    {
        m_BaseMoveSpeed = m_Movespeed;
        m_BaseSoundRange = m_SoundRange;

        m_StaminaBar.maxValue = m_MaxStamina;
        CurrentHeight = new Vector3(1, 1, 1);

       
        m_Stamina = m_MaxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        m_StaminaBar.value = m_Stamina;
        m_IsGrounded = Physics.CheckSphere(m_GroundCheck.position, m_GroundDistance, m_GroundMask);

        if (m_IsGrounded && m_Velocity.y < 0)
        {
            m_Velocity.y = -2;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            RunSound.Stop();
            WalkSound.Play();
            //Debug.Log("Ping;");
        }
        else if(Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
        {
            RunSound.Stop();
            WalkSound.Stop();
           //Debug.Log("Ping2");

        }

        //m_Control.Move(direction * m_Movespeed * Time.deltaTime);

        // jump
        if (Input.GetButtonDown("Jump") && m_IsGrounded)
        {
            m_Velocity.y = Mathf.Sqrt(m_JumpHeight * -2f * m_Gravity);
            Debug.Log("133");
        }

        // Wall Climb
        RaycastHit hit;
        if (!Physics.Raycast(HeadCheck.position, transform.forward + new Vector3(0, 1, 0), 1f, m_ClimbAbleWallmask) && m_WasOnWall)
        {
            vertical = 0;
            m_Velocity.y = Mathf.Sqrt(-m_PushOffOffSet * -2f * m_Gravity);
            Invoke("DisableOffset", 0.3f);

        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, 1f, m_ClimbAbleWallmask) && !m_IsGrounded && Input.GetKey(KeyCode.W) && m_Stamina > (m_MaxStamina * 0.1))
        {
            m_WasOnWall = true;

            if (Input.GetKey(KeyCode.LeftControl))
            {
                vertical = 0;
                horizontal = 0;
            }
            else
            {
                m_Velocity.y = CLimbSpeed;
            }

        }
        else
        {
            m_Velocity.y += m_Gravity * Time.deltaTime;
        }


        Vector3 direction = transform.right * horizontal + transform.forward * vertical;

        //Jump
        if (Input.GetButtonDown("Jump") && m_IsGrounded)
        {
            m_Velocity.y = Mathf.Sqrt(m_JumpHeight * -2f * m_Gravity);
        }


        // Applying movement
        if (!m_IsGrounded)
        {
            m_Control.Move(direction * m_Movespeed * m_BunnyHopSpeedMulti * Time.deltaTime);
        }
        else
        {
            m_Control.Move(direction * m_Movespeed * Time.deltaTime);
        }
        
        m_Control.Move(m_Velocity * Time.deltaTime);


        // crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCrouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StopCrouch();
        }
        
        transform.localScale = CurrentHeight;

        // enemie detection
        m_EnemiesInRange = Physics.OverlapSphere(transform.position, m_SoundRange, m_EnemyLayer);

        if (m_EnemiesInRange.Length != 0)
        {
            for (int i = 0; i < m_EnemiesInRange.Length; i++)
            {
                m_EnemiesInRange[i].GetComponent<enemyState>().hunting = true;
            }
        }

        // run 
        if (m_Stamina > (m_MaxStamina * 0.1) && m_IsGrounded)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartRun();
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                StopRun();
            }
        }
        else if(m_Stamina <= 1 && m_IsRunning)
        {
            StopRun();
        }

     
        // stamina

        m_Stamina = Mathf.Clamp(m_Stamina, 0, m_MaxStamina);

        if (m_IsRunning || m_WasOnWall)
        {
            m_Stamina -= m_StaminaDrain * Time.deltaTime;
        }
        else
        {
            m_Stamina += m_StaminaGain * Time.deltaTime;
        }
        
    }


    private void DisableOffset()
    {
        m_WasOnWall = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, m_SoundRange);
    }
    // Crouch
    private void StartCrouch()
    {
        CurrentHeight.y = 0.5f;
        m_SoundRange = m_BaseSoundRange / 4;
        m_Movespeed /= 2;
    } 
    private void StopCrouch()
    {
        CurrentHeight.y = 1f;
        m_SoundRange = m_BaseSoundRange;
        m_Movespeed *= 2;
    } 
    // Run
    private void StartRun()
    {
        RunSound.Play();
        WalkSound.Stop();
        m_IsRunning = true;
        m_Movespeed = m_BaseMoveSpeed * 2;
        m_SoundRange = m_BaseSoundRange * 2;
    } 
    private void StopRun()
    {
        RunSound.Stop();
        WalkSound.Play();
        m_IsRunning = false;
        m_Movespeed = m_BaseMoveSpeed;
        m_SoundRange = m_BaseSoundRange;
    }
}
