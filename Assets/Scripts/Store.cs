using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public static Store Instance;

    private void Awake()
    {
        Store.Instance = this;
    }

    public void ExitStore()
    {
        gameObject.SetActive(false);
        RunManager.Instance.StartNextDay();
    }
}
