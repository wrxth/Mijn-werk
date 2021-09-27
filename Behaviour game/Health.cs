using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour  ,Ipooled
{
    [SerializeField] private bool IsTower;
    [SerializeField] private GameObject MenuItems;
    public float MaxHealth;
    public float CurrentHealth;


    [SerializeField] private Slider HealthSlider;

    [SerializeField] private int EnemyDeathReward, ScoreToGive;

    public float LowHealthPoint;
    [SerializeField] private GameObject PlayerCamera;

    [SerializeField] private bool IsPlayerUnit;

    public void OnObjectSpawn()
    {
        PlayerCamera = GameObject.Find("CameraParent");

        float maxHealth = MaxHealth + UpgradeData.Instance.MaxhealthBuff;

        CurrentHealth = maxHealth;

        if (IsTower == true)
        {
            MenuItems.SetActive(false);
        }
    }

    private void Start()
    {
        PlayerCamera = GameObject.Find("CameraParent");

        CurrentHealth = MaxHealth;

        if (IsTower == true)
        {
            MenuItems.SetActive(false);
        }
    }


    void Update()
    {
        HealthSlider.gameObject.transform.LookAt(PlayerCamera.transform.position);
        HealthSlider.value = CalculateHealth();

        if (CurrentHealth <= 0)
        {
            if (IsTower == true)
            {
                Time.timeScale = 0f;
                MenuItems.SetActive(true);
            }
            //Instantiate(explosion,transform.position,transform.rotation);
            gameObject.SetActive(false);
        }
    }


    public void TakeDamage(int _damage)
    {
        CurrentHealth -= _damage;
    }

    public float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    } 
    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public void FullHeal()
    {
        CurrentHealth = MaxHealth;
    }

    public void IncreaseHealth(int _addedhealth)
    {
        Debug.Log("heal");
        if (CurrentHealth < MaxHealth)
        {
            CurrentHealth += _addedhealth;
        }
    }
}
