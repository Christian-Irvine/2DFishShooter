using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivingObject : MonoBehaviour
{   
    protected UnityEvent Death = new UnityEvent();
    protected UnityEvent DespawnCollision = new UnityEvent();

    private float health;
    public float Health
    {
        get { return health; }
        set 
        { 
            health = value; 
            CheckDeath();
        }
    }

    [SerializeField] private float maxHealth;
    public float MaxHealth
    {
        get { return maxHealth; }
    }

    protected void Start()
    {
        Health = MaxHealth;
        OnStart();
    }

    protected virtual void OnStart() { }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Death?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Despawner"))
        {
            DespawnCollision?.Invoke();
        }
    }
}
