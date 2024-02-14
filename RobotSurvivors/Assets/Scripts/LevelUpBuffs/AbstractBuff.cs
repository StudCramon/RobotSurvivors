using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBuff : MonoBehaviour
{
    public abstract string BuffPrompt { get; set; }
    public abstract void Buff();
    public virtual void SendToReciever()
    {
        FindObjectOfType<Player>().onLevelUp += Buff;
    }

}
