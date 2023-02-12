using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    protected void Update() {
        // check if off screen
        if (transform.position.x < Camera.main.transform.position.x - 10f) {
            Destroy(gameObject);
        }
    }
}