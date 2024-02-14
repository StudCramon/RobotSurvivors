using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHealth : AbstractBuff
{
    string buffPrompt = "Increase Health";

    public override string BuffPrompt { get => buffPrompt; set => buffPrompt = value; }

    public override void Buff()
    {
        Player player = FindObjectOfType<Player>();
        player.MaxHealth = player.MaxHealth * 1.10f;
        player.CurrentHealth = player.MaxHealth;

    }
}
