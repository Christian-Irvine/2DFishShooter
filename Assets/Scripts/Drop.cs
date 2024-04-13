using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropType
{
    Coin,
    Gun
}

[RequireComponent(typeof(Rigidbody2D))]
public class Drop : MonoBehaviour
{
    [SerializeField] private DropType droptype;
    [SerializeField] private float pickupDelay;
    [SerializeField] private float pickupSpeed;
    private float spawnTime;
    private Rigidbody2D rb;
    private float ground;
    private bool pickedUp = false;

    private void Awake()
    {
        spawnTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ground = Random.Range(-3.5f, -4.5f);
        rb.AddForce(new Vector2(Random.Range(-2, 2), Random.Range(-8f, 18f)));
    }

    private void Update()
    {
        if (transform.position.y <= ground)
        {
            StopFalling();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (pickedUp) return;

        if (Time.time - spawnTime < pickupDelay) return;

        if (collision.gameObject.CompareTag("Crosshair"))
        {
            Vector3 pos;

            switch (droptype)
            {
                case DropType.Coin:
                    pos = DropManager.Instance.coinPickupLocation.position;
                    break;
                case DropType.Gun:
                    pos = DropManager.Instance.gunPickupLocation.position;
                    break;
                default: 
                    pos = Vector3.zero; 
                    break;
            }

            StartCoroutine(PickupDrop(pos, 0));
        }
    }

    public IEnumerator PickupDrop(Vector3 movePos, float pickupDelay)
    {
        if (pickedUp) yield break;

        pickedUp = true;

        yield return new WaitForSeconds(pickupDelay);

        StopFalling();

        while (transform.position != movePos)
        {
            yield return new WaitForEndOfFrame();

            transform.position = Vector3.MoveTowards(transform.position, movePos, DropManager.Instance.dropPickupSpeed * Time.deltaTime);
        }

        switch (droptype)
        {
            case DropType.Coin:
                DropManager.Instance.droppedCoins.Remove(this);
                RunManager.Instance.Coins++;
                break;
        }

        Destroy(gameObject);
    }

    private void StopFalling()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
    }
}
