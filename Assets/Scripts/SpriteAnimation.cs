using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private List<Sprite> frames;
    [SerializeField] private float frameDelay = 0.5f;

    private SpriteRenderer spriteRenderer;
    private int currentFrameIndex = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        while (true)
        {
            yield return new WaitForSeconds(frameDelay);
            ChangeFrame();
        }
    }

    private void ChangeFrame()
    {
        currentFrameIndex = (currentFrameIndex + 1) % frames.Count;
        spriteRenderer.sprite = frames[currentFrameIndex];
    }
}
