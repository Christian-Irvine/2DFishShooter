using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Fish : LivingObject
{
    public class FishEvent : UnityEvent<Fish> { }

    public FishEvent RemoveFish = new FishEvent();

    [HideInInspector] public int direction;

    [SerializeField] private float baseSpeed;
    private float speed;

    protected override void OnStart()
    {
        transform.localScale = new Vector3(transform.localScale.x * -direction, transform.localScale.y, transform.localScale.z);

        float speedVariance = baseSpeed / 5;
        speed = Random.Range(baseSpeed - speedVariance, baseSpeed + speedVariance);
    }

    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * speed * direction;
    }

    private void Die()
    {
        RemoveFish?.Invoke(this);
    }

    private void LeaveSide()
    {
        RemoveFish?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Despawner"))
        {
            FishSpawner.Instance.RemoveFish(this);
        }
    }
}
