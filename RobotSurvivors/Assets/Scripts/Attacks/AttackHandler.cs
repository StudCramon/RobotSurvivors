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
    float delayBetweenAttacks = 0.1f;

    void Start()
    {
        attacks = new List<GenericAttack>(); //=============REFACTOR============================
        this.AddAttack(new CircularSawLauncher(attackHolder));    //=================THIS==========================
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
                attackItem.LevelUp();
                return;
            }
        }
        attack.AttackOwner = attackHolder;
        pendingAttacks.Add(attack);
    }

    void TransferPendingAttacks()
    {
        foreach(GenericAttack attack in pendingAttacks)
        {
            attacks.Add(attack);
        }

        pendingAttacks.Clear();

        //foreach (GenericAttack attack in attacks)
        //{
        //    attack.ReadyToFire = true;
        //}
    }

    IEnumerator AttacksWithDelay(float delay)
    {
        coroutineIsRunning = true;
        for (int i = 0; i < attacks.Count; i++)
        {
            attacks[i].ExecuteAttack();
        }

        if (pendingAttacks.Count > 0)
        {
            TransferPendingAttacks();
        }
        yield return new WaitForSeconds(delay);
        coroutineIsRunning = false;
    }
}
