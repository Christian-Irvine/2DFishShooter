using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubManager : MonoBehaviour
{
    public static HubManager Instance;
    [SerializeField] private GameObject hubCanvas;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartHub();
    }

    public void StartRun()
    {
        RunManager.Instance.gameObject.SetActive(true);
        RunManager.Instance.StartCoroutine(RunManager.Instance.StartRun());
        hubCanvas.SetActive(false);
    }

    public void StartHub()
    {
        CasinoManager.Instance.gameObject.SetActive(false);
        RunManager.Instance.gameObject.SetActive(false);
        hubCanvas.SetActive(true);
    }
}
