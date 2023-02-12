using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Transform handRotationPoint;
    // public float maxVelocity = 1.5f;

    public Hand hand;
    public HealthBar healthBar;
    public Inventory inventory;

    public float maxHealth = 100f;
    public float health = 100f;

    public float acceleration = 10f;
    // public float decelleration = 20f;

    public float pickupRadius = 0.25f;

    public float damageHealingCooldown = 2f;
    public float healthRegen = 4f;

    private Vector2 movementVector = Vector2.zero;
    private float timeOfLastDamage = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float angle = Random.Range(0, 360*Mathf.Deg2Rad);

        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    void Update() {
        if (hand.weapon != inventory.currentWeapon)
        {
            hand.weapon = inventory.currentWeapon;
            for (int i = 0; i < inventory.weapons.Length; i++)
            {
                inventory.weapons[i].gameObject.SetActive(i == inventory.currentSlot);
            }
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementVector = new Vector2(horizontal, vertical);

        // healing if time since last damage is greater than cooldown
        if (Time.time - timeOfLastDamage > damageHealingCooldown && maxHealth > health)
        {
            health += Mathf.Min(healthRegen * Time.deltaTime, maxHealth - health);
            UpdateHealthBar();
        }

        if (movementVector.magnitude == 0f) movementVector = Vector2.zero;
    }

    void UpdateHealthBar()
    {
        healthBar.percentRemaining = health / maxHealth;
    }

    void FixedUpdate()
    {
        // handle firing
        if (Input.GetAxis("Fire1") > 0f && hand.weapon != null)
        {
            hand.weapon.fire(gameObject);
        }

        // handle movement
        // bool isDecellerating = Vector3.Dot(rb.velocity, movementVector) <= 0f;
        // if (movementVector.magnitude == 0f)
        // {
        //     movementVector = -rb.velocity.normalized;
        // }

        // rb.AddForce(movementVector * rb.mass * (isDecellerating ? decelleration : acceleration));
        rb.AddForce(movementVector * rb.mass * acceleration);

        // handle mouse
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        handRotationPoint.rotation = Quaternion.Euler(0f, 0f, rot_z);
        
        // flip the sprite if rot > 180
        handRotationPoint.localScale = new Vector3(1f, Mathf.Abs(rot_z) > 90f ? -1f : 1f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision with "+other.gameObject.name+" which has tag "+other.gameObject.tag);
        if (other.gameObject.tag == "Item")
        {
            Pickupabble item = other.gameObject.GetComponent<Pickupabble>();
            // Debug.Log("item: "+item.name);
            if (item != null)
            {
                item.Pickup(this);
            }
        } else  if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            // Debug.Log("enemy: "+enemy.name);
            if (enemy != null)
            {
                enemy.Attack(this);
            }
        }
    }

    public void takeDamage(float damage, GameObject source)
    {
        health -= damage;
        UpdateHealthBar();

        if (healthBar.percentRemaining <= 0)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            this.enabled = false;
        }

        timeOfLastDamage = Time.time;
    }
}
