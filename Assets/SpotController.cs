using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotController : MonoBehaviour
{
    float timer;
    public SpriteRenderer spriteRenderer;
    int num = 0;

    static ObjectPool spotObjectPool;
    static Color[] colors = new Color[] { new Color(1, 1, 1, 1.0f), new Color(1, 1, 1, 0.9f), new Color(1, 1, 1, 0.8f), new Color(1, 1, 1, 0.7f), new Color(1, 1, 1, 0.6f), new Color(1, 1, 1, 0.5f), new Color(1, 1, 1, 0.4f), new Color(1, 1, 1, 0.3f), new Color(1, 1, 1, 0.2f), new Color(1, 1, 1, 0.1f), new Color(1, 1, 1, 0f) };
    [HideInInspector]public Sprite sprite;

    void Start()
    {
        reset();
        spotObjectPool = transform.parent.GetComponent<ObjectPool>();
    }

    public void reset()
    {
        spriteRenderer.sprite = sprite;
        num = 0;
        timer = 0;
        spriteRenderer.color = colors[num];
    }

    void Update()
    {
        timer += Time.deltaTime;
        while (timer >= 0.1f)
        {
            if (num < colors.Length)
            {
                spriteRenderer.color = colors[num++];
                timer -= 0.1f;
            }
            else
            {
                spotObjectPool.ReturnObject(gameObject);
                timer = 0;
            }
        }
    }
}
