using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Guns
{
    public string name;
    public Gun gun;
}

public class GunManager : MonoBehaviour
{
    [SerializeField] private List<Guns> allGuns;  
    private List<Gun> collectedGuns = new List<Gun>();
    private bool moveGun = true;

    private Gun currentGun;

    private void Start()
    {
        RunManager.Instance.StartNewRun.AddListener(StartRun);
    }

    private void StartRun()
    {
        collectedGuns.Add(GetGunFromName("pistol"));
        allGuns.ForEach(guns => guns.gun.gameObject.SetActive(false)); 
        ChangeGun(0);
    }

    private void Update()
    {
        if (moveGun) MoveGun();
    }

    private void MoveGun()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }

    private Gun GetGunFromName(string name)
    {
        foreach (Guns guns in allGuns)
        {
            if (guns.name == name) return guns.gun;
        }

        Debug.LogWarning($"Gun {name} does not exist, returning null");
        return null;
    }

    private void ChangeGun(int gunIndex)
    {
        if (currentGun != null) currentGun.gameObject.SetActive(false);
        currentGun = collectedGuns[gunIndex];
        currentGun.gameObject.SetActive(true);
    }
}
