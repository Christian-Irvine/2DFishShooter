using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class BubbleAnimation
{
    public List<Sprite> frames = new List<Sprite>();
}

public class BubbleManager : MonoBehaviour
{
    public static BubbleManager Instance;
    [SerializeField] private Bubble bubblePrefab;
    [SerializeField] List<BubbleAnimation> bubbleSprites;

    public List<Bubble> bubbles = new List<Bubble>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RunManager.Instance.EndDay.AddListener(OnEndDay);
    }

    public void SpawnRandomBubble(Vector2 position)
    {
        Bubble newBubble = Instantiate(bubblePrefab, position, Quaternion.identity, SpawnedObjectsManager.Instance.transform);

        int index = Random.Range(0, bubbleSprites.Count);

        newBubble.spriteRenderer.sprite = bubbleSprites[index].frames[0];
        newBubble.frames = bubbleSprites[index].frames;

        bubbles.Add(newBubble);
    }

    private void OnEndDay()
    {
        PopAllBubbles();
    }

    private void PopAllBubbles()
    {
        foreach (Bubble bubble in bubbles) 
        {
            StartCoroutine(PopBubble(bubble, Random.value * 0.8f));
        }
    }

    public IEnumerator PopBubble(Bubble bubble, float delay)
    {
        yield return new WaitForSeconds(delay);
        bubble.Pop();
    }
}
