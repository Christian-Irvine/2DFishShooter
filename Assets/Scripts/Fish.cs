using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Fish : MonoBehaviour
{
    public class FishEvent : UnityEvent<Fish> { }

    public FishEvent RemoveFish = new FishEvent();

    void Start()
    {
        
    }

    void Update()
    {
        
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
