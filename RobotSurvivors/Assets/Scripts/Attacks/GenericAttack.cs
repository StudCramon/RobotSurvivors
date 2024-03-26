using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GenericAttack 
{
    public abstract float CoolDown { get; set; }
    public abstract bool ReadyToFire{ get; set; }
    public abstract void ExecuteAttack();
    public abstract DestroyableObject AttackOwner { get; set; }
    public abstract void LevelUp();
}
