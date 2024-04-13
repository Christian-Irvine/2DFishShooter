using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropType
{
    Coin,
    Gun
}

[RequireComponent(typeof(Rigidbody2D))]
public class Drop : MonoBehaviour
{
    [SerializeField] private DropType droptype;
    [SerializeField] private float pickupDelay;
    private float spawnTime;
    private Rigidbody2D rb;
    private float ground;

    private void Awake()
    {
        spawnTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ground = Random.Range(-3.5f, -4.5f);
        rb.AddForce(new Vector2(0, Random.Range(-8f, 18f)));
    }

    private void Update()
    {
        if (transform.position.y <= ground)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time - spawnTime < pickupDelay) return;

        if (collision.gameObject.CompareTag("Crosshair"))
        {
            Destroy(gameObject);
        }
    }
}
