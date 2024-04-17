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

    [SerializeField] private LayerMask layerMask;
    private float timeOfLastShot;
    private bool mouseDown;
    private int bulletsInChamber;
    private float compoundReloadSpeed;
    private float compoundShootCooldown;
    private float compoundBulletDamage;

    private void Start()
    {
        RunManager.Instance.ChangeDay.AddListener(StartDay);
        RunManager.Instance.StartNewRun.AddListener(StartRun);
        compoundBulletDamage = bulletDamage;
        compoundReloadSpeed = reloadTime;
        compoundShootCooldown = cooldown;
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
        if (Time.time - timeOfLastShot >= compoundShootCooldown && bulletsInChamber > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        timeOfLastShot = Time.time;
        bulletsInChamber -= 1;
        //Debug.Log($"Ammo: {bulletsInChamber}/{chamberSize}");

        for (int shots = 1; shots <= spreadShotCount; shots++)
        {
            Vector3 rayPos = transform.position + new Vector3(Random.Range(-spreadRadius, spreadRadius), Random.Range(-spreadRadius, spreadRadius), 0);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, -Vector2.zero, Mathf.Infinity, layerMask);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Fish"))
                {
                    LivingObject hitFish = hit.collider.GetComponent<LivingObject>();
                    float distance =  Mathf.Abs(transform.position.x + transform.position.y - hitFish.transform.position.x + hitFish.transform.position.y);

                    //The * 5 just keeps the XP at a reasonable number
                    float xp = (hitFish.XPMultiplier / (distance * 5) * compoundBulletDamage);
                    RunManager.Instance.XP += xp;
                    Debug.Log("Hit" + compoundBulletDamage);
                    hitFish.Health -= compoundBulletDamage;
                }
            }
        }

        if (bulletsInChamber == 0) StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        bulletsInChamber = chamberSize;
        Debug.Log("Reloaded!");
    }

    private void StartDay(int day)
    {
        bulletsInChamber = chamberSize;
    }

    private void StartRun()
    {
        compoundBulletDamage = bulletDamage;
    }

    public void SetCompoundReloadSpeed(float decimalChange, int count)
    {
        compoundReloadSpeed = GetCompoundValue(reloadTime, decimalChange, count);

        Debug.Log("Reload " + compoundReloadSpeed);
    }

    public void SetCompoundShootSpeed(float decimalChange, int count)
    {
        compoundShootCooldown = GetCompoundValue(cooldown, decimalChange, count);

        Debug.Log("Cooldown " + compoundShootCooldown);
    }

    public void SetCompoundBulletDamage(float decimalChange, int count)
    {
        compoundBulletDamage = GetCompoundValue(bulletDamage, decimalChange, count);

        Debug.Log("Damage " + compoundBulletDamage);
    }

    private float GetCompoundValue(float original, float decimalChange, int count)
    {
        return original * Mathf.Pow(1 + decimalChange, count);
    }
}
