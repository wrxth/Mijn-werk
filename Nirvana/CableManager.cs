using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CableManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Cables;
    [SerializeField] private PowerBox Pb;
    private GameObject Player;
    [SerializeField] private GameObject ReturnPos;

    private TVScript TVScript;

    [SerializeField] private GameObject PlayerPin;

    [SerializeField] private Image FadeInScreen;

    [SerializeField] private AudioSource FuzeAudio;
    [SerializeField] private AudioClip Zap;
    [SerializeField] private AudioClip Click;
    [SerializeField] private AudioClip Buzzing;

    [SerializeField] private Animator Switch;

    private bool canPlay = true;
    private void Start()
    {
        Invoke("DelayStart", 1f);
    }
    void Update()
    {
        if (CheckStatus() && !Pb.CorrectPower())
        {
            Debug.Log("fails in cable manager");
            StartCoroutine(FailState());
        }
        else if (CheckStatus() && Pb.CorrectPower())
        {
            FuzeAudio.clip = Click;
            FuzeAudio.Play();
            Switch.SetBool("NeedToFlip", true);

            GameObject Obj = GameObject.Find("tvknop");
            TVScript = Obj.GetComponent<TVScript>();
            TVScript.IsRunning = true;
            Lights.instance.GeneratorOn = true;
            if (canPlay)
            {
                AudioManager.Instance.PlaySFX(Buzzing, 0.35f);
                canPlay = false;
            }
        }
        if (ShowPin() && PlayerPin != null)
        {
            PlayerPin.GetComponent<MeshRenderer>().enabled = true;
        }
        else if (PlayerPin != null)
        {
            PlayerPin.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    
    private bool CheckStatus()
    {
        // Amount correctly connected pins
        int succesAmount = 0;
        
        // Check the pins
        for (int i = 0; i < Cables.Length; i++)
        {
            Cable c = Cables[i].GetComponent<Cable>();
            if (c != null)
            {
                // Pin not connected
                if (c.CS == Cable.CableState.CONNECTING || c.CS == Cable.CableState.NOT_CONNECTED)
                {
                    return false;
                }
                else if (c.CS == Cable.CableState.WRONG_SEQUENCE)    // Pin connected to the Wrong Port
                {
                    Debug.Log("fail");
                }
                else if (c.CS == Cable.CableState.CONNECTED)         // Pinn connected to the correct port
                {
                    Debug.Log("Succeed");
                    succesAmount++;
                }
            }
        }
        
        // Fail detected
        if (succesAmount != Cables.Length)
        {
            
            StartCoroutine(FailState());
            Debug.Log("succes ammount: " + succesAmount + "cable ammount" + Cables.Length);
            succesAmount = 0;
            return false;
        }
        
        // All pins passed the checks
        return true;
    }

    private bool ShowPin()
    {
        for (int i = 0; i < Cables.Length; i++)
        {
            Cable c = Cables[i].GetComponent<Cable>();

            if (c != null)
            {
                if (c.CS == Cable.CableState.CONNECTING)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void DelayStart()
    {
        PlayerPin = GameObject.Find("pin");
        Player = GameObject.Find("Player");
        FadeInScreen = GameObject.Find("FadeOut").GetComponent<Image>();
        FadeInScreen.canvasRenderer.SetAlpha(0f);
    }

    private IEnumerator FailState()
    {
        FuzeAudio.Play();
        FuzeAudio.clip = Zap;
        FuzeAudio.Play();
        FadeInScreen.CrossFadeAlpha(1, 1, false);
        for (int i = 0; i < Cables.Length; i++)
        {
            Cables[i].GetComponent<Cable>().Reset();
        }
        yield return new WaitForSeconds(1f);
        Player.transform.position = ReturnPos.transform.position;
        FadeInScreen.canvasRenderer.SetAlpha(0f);
    }
}
