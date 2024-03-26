using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBuff : MonoBehaviour
{
    public abstract Sprite Sprite { get; set; }
    public abstract string BuffPrompt { get; set; }
    public abstract void Buff();
    public virtual void SendToReciever()
    {
        FindObjectOfType<Player>().onLevelUp += Buff;
    }

    public virtual void UpdateAttacksUI()
    {
        ObtainedAttacksPanel panel = GameObject.FindAnyObjectByType<ObtainedAttacksPanel>();
        panel.AddImageToUI(Sprite);
    }
}
