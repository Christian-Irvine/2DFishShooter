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

    [SerializeField] private List<GunUpgrade> gunUpgrades;
    public List<GunUpgrade> GunUpgrades {  get { return gunUpgrades; } }

    [SerializeField] private List<Guns> allGuns;
    public List<Guns> AllGuns
    {
        get { return allGuns; }
        set { allGuns = value; }
    }
    private List<Gun> collectedGuns = new List<Gun>();
    public List<Gun> CollectedGuns
    {
        get { return collectedGuns; }
        set { collectedGuns = value; }
    }

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
        foreach (GunUpgrade upgrade in gunUpgrades)
        {
            upgrade.reloadSpeed.currentUses = 0;
            upgrade.shootSpeed.currentUses = 0;
            upgrade.bulletDamage.currentUses = 0;
        }
    }
}
