using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeed : AbstractBuff
{
    string buffPrompt = "Increase Speed";

    public override string BuffPrompt { get => buffPrompt; set => buffPrompt = value; }

    public override void Buff()
    {
        Player player = FindObjectOfType<Player>();
        player.Speed = player.Speed * 1.10f;
    }
}
