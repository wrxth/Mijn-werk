using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;


    public float Amplitude, Length, Speed, Offset;
    
    // singleton
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
        // verander de wave height
        Offset += Time.deltaTime * Speed;
    }
    
    // Return de wave height
    public float GetWaveHeight(float _x)
    {
        return Amplitude * Mathf.Sin(_x/Length + Offset);
    }
}
