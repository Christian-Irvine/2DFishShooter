using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public void ExitStore()
    {
        gameObject.SetActive(false);
        RunManager.Instance.StartNextDay();
    }
}
