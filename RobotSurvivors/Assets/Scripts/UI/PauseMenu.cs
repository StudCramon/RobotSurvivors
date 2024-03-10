using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameInput gameInput;
    // Start is called before the first frame update
    bool buttonIsBeingHold = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameInput.PauseMenuButtonPressed() && !buttonIsBeingHold)
        {
            if(pausePanel.activeInHierarchy)
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else if(!pausePanel.activeInHierarchy)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0.0f;
            }
            buttonIsBeingHold = true;
        }
        else if(!gameInput.PauseMenuButtonPressed())
        {
            buttonIsBeingHold = false;
        }
    }
}
