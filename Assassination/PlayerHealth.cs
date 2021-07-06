using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float m_MaxHealth;
    [SerializeField] private float m_CurrentHealth;
    [SerializeField] private float m_HealthRegen = 1f;

    [SerializeField] private Slider m_HealthBar;

    private bool CanRegen;
    // Start is called before the first frame update
    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
        m_HealthBar.maxValue = m_MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanRegen == true)
        {
            m_CurrentHealth += m_HealthRegen * Time.deltaTime;
        }
        m_CurrentHealth = Mathf.Clamp(m_CurrentHealth, 0, m_MaxHealth);
        m_HealthBar.value = m_CurrentHealth;
    }
    public void TakeDamage(float _damage)
    {
        CanRegen = false;
        m_CurrentHealth -= _damage;

        if (m_CurrentHealth < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }
        Invoke("Regen", 3f);
    }

    private void Regen()
    {
        CanRegen = true;
    }
}
