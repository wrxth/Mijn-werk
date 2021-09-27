using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCommand : MonoBehaviour
{
    public static UnitCommand Instance;

    public void Awake()
    {
        Instance = this;
    }


    public int UnitsSelected;

    [SerializeField] private GameObject UpgradeScreen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (UnitsSelected == 0)
        //{
        //    UpgradeScreen.SetActive(false);
        //}
        //else
        //{
        //    UpgradeScreen.SetActive(true);

        //}
    }
}
