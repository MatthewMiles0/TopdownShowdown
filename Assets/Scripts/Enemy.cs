using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : WorldObject
{
    public float maxHealth = 50f;
    [System.NonSerialized] public float health;
    [SerializeField] protected HealthBar healthBar = null;

    public float difficulty = 0f;
    public float damage = 10f;

    protected void Start()
    {
        health = maxHealth;
    }

    new void Update()
    {
        base.Update();
    }

    public void TakeDamage(float damage, GameObject source, float knockback)
    {
        health -= damage;

        if (healthBar != null)
        {
            healthBar.percentRemaining = health / 50.0f;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Attack(Player player)
    {
        player.TakeDamage(damage, gameObject);
        player.GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position).normalized * 100f);
    }
}
