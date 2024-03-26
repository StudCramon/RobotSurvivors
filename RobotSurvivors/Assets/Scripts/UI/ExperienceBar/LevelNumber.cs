using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelNumber : MonoBehaviour
{
    Player player;
    TextMeshProUGUI levelText;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        levelText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = player.CurrentLevel.ToString();
    }
}
