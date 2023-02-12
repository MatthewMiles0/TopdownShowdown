using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndBounce : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float bounceSpeed = 1f;
    public float bounceHeight = 0.5f;

    public float bounceCycle = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        // rotate
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // pulse size using a sine wave
        bounceCycle += Time.deltaTime * bounceSpeed;
        transform.localScale = Vector3.one * (1 + Mathf.Sin(bounceCycle) * bounceHeight);
    }
}
