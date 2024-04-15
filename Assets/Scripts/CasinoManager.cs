using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoManager : MonoBehaviour
{
    public static CasinoManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void BackToHub()
    {
        RunManager.Instance.EndRunReset?.Invoke();
        HubManager.Instance.StartHub();
    }
}
