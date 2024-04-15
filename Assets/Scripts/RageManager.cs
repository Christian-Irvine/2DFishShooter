using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageManager : MonoBehaviour
{
    public static RageManager Instance;

    [SerializeField] private float baseMaxRage;
    [SerializeField] private float rageAmount;
    public float RageAmount
    {
        get { return rageAmount; }
        set 
        { 
            rageAmount = Mathf.Clamp(value, 0, baseMaxRage);

            Debug.Log(RageAmount);

            if (RageAmount == baseMaxRage && !RunManager.Instance.RunOver)
            {
                RunManager.Instance.EndRun?.Invoke();
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}
