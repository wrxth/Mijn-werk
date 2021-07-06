using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private GameObject Camera;
    [Header("camera offset")]
    [SerializeField] private float OffsettX;
    [SerializeField] private float OffsettY;
    [SerializeField] private float OffsettZ;
    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector3 newPos = transform.position - new Vector3(OffsettX,OffsettY,OffsettZ);
        Camera.transform.position = newPos;
        
        Camera.SetActive(PlayerState.Instance.Cs != PlayerState.CurrentState.AT_STORE);
    }
}
