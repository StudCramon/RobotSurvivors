using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    Player player;
    Image imgRef;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        imgRef = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        imgRef.fillAmount = (float)player.CurrentExp / (float)player.ExpToLevelUp;
    }
}
