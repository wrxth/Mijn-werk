using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMirror : MonoBehaviour, Iinteract
{
    public enum MirrorState
    {
        NOT_BEING_CONTROLLED,
        BEING_CONTROLLED
    }
    [SerializeField] private float RotationSpeed = 50f;
    [SerializeField] private float YRotation;

    [Header("min and max angle vars")]
    [Tooltip("the minimum angle value")]
    [SerializeField] private float Ymin;
    [Tooltip("the maximum angle value")]
    [SerializeField] private float Ymax;

    [SerializeField] private GameObject MirrorCam;
    [SerializeField] private GameObject Player;

    [SerializeField] private MirrorState MS;
    private void Start()
    {
        Invoke("DelayStart", 2f);
    }

    void Update()
    {
        if (MS == MirrorState.BEING_CONTROLLED)
        {
            YRotation = transform.eulerAngles.y;

            YRotation = Mathf.Clamp(YRotation, Ymin, Ymax);

            if (Input.GetKey(KeyCode.A))
            {
                YRotation += -RotationSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                YRotation += RotationSpeed * Time.deltaTime;
            }

            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, YRotation, transform.eulerAngles.z);
            if (MS == MirrorState.BEING_CONTROLLED && Input.GetKeyDown(KeyCode.E))
            {
                MS = MirrorState.NOT_BEING_CONTROLLED;
                MirrorCam.SetActive(false);
                Player.SetActive(true);
            }
        }
    }

    public void Interact()
    {
        MS = MirrorState.BEING_CONTROLLED;
        MirrorCam.SetActive(true);

        Player.SetActive(false);
    }

    public void DelayStart()
    {
        Player = GameObject.Find("Player");
    }
}
