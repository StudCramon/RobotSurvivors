using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSawLauncher : GenericAttack
{
    [SerializeField] float coolDown = 5.0f;
    [SerializeField] GameObject projectilePrefab;
    int currentLevel = 1;
    int numberOfProjectiles = 1;
    float damage = 1.0f;

    DestroyableObject attackOwner;
    bool readyToFire = true;

    public float CoolDown { get => coolDown; set => coolDown = value; }
    public bool ReadyToFire { get => readyToFire; set => readyToFire = value; }
    public DestroyableObject AttackOwner { get => attackOwner; set => attackOwner = value; }

    public CircularSawLauncher(DestroyableObject attackOwner)
    {
        this.attackOwner = attackOwner;
        projectilePrefab = Resources.Load("Prefabs/Projectiles/CircularSawProjecile") as GameObject;
        if(projectilePrefab == null)
        {
            Debug.LogError("projectilePrefab = null");
        }
    }

    public void ExecuteAttack()
    {
        if (attackOwner == null)
        {
            Debug.LogError("No attack owner");
            return;
        }

        if (readyToFire)
        {
            attackOwner.StartCoroutine(AttackWithDelay(0.5f));
        }
    }

    IEnumerator AttackWithDelay(float seconds)
    {
        readyToFire = false;
        projectilePrefab.GetComponent<CircularSaw>().owner = attackOwner;
        GameObject tempPellet = MonoBehaviour.Instantiate(projectilePrefab, attackOwner.transform.position, attackOwner.GetAttackDirection());
        tempPellet.GetComponent<CircularSaw>().damage = damage;
        tempPellet.GetComponent<CircularSaw>().counter = 2 * Mathf.PI / numberOfProjectiles;

        for (int i = 0; i < numberOfProjectiles - 1; ++i)
        {
            //yield return new WaitForSeconds(seconds);
            projectilePrefab.GetComponent<CircularSaw>().owner = attackOwner;
            tempPellet = MonoBehaviour.Instantiate(projectilePrefab, attackOwner.transform.position, attackOwner.GetAttackDirection());
            tempPellet.GetComponent<CircularSaw>().damage = damage;
            tempPellet.GetComponent<CircularSaw>().counter = (2 * Mathf.PI / numberOfProjectiles) * (i + 2);
        }
        yield return new WaitForSeconds(coolDown + tempPellet.GetComponent<CircularSaw>().timeOfLife);
        readyToFire = true;
    }

    public void LevelUp()
    {
        switch (++currentLevel)
        {
            case 2:
                ++numberOfProjectiles;
                break;
            case 3:
                damage += 1.0f;
                break;
            case 4:
                ++numberOfProjectiles;
                break;
            case 5:
                coolDown /= 2;
                break;
            default:
                Debug.Log("Current Level: " + currentLevel);
                break;
        }
    }
}
