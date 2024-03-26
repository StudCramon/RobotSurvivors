using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCircularSaw : AbstractBuff
{
    [SerializeField] Sprite sprite;
    public override Sprite Sprite { get => sprite; set => sprite = value; }
    string buffPrompt = "Add Circular Saw";

    public override string BuffPrompt { get => buffPrompt; set => buffPrompt = value; }

    public override void Buff()
    {
        Player player = FindObjectOfType<Player>();
        player.attackHandler.AddAttack(new CircularSawLauncher(player));
    }
}
