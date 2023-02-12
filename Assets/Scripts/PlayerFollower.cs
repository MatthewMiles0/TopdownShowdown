using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerFollower : MonoBehaviour
{
    public Transform player;

    [SerializeField] private float maxDistanceFromPlayer = 1f;

    [SerializeField] GameObject leftWall;

    void Update()
    {
        float cameraToPlayer = player.transform.position.x - transform.position.x;
        if (cameraToPlayer > maxDistanceFromPlayer)
        {
            transform.position += new Vector3(cameraToPlayer - maxDistanceFromPlayer, 0f, 0f);
        } else {
            transform.position += Vector3.right * Time.deltaTime * 0.125f;
        }

        // move the left wall to the left edge of the camera
        // wall width = 0.5f
        leftWall.transform.position = new Vector3(transform.position.x - Camera.main.orthographicSize * Camera.main.aspect - 0.5f, leftWall.transform.position.y, leftWall.transform.position.z);
    }
}
