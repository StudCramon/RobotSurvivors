using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class PelletShooter : GenericAttack
{
    [SerializeField] float coolDown = 1.0f;
    [SerializeField] PelletSO pellet;
    int currentLevel = 1;
    int numberOfProjectiles = 1;
    float damage = 1.0f;
    
    DestroyableObject attackOwner;
    bool readyToFire = true;

    public float CoolDown { get => coolDown; set => coolDown = value; }
    public bool ReadyToFire { get => readyToFire; set => readyToFire = value; }
    public DestroyableObject AttackOwner { get => attackOwner; set => attackOwner = value; }

    public PelletShooter(DestroyableObject attackOwner)
    {
        this.attackOwner = attackOwner;
        pellet = Resources.Load("ScriptableObjects/Pellets/BasicPellet") as PelletSO;
    }

    public void ExecuteAttack()
    {
        if(attackOwner == null)
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
        pellet.prefab.GetComponent<Pellet>().owner = attackOwner;
        Transform tempPellet = MonoBehaviour.Instantiate(pellet.prefab, attackOwner.transform.position, attackOwner.GetAttackDirection());
        tempPellet.GetComponent<Pellet>().damage = damage;

        for (int i = 0; i < numberOfProjectiles - 1; ++i)
        {
            yield return new WaitForSeconds(seconds);
            pellet.prefab.GetComponent<Pellet>().owner = attackOwner;
            tempPellet = MonoBehaviour.Instantiate(pellet.prefab, attackOwner.transform.position, attackOwner.GetAttackDirection());
            tempPellet.GetComponent<Pellet>().damage = damage;
        }
        yield return new WaitForSeconds(coolDown);
        readyToFire = true;
    }

    public void LevelUp()
    {
        switch(++currentLevel)
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
