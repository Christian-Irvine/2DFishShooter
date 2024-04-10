using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private int spreadShotCount;
    [SerializeField] private float spreadRadius;
    [SerializeField] private float bulletDamage;
    [SerializeField] private bool holdToShoot;
    [SerializeField] private float reloadTime;
    [SerializeField] private int chamberSize;
    [SerializeField] private float cooldown;
    private float timeOfLastShot;
    private bool mouseDown;
    private int bulletsInChamber;

    private void Start()
    {
        bulletsInChamber = chamberSize;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
            TryShoot();
        }
        else if (Input.GetMouseButtonUp(0)) mouseDown = false;
        else if (holdToShoot && mouseDown) TryShoot();

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    private void TryShoot()
    {
        if (Time.time - timeOfLastShot >= cooldown && bulletsInChamber > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        timeOfLastShot = Time.time;
        bulletsInChamber -= 1;
        Debug.Log($"Ammo: {bulletsInChamber}/{chamberSize}");
    }

    IEnumerator Reload()
    {
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        bulletsInChamber = chamberSize;
        Debug.Log("Reloaded!");
        Debug.Log($"Ammo: {bulletsInChamber}/{chamberSize}");
    }
}
