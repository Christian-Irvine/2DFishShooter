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
    private Rigidbody2D rb;
    private float ground;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.AddForce(new Vector2(Random.Range(-11, 11), Random.Range(8f, 11f)));
        ground = Random.Range(-3.5f, -4.5f);
    }

    private void Update()
    {
        if (transform.position.y <= ground) rb.simulated = false;
    }
}
