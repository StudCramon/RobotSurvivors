using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObtainedAttacksPanel : MonoBehaviour
{
    [SerializeField] Image[] images;

    public void AddImageToUI(Sprite img)
    {
        for(int i = 0; i < images.Length; ++i)
        {
            if(images[i].sprite == img)
            {
                break;
            }
            if(images[i].sprite == null)
            {
                images[i].sprite = img;
                images[i].color = Color.white;
                break;
            }
        }
    }
}
