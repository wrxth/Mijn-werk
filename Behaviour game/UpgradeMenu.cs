using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace steering
{
    // gemaakt door: finn
    public class UpgradeMenu : MonoBehaviour
    {
        // instance
        public static UpgradeMenu instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
        [SerializeField] private SteeringData SD;

        public float MaxSpeed;
        public float MaxDesiredVelocity;
        public float MaxSteeringForce;

        public int UpgradePoints;
        private bool AllowUpgrade;

        // punten die in de stats zitten
        private int MSU;
        private int ASU;
        private int ADU;
        private int SRU;
        private int DU;

        // visueele representatie van de punten
        [SerializeField] private Text Points;
        [SerializeField] private Text ASPoints;
        [SerializeField] private Text ADPoints;
        [SerializeField] private Text SRPoints;
        [SerializeField] private Text MSPoints;
        [SerializeField] private Text DUPoints;

        private void Start()
        {
            // punten goed zetten
            UpgradePoints = 100;
            MSU = 0;
            ASU = 0;
            ADU = 0;
            SRU = 0;
            DU = 0;


        }

        private void Update()
        {
            // de punten in de text zetten
            Points.text = "Left Points: " + UpgradePoints;
            ASPoints.text = "Points invested: " + ASU;
            ADPoints.text = "Points invested: " + ADU;
            SRPoints.text = "Points invested: " + SRU;
            MSPoints.text = "Points invested: " + MSU;
            DUPoints.text = "Points invested: " + DU;

            // kijken of er nog upgrade points beschikbaar zijn
            if (UpgradePoints <= 1)
            {
                AllowUpgrade = false;
            }
            if (UpgradePoints >= 1)
            {
                AllowUpgrade = true;
            }
        }

        // verschillende stat buff functie
        public void MoveSpeedMinus()
        {
            if (UpgradePoints <= 100 && MSU >= 1)
            {
                MaxSpeed -= 0.2f;
                MaxDesiredVelocity--;
                MaxSteeringForce--;
                UpgradePoints++;
                MSU--;
            }
            else
            {
                Debug.Log("Can't downgrade Movement Speed");
            }
        }
        public void MoveSpeedPlus()
        {
            if (AllowUpgrade)
            {
                MaxSpeed += 0.2f;
                MaxDesiredVelocity++;
                MaxSteeringForce++;
                UpgradePoints--;
                MSU++;
            }
            if (!AllowUpgrade)
            {
                Debug.Log("Can't upgrade Movement Speed");
            }
        }

        public void AttackSpeedMinus()
        {
            if (UpgradePoints <= 100 && ASU >= 1)
            {
                UpgradeData.Instance.AtkInterval += 0.05f;
                UpgradePoints++;
                ASU--;
            }
            else
            {
                Debug.Log("Can't downgrade Attack Speed");
            }
        }
        public void AttackSpeedPlus()
        {
            if (AllowUpgrade)
            {
                UpgradeData.Instance.AtkInterval -= 0.05f;
                UpgradePoints--;
                ASU++;
            }
            if (!AllowUpgrade)
            {
                Debug.Log("Can't upgrade Attack Speed");
            }
        }

        public void AttackDamageMinus()
        {
            if (UpgradePoints <= 100 && ADU >= 1)
            {
                UpgradeData.Instance.DamageBuff--;
                UpgradePoints++;
                ADU--;
            }
            else
            {
                Debug.Log("Can't downgrade Attack Damage");
            }
        }
        public void AttackDamagePlus()
        {
            if (AllowUpgrade)
            {
                UpgradeData.Instance.DamageBuff++;
                UpgradePoints--;
                ADU++;
            }
            if (!AllowUpgrade)
            {
                Debug.Log("Can't upgrade Attack Damage");
            }
        }

        public void SightRangeMinus()
        {
            if (UpgradePoints <= 100 && SRU >= 1)
            {
                UpgradeData.Instance.AtkRange -= 0.2f;
                UpgradePoints++;
                SRU--;
            }
            else
            {
                Debug.Log("Can't downgrade Sight Range");
            }
        }
        public void SightRangePlus()
        {
            if (AllowUpgrade)
            {
                UpgradeData.Instance.AtkRange += 0.2f;
                UpgradePoints--;
                SRU++;
            }
            if (!AllowUpgrade)
            {
                Debug.Log("Can't upgrade Sight Range");
            }
        }

        public void DefenceMinus()
        {
            if (UpgradePoints <= 100 && DU >= 1)
            {
                UpgradeData.Instance.MaxhealthBuff -= 5;
                UpgradePoints++;
                DU--;
            }
            else
            {
                Debug.Log("Can't downgrade Defence");
            }
        }
        public void DefencePlus()
        {
            if (AllowUpgrade)
            {
                UpgradeData.Instance.MaxhealthBuff += 5;
                UpgradePoints--;
                DU++;
            }
            if (!AllowUpgrade)
            {
                Debug.Log("Can't upgrade Defence");
            }
        }
    }
}
