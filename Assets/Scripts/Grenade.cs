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

    public float knockbackScale = 3f;

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
        
        // If grenade hits enemy, deal damage
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(1, source, knockback);
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

                float ppd = GetPercentPointDistance(collider.transform.position);
                collider.gameObject.GetComponent<Enemy>().TakeDamage(
                    Mathf.Lerp(minExplosionDamage, maxExplosionDamage,
                    ppd), source, knockback*knockbackScale*ppd
                );
            }
        }
        GameObject explosion = Instantiate(explosionVisuals, transform.position, transform.rotation);
        explosion.GetComponent<Explosion>().radius = explosionRadius;
        Destroy(gameObject);
    }

    private float GetPercentPointDistance(Vector3 point)
    {
        return (explosionRadius - Vector2.Distance(this.transform.position, point))/explosionRadius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    IEnumerator ExplodeAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        explode();
    }
}
