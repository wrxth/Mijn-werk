using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController2D))]
public class ZombieControl : MonoBehaviour
{
    [SerializeField] private CharacterController2D CC;
    [SerializeField] private float m_MaxMoveSpeed;
    [SerializeField] private float m_MinMoveSpeed;
    private float m_MoveSpeed;
    void Start()
    {
        m_MoveSpeed = Random.Range(m_MinMoveSpeed,m_MaxMoveSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameState.Instance.Cc != GameState.CurrentState.YOU_LOSE || GameState.Instance.Cc != GameState.CurrentState.YOU_WIN)
        {
            CC.Move(m_MoveSpeed * Time.fixedDeltaTime, false, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameState.Instance.YouLose();
            Debug.Log("EndGame");
            SceneManager.LoadScene("Lose_scene");
        }
    }
}
