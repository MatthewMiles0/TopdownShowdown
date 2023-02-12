using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Slimbo : Enemy
{
    private Transform target;
    public float speed = 0.5f;
    public float detectionDistance = 10f;

    public float changeOfLookingForNewTarget = 0.1f;

    public float lurchFrequency = 0.5f;
    public float lurchStrength = 50f;

    private float timeOfLastLurch = 0f;

    new void Update()
    {
        base.Update();

        if (Time.time - timeOfLastLurch > 1f/lurchFrequency && target != null)
        {
            timeOfLastLurch = Time.time;

            if (Random.Range(0f, 1f) < 0.5f)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * lurchStrength * Random.Range(0.5f, 1f));
            } else {
                GetComponent<Rigidbody2D>().AddForce((target.position - transform.position).normalized * lurchStrength * Random.Range(0.5f, 1f));
            }
        }

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
