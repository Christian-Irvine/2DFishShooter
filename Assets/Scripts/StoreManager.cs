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

    private void UpdateStoreInformation()
    {
        
    }

    public void UpgradeReloadSpeed()
    {
        GunStatManager.Instance.GunUpgrades[(int)currentGunTab].reloadSpeed.currentUses += 1;

        float decimalIncrease = GunStatManager.Instance.GunUpgrades[(int)currentGunTab].reloadSpeed.amountPerUse;
        int count = GunStatManager.Instance.GunUpgrades[(int)currentGunTab].reloadSpeed.currentUses;

        GunStatManager.Instance.AllGuns[0].gun.SetCompoundReloadSpeed(decimalIncrease, count);
    }

    public void UpgradeShootSpeed()
    {
        GunStatManager.Instance.GunUpgrades[(int)currentGunTab].shootSpeed.currentUses += 1;

        float decimalIncrease = GunStatManager.Instance.GunUpgrades[(int)currentGunTab].shootSpeed.amountPerUse;
        int count = GunStatManager.Instance.GunUpgrades[(int)currentGunTab].shootSpeed.currentUses;

        GunStatManager.Instance.AllGuns[0].gun.SetCompoundShootSpeed(decimalIncrease, count);
    }

    public void UpgradeDamage()
    {
        GunStatManager.Instance.GunUpgrades[(int)currentGunTab].bulletDamage.currentUses += 1;

        float decimalIncrease = GunStatManager.Instance.GunUpgrades[(int)currentGunTab].bulletDamage.amountPerUse;
        int count = GunStatManager.Instance.GunUpgrades[(int)currentGunTab].bulletDamage.currentUses;

        GunStatManager.Instance.AllGuns[0].gun.SetCompoundBulletDamage(decimalIncrease, count);
    }
}
