using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public static DropManager Instance;

    [HideInInspector] public List<Drop> droppedCoins = new List<Drop>();
    [SerializeField] private float coinPickupCooldown;
    public Transform coinPickupLocation;
    public Transform gunPickupLocation;
    public float dropPickupSpeed;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RunManager.Instance.EndDay.AddListener(OnEndDay);
    }

    private void OnEndDay()
    {
        StartCoroutine(PickupCoins());
    }

    IEnumerator PickupCoins()
    {
        yield return new WaitForSeconds(coinPickupCooldown);

        foreach (Drop coin in droppedCoins.ToList())
        {
            StartCoroutine(coin.PickupDrop(coinPickupLocation.position, Random.Range(0, 1.5f)));
        }

        yield return new WaitUntil(() => droppedCoins.Count == 0);
        RunManager.Instance.DropsPickedUp?.Invoke();
    }
}
