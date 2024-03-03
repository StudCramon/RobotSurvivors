using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;

    public void ToggleOptionsMenu()
    {
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
    }
}
