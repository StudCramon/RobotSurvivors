using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemy;
    Camera mainCamera;

    float outsideCoordinateX = 45.0f;
    float outsideCoordinateY = 20.0f;
    float distanceFromEdge = 5.0f;
    float coolDown = 3.0f;

    bool readyToSpawn = true;

    private void OnDisable()
    {
        Timer.instance.onMinuteDelta -= IncreaseSpawnRate;
    }
    // Start is called before the first frame update
    void Start()
    {
        Timer.instance.onMinuteDelta += IncreaseSpawnRate;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToSpawn)
        {
            readyToSpawn = false;
            StartCoroutine(SpawnEnemyWithCoolDown(coolDown));
        }
        Debug.DrawLine(new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0.0f), new Vector3(outsideCoordinateX, outsideCoordinateY, 0.0f), color: Color.red);
        //Debug.DrawLine(new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0.0f), new Vector3(outsideCoordinateX, 0.0f, 0.0f), color: Color.red);
        //Debug.DrawLine(new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0.0f), new Vector3(0.0f, outsideCoordinateY, 0.0f), color: Color.red);
    }

    IEnumerator SpawnEnemyWithCoolDown(float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        SpawnEnemyAtRandomOutsideOfView();
        readyToSpawn = true;
    }

    void SpawnEnemy()
    {
        GameObject enmy = Instantiate(enemy[0]);
        enmy.transform.position = this.transform.position;
    }

    void SpawnEnemyAtRandomOutsideOfView()
    {
        GameObject enmy = Instantiate(enemy[Random.value > 0.5 ? 0 : 1]);
        enmy.transform.position = RandomCoordinatesOutsideOfView();
    }

    int RandomSign()
    {
        return Random.value > 0.5 ? 1 : -1;
    }

    Vector3 RandomCoordinatesOutsideOfView()
    {
        Vector3 cameraPosition = FindAnyObjectByType<CameraScript>().transform.position;
        float xCoord;
        float yCoord;

        Vector3 upperRightCornerCameraCoord = mainCamera.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, mainCamera.nearClipPlane));

        outsideCoordinateX = upperRightCornerCameraCoord.x - mainCamera.transform.position.x;
        outsideCoordinateY = upperRightCornerCameraCoord.y - mainCamera.transform.position.y;

        if (RandomSign() == 1)
        {
            xCoord = cameraPosition.x + Random.Range(outsideCoordinateX, outsideCoordinateX + distanceFromEdge) * RandomSign();
            yCoord = cameraPosition.y + Random.Range(0, outsideCoordinateY + distanceFromEdge) * RandomSign();
        }
        else
        {
            xCoord = cameraPosition.x + Random.Range(0, outsideCoordinateX + distanceFromEdge) * RandomSign();
            yCoord = cameraPosition.y + Random.Range(outsideCoordinateY, outsideCoordinateY + distanceFromEdge) * RandomSign();
        }

        return new Vector3(xCoord, yCoord, 0.0f);
    }

    void IncreaseSpawnRate()
    {
        if(coolDown > 1.0f)
        {
            coolDown -= 1.0f;
        }
        else
        {
            coolDown = 0.1f;
        }
        Debug.Log("SpawnRate is: " + coolDown);
    }
}
