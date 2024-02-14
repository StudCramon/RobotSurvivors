using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversalHeathBar : MonoBehaviour
{
    DestroyableObject owner;
    Image bar, background;
    float ratio;
    // Start is called before the first frame update
    void Start()
    {
        owner = GetComponentInParent<DestroyableObject>();
        bar = GetComponentInChildren<Bar>().GetComponent<Image>();
        background = GetComponentInChildren<BackGround>().GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ratio = owner.CurrentHealth / owner.MaxHealth;
        if (ratio >= 1.0f)
        {
            bar.enabled = false;
            background.enabled = false;
        }
        else
        {
            bar.enabled = true;
            background.enabled = true;
            bar.fillAmount = ratio;
        }
     }
}
