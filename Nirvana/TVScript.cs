using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVScript : MonoBehaviour, Iinteract
{
    public Material TvTip;
    [SerializeField] AudioClip clip;
    public bool IsRunning;
    private bool playOnce = true;

    public void Interact()
    {
        if (IsRunning == true)
        {
            
            TvTip.EnableKeyword("_EMISSION");

            if (playOnce == true)
            {
               AudioManager.Instance.PlaySFX(clip, 0.05f);
            }
        }   
    }

    private void OnApplicationQuit()
    {
        TvTip.DisableKeyword("_EMISSION");
    }

}
