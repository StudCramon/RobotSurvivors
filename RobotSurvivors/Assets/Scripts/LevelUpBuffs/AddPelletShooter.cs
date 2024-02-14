using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPelletShooter : AbstractBuff
{
    string buffPrompt = "Add Pellet Shooter";

    public override string BuffPrompt { get => buffPrompt; set => buffPrompt = value; }

    public override void Buff()
    {
        Player player = FindObjectOfType<Player>();
        player.attackHandler.AddAttack(new PelletShooter());
    }
}
