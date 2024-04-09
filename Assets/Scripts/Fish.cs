using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Fish : MonoBehaviour
{
    public class FishEvent : UnityEvent<Fish> { }

    public FishEvent RemoveFish = new FishEvent();

    [HideInInspector] public int direction;

    [SerializeField] private float speed;

    void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x * -direction, transform.localScale.y, transform.localScale.z);
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
