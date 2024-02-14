using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffInvincWindow : AbstractBuff
{
    string buffPrompt = "Increase Invincibility on damage";

    public override string BuffPrompt { get => buffPrompt; set => buffPrompt = value; }

    public override void Buff()
    {
        Player player = FindObjectOfType<Player>();
        player.InvincibilityWindow = player.InvincibilityWindow * 1.10f;
    }
}
