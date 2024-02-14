using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExperienceHandler : MonoBehaviour
{
    public static ExperienceHandler instance;

    public delegate void HandleExperience(int experience);
    public event HandleExperience onGainExperience;

    public void AddExperience(int amount)
    {
        onGainExperience?.Invoke(amount);
    }

    ExperienceHandler()
    {
        /*if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }*/

        if(instance == null)
        {
            instance = this;
        }
    }
}
