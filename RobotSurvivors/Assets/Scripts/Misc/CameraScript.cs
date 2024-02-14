using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]GameObject playerRef;
    [SerializeField] GameInput gameInput;
    Vector3 cameraPosition;
    float cameraSpeed = 10.0f;
    float cameraDrag = 10.0f;
    float cameraSize;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.Find("Player");
        GetComponent<Rigidbody2D>().drag = cameraDrag;
        cameraSize = GetComponent<Camera>().orthographicSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowGameObject(playerRef);
    }

    void Update()
    {
        HandleZoomCamera();
    }

    void FollowGameObject(GameObject objectToFollow)
    {
        if (objectToFollow != null)
        {
            cameraPosition = objectToFollow.transform.position;
            cameraPosition.z = -10;
            
            if(transform.position != cameraPosition)
            {
                transform.position += (cameraPosition - transform.position) / (cameraSpeed);
            }
        }
    }

    void HandleZoomCamera()
    {
        GetComponent<Camera>().orthographicSize += -gameInput.GetZoom() * Time.deltaTime;

        if (GetComponent<Camera>().orthographicSize < cameraSize / 2)
        {
            GetComponent<Camera>().orthographicSize = cameraSize / 2;
        }
        else if (GetComponent<Camera>().orthographicSize > cameraSize)
        {
            GetComponent<Camera>().orthographicSize = cameraSize;
        }
    }
}
