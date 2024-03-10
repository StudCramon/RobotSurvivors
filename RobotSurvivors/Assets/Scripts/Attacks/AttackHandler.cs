using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] List<GenericAttack> attacks;
    [SerializeField] DestroyableObject attackHolder;

    List<GenericAttack> pendingAttacks = new List<GenericAttack>();

    bool coroutineIsRunning = false;
    float delayBetweenAttacks = 0.2f;

    void Start()
    {
        attacks = new List<GenericAttack>(); //=============REFACTOR============================
        attacks.Add(new PelletShooter());    //=================THIS==========================
    }

    // Update is called once per frame
    void Update()
    {
        if (!coroutineIsRunning)
        {
            StartCoroutine(AttacksWithDelay(delayBetweenAttacks));
        }
    }

    public void AddAttack(GenericAttack attack)
    {
        foreach(GenericAttack attackItem in attacks)
        {
            if(attackItem.GetType() == attack.GetType())
            {
                //levelUpAttack
                return;
            }
        }
        pendingAttacks.Add(attack);
    }

    void TransferPendingAttacks()
    {
        foreach(GenericAttack attack in pendingAttacks)
        {
            attacks.Add(attack);
        }

        pendingAttacks.Clear();

        foreach (GenericAttack attack in attacks)
        {
            attack.ReadyToFire = true;
        }
    }

    public void AddPelletShooter()
    {
        pendingAttacks.Add(new PelletShooter());
    }

    IEnumerator WaitForCoolDown(int attackIndex)
    {
        yield return new WaitForSeconds(attacks[attackIndex].CoolDown);
        attacks[attackIndex].ReadyToFire = true;
    }

    IEnumerator AttacksWithDelay(float delay)
    {
        coroutineIsRunning = true;
        for (int i = 0; i < attacks.Count; i++)
        {
            if (attacks[i].ReadyToFire)
            {
                yield return new WaitForSeconds(delay);
                attacks[i].ExecuteAttack(attackHolder.transform.position, attackHolder.GetAttackDirection(), attackHolder);
                AudioManager.instance.PlaySound(SoundNames.LASERSHOT);
                StartCoroutine(WaitForCoolDown(i));
            }
        }

        if (pendingAttacks.Count > 0)
        {
            TransferPendingAttacks();
        }
        yield return new WaitForSeconds(delay * 5.0f);
        coroutineIsRunning = false;
    }
}
