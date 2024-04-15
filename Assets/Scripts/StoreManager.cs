using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static StoreManager Instance;

    private GunType currentGunTab;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentGunTab = GunType.Pistol;
    }

    public void ExitStore()
    {
        gameObject.SetActive(false);
        RunManager.Instance.StartNextDay();
    }

    public void ChangeGun(int gunIndex)
    {
        currentGunTab = (GunType)gunIndex;
        
    }
}
