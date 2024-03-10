using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timer = 0.0f;
    float minutes = 0.0f;
    float seconds = 0.0f;
    TextMeshProUGUI textMeshPro;
    string minutesPart;
    string secondsPart;

    Player player;
    // Start is called before the first frame update

    public static Timer instance;

    public delegate void HandleMinuteDelta();
    public event HandleMinuteDelta onMinuteDelta;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player = Transform.FindAnyObjectByType<Player>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            timer += Time.deltaTime;
            int nextMinute = Mathf.FloorToInt(timer / 60);
            if(nextMinute != minutes)
            {
                onMinuteDelta?.Invoke();
            }
            minutes = nextMinute;
            seconds = (int)(timer % 60);
            if (seconds < 10)
            {
                secondsPart = "0" + seconds.ToString();
            }
            else
            {
                secondsPart = seconds.ToString();
            }

            if (minutes < 10)
            {
                minutesPart = "0" + minutes.ToString();
            }
            else
            {
                minutesPart = minutes.ToString();
            }

            textMeshPro.text = minutesPart + ":" + secondsPart;
        }
    }
}
