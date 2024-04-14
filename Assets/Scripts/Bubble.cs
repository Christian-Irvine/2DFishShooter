using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    [SerializeField] private float speed;
    public List<Sprite> frames = new List<Sprite>();

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.AddForce(new Vector2(Random.value * 5 - 1, Random.value * 5));
        BubbleManager.Instance.bubbles.Remove(this);
        StartCoroutine(PopAnimation(Random.Range(1.5f, 3.5f)));
    }

    public void Pop()
    {
        BubbleManager.Instance.bubbles.Remove(this);
        StartCoroutine(PopAnimation(0));
    }

    IEnumerator PopAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (Sprite frame in frames)
        {
            spriteRenderer.sprite = frame;
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject);
    }
}

