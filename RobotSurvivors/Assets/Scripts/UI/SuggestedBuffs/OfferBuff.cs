using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfferBuff : MonoBehaviour
{
    Player player;
    BuffsPanel buffsPanel;

    // Start is called before the first frame update
    void Start()
    {
        buffsPanel = GetComponentInChildren<BuffsPanel>();
        buffsPanel.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.onLevelUp += Enable;
    }

    void OnDisable()
    {
        player.onLevelUp -= Enable;
    }

    void Enable()
    {
        buffsPanel.gameObject.SetActive(true);
    }
}
