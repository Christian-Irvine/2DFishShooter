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
        RunManager.Instance.ChangeDay.AddListener(StartDay);
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

        for (int shots = 1; shots <= spreadShotCount; shots++)
        {
            Vector3 rayPos = transform.position + new Vector3(Random.Range(-spreadRadius, spreadRadius), Random.Range(-spreadRadius, spreadRadius), 0);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, -Vector2.up);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Fish"))
                {
                    LivingObject hitFish = hit.collider.GetComponent<LivingObject>();
                    hitFish.Health -= bulletDamage;
                }
            }
        }
    }

    IEnumerator Reload()
    {
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        bulletsInChamber = chamberSize;
        Debug.Log("Reloaded!");
        Debug.Log($"Ammo: {bulletsInChamber}/{chamberSize}");
    }

    private void StartDay(int day)
    {
        bulletsInChamber = chamberSize;
    }
}
