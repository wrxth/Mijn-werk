using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public enum CurrentState
    {
        WAITING,
        YOU_WIN,
        YOU_LOSE
    }
    public static GameState Instance;

    public CurrentState Cc;

    [SerializeField] private GameObject m_LoseScreen;
    [SerializeField] private GameObject m_WinScreen;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Cc == CurrentState.YOU_WIN)
        {
            Invoke("Won", 2f);
        }
        else if(Cc == CurrentState.YOU_LOSE)
        {
            Invoke("Lost", 2f);
        }
    }

    public void YouLose()
    {
        Cc = CurrentState.YOU_LOSE;
    }
    public void YouWin()
    {
        Cc = CurrentState.YOU_WIN;
    }

    private void Lost()
    {
        Time.timeScale = 0f;
        m_LoseScreen.SetActive(true);
    }
    private void Won()
    {
        Time.timeScale = 0f;
        m_WinScreen.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } 
    public void Quit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
