using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuffsPanel : MonoBehaviour
{
    [SerializeField] AbstractBuff[] buffs;

    private void OnEnable()
    {
        Time.timeScale = 0;
        int randomIndex;
        List<int> availableIndexes = new List<int>();
        for (int i = 0; i < buffs.Length; ++i)
        {
            availableIndexes.Add(i);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            randomIndex = Random.Range(0, availableIndexes.Count);

            child.GetComponentInChildren<TextMeshProUGUI>().text = buffs[availableIndexes[randomIndex]].BuffPrompt;
            child.GetComponent<Button>().onClick.AddListener(ClosePanel);
            child.GetComponent<Button>().onClick.AddListener(buffs[availableIndexes[randomIndex]].Buff);

            availableIndexes.RemoveAt(randomIndex);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        Time.timeScale = 1;    
    }

    void ClosePanel()
    {
        gameObject.SetActive(false);
        ExperienceHandler.instance.AddExperience(0);
    }
}
