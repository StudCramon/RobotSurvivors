using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericAttack 
{
    public abstract float CoolDown { get; set; }
    public abstract bool ReadyToFire{ get; set; }
    public abstract void ExecuteAttack(Vector3 position, Quaternion direction, DestroyableObject owner);
    public abstract DestroyableObject AttackOwner { get; set; }
}
