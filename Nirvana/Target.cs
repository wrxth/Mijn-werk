using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Animator[] AnimatorsToActivate;
    [SerializeField] private AudioClip ClosetOpen;
    [SerializeField] private GameObject portalCollider;
    private bool HasPlayed = false;
    void Update()
    {
        
    }

    public void MirrorsAlligned()
    {
        if (HasPlayed == false)
        {
            AudioManager.Instance.PlaySFX(ClosetOpen);
            portalCollider.SetActive(true);

            HasPlayed = true;
        }
        for (int i = 0; i < AnimatorsToActivate.Length; i++)
        {
            AnimatorsToActivate[i].enabled = true;
            AnimatorsToActivate[i].SetBool("Activate", true);
        }
    }
}
