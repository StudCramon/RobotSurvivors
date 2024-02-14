using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PelletShooter : GenericAttack
{
    [SerializeField] float coolDown;
    [SerializeField] PelletSO pellet;
    
    DestroyableObject attackOwner;
    bool readyToFire;

    public override float CoolDown { get => coolDown; set => coolDown = value; }
    public override bool ReadyToFire { get => readyToFire; set => readyToFire = value; }
    public override DestroyableObject AttackOwner { get => attackOwner; set => attackOwner = value; }

    public PelletShooter()
    {
        coolDown = 1.0f;
        readyToFire = true;
        pellet = Resources.Load("ScriptableObjects/Pellets/BasicPellet") as PelletSO;
    }

    override public void ExecuteAttack(Vector3 position, Quaternion direction, DestroyableObject owner)
    {
        if (readyToFire)
        {
            pellet.prefab.GetComponent<Pellet>().owner = owner;
            MonoBehaviour.Instantiate(pellet.prefab, position, direction);
            readyToFire = false;
        }
    }
}
