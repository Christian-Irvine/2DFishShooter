using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum GunType
{
    Pistol,
    AssultRifle,
    Shotgun,
    SMG,
    Flamethrower
}

public class GunStatManager : MonoBehaviour
{
    public static GunStatManager Instance;
    //Do Not Modify This!
    [SerializeField] private List<GunUpgrade> baseUpgrades;
    public List<GunUpgrade> runUpgrades = new List<GunUpgrade>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RunManager.Instance.StartNewRun.AddListener(StartRun);
    }

    private void StartRun()
    {   
        foreach (GunUpgrade upgrade in baseUpgrades)
        {
            upgrade.reloadSpeed.currentUses = 0;
            upgrade.shootSpeed.currentUses = 0;
            upgrade.bulletDamage.currentUses = 0;
        }
    }
}
