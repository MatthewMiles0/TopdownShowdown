using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombert : Enemy
{
    private Transform target;
    public float speed = 0.5f;
    public float detectionDistance = 10f;

    public float changeOfLookingForNewTarget = 0.1f;

    new void Update()
    {
        base.Update();

        if (target != null)
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            newPos.z = transform.position.z;

            transform.position = newPos;
        }

        if (target == null && Random.Range(0f, 1f) < changeOfLookingForNewTarget*Time.deltaTime)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

            if (targets.Length == 0)
            {
                Debug.LogError("No player found!");
            }

            target = targets[Random.Range(0, targets.Length)].transform;

            if (Vector3.Distance(transform.position, target.position) > detectionDistance)
            {
                target = null;
            }
        }
    }
}
