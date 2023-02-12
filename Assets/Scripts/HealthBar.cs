using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float percentRemaining = 1.0f;
    [SerializeField] private Transform foregroundSprite = null;
    [SerializeField] private Transform backgroundSprite = null;

    private bool activated = false;

    void Start()
    {
        if (percentRemaining == 1f)
        {
            foreach(SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
            {
                sr.enabled = false;
            }
        }
    }

    void Update()
    {
        if (percentRemaining <= 0f) percentRemaining = 0f;

        if (!activated && percentRemaining < 1f)
        {
            activated = true;
            foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
            {
                sr.enabled = true;
            }
        }

        foregroundSprite.localScale = new Vector3(percentRemaining, 1, 1);
        foregroundSprite.localPosition = new Vector3(backgroundSprite.localPosition.x - backgroundSprite.localScale.x * (1 - percentRemaining) / 2, foregroundSprite.localPosition.y, foregroundSprite.localPosition.z);
    }
}
