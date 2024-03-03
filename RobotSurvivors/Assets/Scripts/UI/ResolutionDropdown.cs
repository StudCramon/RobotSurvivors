using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionDropdown : MonoBehaviour
{
    Resolution[] resolutions;
    TMP_Dropdown dropdown;
    static public List<Vector2> resolutionNumbers = new List<Vector2>();
    // Start is called before the first frame update
    void OnEnable()
    {
        if(Screen.currentResolution.width < 320 && Screen.currentResolution.height < 240)
        {
            Screen.SetResolution(320, 240, false);
        }
        resolutions = Screen.resolutions;
        dropdown = GetComponentInChildren<TMP_Dropdown>();
        for(int i = 0; i < resolutions.Length; ++i)
        {
            if (i != 0)
            {
                if(resolutions[i].width == resolutions[i-1].width && resolutions[i].height == resolutions[i - 1].height)
                {
                    continue;
                }
            }
            string text = resolutions[i].width + "x" + resolutions[i].height;
            //resolutionNumbers[i] = new Vector2(resolutions[i].width, resolutions[i].height);
            resolutionNumbers.Add(new Vector2(resolutions[i].width, resolutions[i].height));
            TMPro.TMP_Dropdown.OptionData data = new TMPro.TMP_Dropdown.OptionData(text);
            dropdown.options.Add(data);
        }

        Vector2 currentResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        for(int i = 0; i < resolutionNumbers.Count; ++i)
        {
            if (resolutionNumbers[i] == currentResolution)
            {
                GetComponentInChildren<TMP_Dropdown>().value = i;
                break;
            }
        }
    }
}
