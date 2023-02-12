using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 5f;
    public float duration = 1f;
    public float startOpacity = 0.8f;

    private float opacity;

    void Start()
    {
        opacity = startOpacity;
        transform.localScale = Vector3.one * radius * 2;
    }

    void Update()
    {
        opacity -= Time.deltaTime / duration;
        if (opacity <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, opacity);
        }    
    }
}
