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

    [SerializeField] private GameObject sprite;
    [SerializeField] private float baseSpeed;
    private float speed;

    protected override void OnStart()
    {
        sprite.transform.localScale = new Vector3(transform.localScale.x * -direction, transform.localScale.y, transform.localScale.z);

        float speedVariance = baseSpeed / 5;
        speed = Random.Range(baseSpeed - speedVariance, baseSpeed + speedVariance);

        Death.AddListener(Die);
        DespawnCollision.AddListener(LeaveSide);
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
}
