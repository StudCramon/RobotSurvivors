using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : DestroyableObject
{
    [SerializeField] GameInput gameInput;

    [SerializeField] float speed = 100.0f;
    [SerializeField] float drag = 1.0f;
    [SerializeField] float armorRating = 0.0f;

    [SerializeField] int currentExp = 0;
    [SerializeField] int expToLevelUp = 4;
    [SerializeField] int level = 1;

    Rigidbody2D plyrRgBd2D;

    SpriteRenderer sprite;

    public AttackHandler attackHandler;

    Vector3 moveDir;
    Vector3 attackDir = Vector3.right;

    float maxHealth;
    float currentHealth;
    float invincibilityWindow = 1.0f;
    float knockbackMultiplier = 500.0f;
    float pointerSpeed = 5.0f;

    bool isInvincible = false;
    bool enableInvincibility = false;

    public override float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public override float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float ArmorRating { get => armorRating; set => armorRating = value; }

    public float Speed { get => speed; set => speed = value; }
    public float InvincibilityWindow { get => invincibilityWindow; set => invincibilityWindow = value; }

    public int CurrentExp { get => currentExp; }
    public int ExpToLevelUp { get => expToLevelUp; }
    public int CurrentLevel { get => level; }

    public delegate void LevelUpHandler();
    public event LevelUpHandler onLevelUp;

    public delegate void DestructionHandler();
    public event LevelUpHandler onDestruction;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 10;
        currentHealth = maxHealth;
        plyrRgBd2D = GetComponent<Rigidbody2D>();
        plyrRgBd2D.drag = drag;
        sprite = GetComponentInChildren<SpriteRenderer>();
        attackHandler = GetComponent<AttackHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAttackDirection();
        UpdateStatus();
        TEMPORARYSPRITETURNFUNCTION();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void OnEnable()
    {
        ExperienceHandler.instance.onGainExperience += GainExperience;
    }

    void OnDisable()
    {
        ExperienceHandler.instance.onGainExperience -= GainExperience;
    }

    void GainExperience(int amount)
    {
        currentExp += amount;
        if(currentExp >= expToLevelUp)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentExp -= expToLevelUp;
        expToLevelUp = (int)(expToLevelUp * level * 1.25f);
        level++;
        onLevelUp?.Invoke();
    }

    public override Quaternion GetAttackDirection()
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg);
    }

    void HandleMovement()
    {
        if(plyrRgBd2D.velocity.magnitude < speed)
        {
            moveDir = gameInput.GetMovementVectorNormalized();
            plyrRgBd2D.AddForce(moveDir * speed);
        }
    }

    void TEMPORARYSPRITETURNFUNCTION() //this needs to be removed from here somwhere else
    {
        if (moveDir.x > 0)
        {
            sprite.flipX = false;
        }
        else if (moveDir.x < 0)
        {
            sprite.flipX = true;
        }
    }

    void UpdateAttackDirection()
    {
        if (moveDir != Vector3.zero)
        {
            if (gameInput.GetInstantAttackDirectionCommand() == 0.0f)
            {
                attackDir = RotateVectorToTarget(attackDir, moveDir);
            }
            else
            {
                attackDir = moveDir;
            }
            
        }
    }

    Vector3 RotateVectorToTarget(Vector3 sourceVector, Vector3 targetVector)
    {
        Quaternion rotationQuaternion;
        Vector3 rotatedVector;
        float angle = pointerSpeed * Time.deltaTime;

        sourceVector.Normalize();
        targetVector.Normalize();

        Vector3 axisOfRotation = Vector3.Cross(sourceVector, targetVector);

        if (axisOfRotation == Vector3.zero && sourceVector != targetVector)
        {
            axisOfRotation = Vector3.forward;
        }

        rotationQuaternion = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, axisOfRotation);

        rotatedVector = rotationQuaternion * sourceVector;

        return rotatedVector;
    }

    void UpdateStatus()
    {
        if(currentHealth <= 0)
        {
            Die();
        }

        if(enableInvincibility)
        {
            enableInvincibility = false;
            StartCoroutine(EnableInvincibility(invincibilityWindow));
        }
    }

    void Die()
    {
        AudioManager.instance.PlaySound(SoundNames.EXPLOSION);
        onDestruction?.Invoke();
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            AudioManager.instance.PlaySound(SoundNames.HITSOUND);
            GetKnockedBack(collision.gameObject);
        }
    }

    override public void LoseHealth(float amount)
    {
        if(!isInvincible)
        {
            amount = amount - armorRating;
            if(amount < 1.0f)
            {
                amount = 1.0f;
            }
            currentHealth -= amount;
            enableInvincibility = true;
        }
    }

    IEnumerator EnableInvincibility(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    override public void GetKnockedBack(GameObject fromWho)
    {
        Vector3 knockback = (transform.position - fromWho.transform.position) * knockbackMultiplier;
        plyrRgBd2D.AddForce(knockback);
    }
}
