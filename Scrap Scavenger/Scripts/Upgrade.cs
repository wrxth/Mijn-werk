using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : Singleton<Upgrade>
{
    public bool Shield;
    public bool Pulse;

    [SerializeField] private Movement m;
    [SerializeField] private float SpeedBuff;
    [SerializeField] private ArmScrapGrabber arm;

    [Header("store prices")]
    [SerializeField] private int ShieldCost = 10;
    [SerializeField] private int SpeedCost = 10;
    [SerializeField] private int RollCost = 10;
    [SerializeField] private int PulseCost = 30;
    [SerializeField] private int ArmCost = 10;

    [SerializeField] private GameObject ShieldBubble;

    private void Update()
    {
        if (Shield == true)
        {
            ShieldBubble.SetActive(true);
        }
        else
        {
            ShieldBubble.SetActive(false);
        }
    }
    public void ActivateShield()
    {
        //Debug.Log("ping");
        if (Score.Instance.Scrap >= ShieldCost)
        {
            SoundEffect.Instance.playSound(SoundEffect.Instance.shopbuy);
            Shield = true;
            Score.Instance.RemoveGold(ShieldCost);
        }
    }
    public void AcquirePulse()
    {
        if (Score.Instance.Scrap >= PulseCost)
        {
            SoundEffect.Instance.playSound(SoundEffect.Instance.shopbuy);
            Pulse = true;
            Score.Instance.RemoveGold(PulseCost);
        }
    }

    public void DeactivateShield()
    {
        Shield = false;
    }

    public void IncreaseSpeed()
    {
        if (Score.Instance.Scrap >= SpeedCost)
        {
            SoundEffect.Instance.playSound(SoundEffect.Instance.shopbuy);
            m.DirectionalSpeedBuff = SpeedBuff;
            Score.Instance.RemoveGold(SpeedCost);
        }
    }
    public void AcquireRoll()
    {
        if (Score.Instance.Scrap >= RollCost)
        {
            SoundEffect.Instance.playSound(SoundEffect.Instance.shopbuy);
            m.RolAcquired = true;
            Score.Instance.RemoveGold(RollCost);
        }
    }

    // arm extra range code invullen
    public void IncreaseArm()
    {
        if (Score.Instance.Scrap >= ArmCost)
        {
            SoundEffect.Instance.playSound(SoundEffect.Instance.shopbuy);
            arm.armrange += 40f;
            Score.Instance.RemoveGold(ArmCost);
        }

    }

}
