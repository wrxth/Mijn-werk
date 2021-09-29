using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FireCanons : MonoBehaviour
{

    public static FireCanons Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }
    [SerializeField] private Transform[] ShotPointsL,ShotPointsR, ShotpointsF;
    [SerializeField] private AudioSource[] CanonSoundsL, CanonSoundsR;

    [SerializeField] private GameObject cam;

    [SerializeField] public int CanonBalls;
    [SerializeField] private TMP_Text Ammo;

   public int VolleyAmount = 1;

    public int NoExtraCanons = -3;

    [SerializeField] private float TimeBetweenShots = 3f;
    private float ShotTime;
    void Start()
    {
        
    }

   
    void Update()
    {
        if (PlayerMode.Instance.pm == PlayerMode.PlayerModus.SHIP)
        {


            Ammo.text = "CanonBalls: " + CanonBalls;
            Vector3 lookingSide = transform.InverseTransformPoint(cam.transform.position);

            ShotTime += Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && Time.timeScale != 0 && ShotTime > TimeBetweenShots)
            {


                if (lookingSide.z < 0)
                {
                    StartCoroutine(FireLeftVolley());

                }
                if (lookingSide.z > 0)
                {

                    StartCoroutine(FireRightVolley());

                }
                ShotTime = 0;
            }

            if (Input.GetMouseButtonDown(1))
            {
                FireFront();
            }
        }
    }

    private IEnumerator FireLeftVolley()
    {
        for (int i = 0; i < VolleyAmount; i++)
        {
            FireLeft();
            yield return new WaitForSeconds(0.3f);
        }
        
    }
    private IEnumerator FireRightVolley()
    {
        for (int i = 0; i < VolleyAmount; i++)
        {
            FireRight();
            yield return new WaitForSeconds(0.3f);
        }
        
    }
    
    private void FireLeft()
    {
        for (int i = 0; i < ShotPointsL.Length + NoExtraCanons; i++)
        {
            if (CanonBalls > 0)
            {
                GenericObjectPool.Instance.SpawnFromPool("playerShot", ShotPointsL[i].position, ShotPointsL[i].rotation);
                CanonSoundsL[0].Play();
                CanonBalls--;
            }
           
        }

        
    }
    private void FireRight()
    {
        for (int i = 0; i < ShotPointsR.Length + NoExtraCanons; i++)
        {
            if (CanonBalls > 0)
            {
                GenericObjectPool.Instance.SpawnFromPool("playerShot", ShotPointsR[i].position, ShotPointsR[i].rotation);
                CanonSoundsL[0].Play();
                CanonBalls--;
            }
        }
    }
    private void FireFront()
    {
        for (int i = 0; i < ShotpointsF.Length; i++)
        {
            if (CanonBalls > 0)
            {
                GenericObjectPool.Instance.SpawnFromPool("playerShot", ShotpointsF[i].position, ShotpointsF[i].rotation);
                CanonSoundsR[i].Play();
                CanonBalls--;
            }
        }
    }

    public void AddCanonBalls(int _ammo)
    {
        CanonBalls += _ammo;
    }
}
