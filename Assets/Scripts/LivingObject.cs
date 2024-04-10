using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingObject : MonoBehaviour
{   
    private float health;
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    [SerializeField] private float maxHealth;
    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    protected void Start()
    {
        Health = MaxHealth;
        OnStart();
    }

    protected virtual void OnStart() { }
}
