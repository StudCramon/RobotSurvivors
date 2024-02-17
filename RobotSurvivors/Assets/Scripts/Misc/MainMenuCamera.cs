using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newCameraPosition = transform.position;
        newCameraPosition.x += speed * Time.deltaTime;
        newCameraPosition.y -= speed * Time.deltaTime;
        transform.position = newCameraPosition;
    }
}
