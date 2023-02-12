using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 1f;
    public GameObject source;
    public float damage = 10f;

    protected void Start()
    {
        Destroy(gameObject, lifeTime);
        if (source == null)
        {
            source = gameObject;
        }
    }

    protected void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, speed * Time.deltaTime);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Enemy>().takeDamage(damage, source);
                // Debug.Log("hit enemy");
            }
            Destroy(gameObject);
        }

        transform.position += transform.right * speed * Time.deltaTime;
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     // gameObject.SetActive(false);
    //     Destroy(gameObject);

    //     if (collision.gameObject.tag == "Damagable")
    //     {
    //         // other.gameObject.GetComponent<Health>().takeDamage(1, source);
    //     }
    // }
}
