using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour, Iinteract
{
    [SerializeField] private Animator Ani;
    
    public bool IsLockedDoor; 
    private void Start()
    {
        // Ani = gameObject.GetComponent<Animator>();
    }
    public void Interact()
    {
        if (IsLockedDoor == false)
        {
            if (Ani.GetBool("IsOpen") == false)
            {
                Ani.SetBool("IsOpen", true);
                AudioManager.Instance.PlaySFX(AudioManager.Instance.doorClip, 1f);
            }
            else
            {
                Ani.SetBool("IsOpen", false);
                AudioManager.Instance.PlaySFX(AudioManager.Instance.doorClip, 1f);
            }
        }
    }
}
