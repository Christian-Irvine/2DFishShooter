using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivingObject : MonoBehaviour
{
    public class DropEvent : UnityEvent<Drop> { }

    protected UnityEvent Death = new UnityEvent();
    protected UnityEvent DespawnCollision = new UnityEvent();
    protected DropEvent DropItem = new DropEvent();
    [SerializeField] private GameObject drop;
    [SerializeField] private Vector2Int dropChance = new Vector2Int(1, 1);
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject healthBarMask;

    [SerializeField] private float xPMultiplier;
    public float XPMultiplier { get { return xPMultiplier; } }

    private float health;
    public float Health
    {
        get { return health; }
        set 
        { 
            health = value; 
            CheckDeath();
            UpdateHealthBar();
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
        healthBar.SetActive(false);
        OnStart();
    }

    protected virtual void OnStart() { }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            if (drop != null)
            {
                for (int i = 0; i < Random.Range(dropChance.x, dropChance.y + 1); i++)
                {
                    Drop droppedCoin = Instantiate(drop, transform.position, Quaternion.identity).GetComponent<Drop>();
                    DropManager.Instance.droppedCoins.Add(droppedCoin);

                    DropItem?.Invoke(droppedCoin); 
                }
            }
               
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

    private void UpdateHealthBar()
    {
        if (Health < MaxHealth) healthBar.SetActive(true);
        healthBarMask.transform.localScale = new Vector3(Health / MaxHealth, 1, 1);
    }
}
