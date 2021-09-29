using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;


    public float Amplitude, Length, Speed, Offset;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
    }


    private void Update()
    {
        Offset += Time.deltaTime * Speed;
    }

    public float GetWaveHeight(float _x)
    {
        return Amplitude * Mathf.Sin(_x/Length + Offset);
    }
}
