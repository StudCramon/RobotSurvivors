using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : DestroyableObject
{
    [SerializeField] float speed = 100.0f;
    [SerializeField] float drag = 10.0f;

    GameObject player;
    Rigidbody2D enmRB2D;

    float maxHealth = 1.0f;
    float currentHealth;
    float knockbackMultiplier = 25.0f;
    float contactDamage = 1.0f;
    int expPoints = 1;

    public Vector3 currentDirection { get; private set; }
    public override float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public override float CurrentHealth { get => currentHealth; set => currentHealth = value; }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Destroy(this.gameObject);
        }
        currentHealth = maxHealth;
        enmRB2D = GetComponent<Rigidbody2D>();
        enmRB2D.drag = drag;
    }

    // Update is called once per frame
    void Update()
    {
        HandleStatus();
    }

    void FixedUpdate()
    {
        HandleMovement();
        TEMPORARYSPRITETURNFUNCTION();
    }

    void HandleMovement()
    {
        if (enmRB2D.velocity.magnitude < speed && player != null)
        {
            currentDirection = player.transform.position - transform.position;
            enmRB2D.AddForce(currentDirection.normalized * speed);
        }
    }

    void HandleStatus()
    {
        if (player == null)
        {
            Destroy(this.gameObject);
        }
        else if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        ExperienceHandler.instance.AddExperience(expPoints);
        Destroy(this.gameObject);
    }

    void TEMPORARYSPRITETURNFUNCTION() //this needs to be removed from here somwhere else
    {
        if (currentDirection.x > 0)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else if (currentDirection.x < 0)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            GetKnockedBack(player);
            enmRB2D.AddForce(-enmRB2D.velocity);
            collision.gameObject.GetComponent<Player>().LoseHealth(contactDamage);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            enmRB2D.AddForce(-enmRB2D.velocity);
        }
    }

    public override Quaternion GetAttackDirection()
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg);
    }

    public override void GetKnockedBack(GameObject fromWho)
    {
        Vector3 knockback = (transform.position - fromWho.transform.position) * knockbackMultiplier;
        GetComponent<Rigidbody2D>().AddForce(knockback);
    }

    public override void LoseHealth(float amount)
    {
        currentHealth -= amount;
    }
}
