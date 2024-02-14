using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestroyableObject : MonoBehaviour
{
    public abstract float CurrentHealth { get; set; }
    public abstract float MaxHealth { get; set; }
    public abstract Quaternion GetAttackDirection();
    public abstract void LoseHealth(float amount);
    public abstract void GetKnockedBack(GameObject fromWho);
}
