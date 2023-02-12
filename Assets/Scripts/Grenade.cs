using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Grenade : Bullet
{
    [SerializeField] private GameObject explosionVisuals;

    public float explosionRadius = 5f;
    public float maxExplosionDamage = 20f;
    public float minExplosionDamage = 5f;

    private LayerMask blockingLayers;

    new void Start()
    {
        blockingLayers = LayerMask.GetMask(new string[] { "Obstacle", "Enemy" });
        GetComponent<Rigidbody2D>().AddForce(transform.right * speed, ForceMode2D.Impulse);
        StartCoroutine(ExplodeAfterTime(3f));
    }

    new void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, speed * Time.deltaTime);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Enemy>().takeDamage(1, source);
            }
        }
    }

    private void explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                // Check line of sight
                RaycastHit2D hit = Physics2D.Raycast(transform.position, collider.transform.position - transform.position, explosionRadius, blockingLayers);

                if (hit.collider != null && hit.collider != collider) {
                    // Debug.Log("Hit "+hit.collider.gameObject.name+" instead of "+collider.gameObject.name);
                    continue;
                }

                collider.gameObject.GetComponent<Enemy>().takeDamage(Mathf.Lerp(minExplosionDamage, maxExplosionDamage,
                    (explosionRadius - Vector2.Distance(this.transform.position, collider.transform.position))/explosionRadius), source);
            }
        }
        GameObject explosion = Instantiate(explosionVisuals, transform.position, transform.rotation);
        explosion.GetComponent<Explosion>().radius = explosionRadius;
        Destroy(gameObject);
    }

    IEnumerator ExplodeAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        explode();
    }
}
