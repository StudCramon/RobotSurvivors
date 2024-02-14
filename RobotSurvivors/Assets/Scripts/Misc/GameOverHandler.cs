using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Player>().onDestruction += ActivateGameOverScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateGameOverScreen()
    {
        panel.SetActive(true);
    }
}
