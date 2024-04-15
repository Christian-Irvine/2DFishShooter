using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunUpgradePart
{
    public string name;
    public string description;
    public string statistic;
    public int maxUses;
    public int currentUses = 0;
    public float amountPerUse;
    public int baseCost;
    public float decimalCostIncrease;
}

[System.Serializable]
public class GunUpgrade
{
    public GunType type;
    public GunUpgradePart reloadSpeed;
    public GunUpgradePart shootSpeed;
    public GunUpgradePart bulletDamage;
}
