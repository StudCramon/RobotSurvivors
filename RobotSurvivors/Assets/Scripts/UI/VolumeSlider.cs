using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        slider = GetComponent<Slider>();
        slider.value = AudioManager.instance.currentValue;
        slider.onValueChanged.AddListener(AudioManager.instance.ChangeVolume);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
