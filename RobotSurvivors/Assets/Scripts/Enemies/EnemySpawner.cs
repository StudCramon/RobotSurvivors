using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemy;

    float outsideCoordinateX = 45.0f;
    float outsideCoordinateY = 20.0f;
    float distanceFromEdge = 5.0f;
    float coolDown = 3.0f;

    bool readyToSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToSpawn)
        {
            readyToSpawn = false;
            StartCoroutine(SpawnEnemyWithCoolDown(coolDown));
        }
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
        GameObject enmy = Instantiate(enemy[0]);
        enmy.transform.position = RandomCoordinatesOutsideOfView();
    }

    int RandomSign()
    {
        return Random.value > 0.5 ? 1 : -1;
    }

    Vector3 RandomCoordinatesOutsideOfView()
    {
        Vector3 cameraPosition = FindAnyObjectByType<CameraScript>().transform.position;
        float xCoord = 0.0f;
        float yCoord = 0.0f;
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
}
