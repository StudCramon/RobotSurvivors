using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffArmor : AbstractBuff
{
    [SerializeField] Sprite sprite;
    public override Sprite Sprite { get => sprite; set => sprite = value; }

    string buffPrompt = "Increase Armor";

    public override string BuffPrompt { get => buffPrompt; set => buffPrompt = value; }

    public override void Buff()
    {
        Player player = FindObjectOfType<Player>();
        player.ArmorRating += 1;
    }
}
