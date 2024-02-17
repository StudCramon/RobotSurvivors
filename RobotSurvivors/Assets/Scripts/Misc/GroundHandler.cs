using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHandler : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject tile;
    float distanceToSpawnNewTileX;
    float distanceToSpawnNewTileY;
    float distanceToSpawnDiagTile;
    float tileWidth;
    float tileHeight;

    bool rightSideHasSpawned = false;
    bool leftSideHasSpawned = false;
    bool upperSideHasSpawned = false;
    bool lowerSideHasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        tileWidth = GetComponentInChildren<SpriteRenderer>().size.x;
        tileHeight = GetComponentInChildren<SpriteRenderer>().size.y;
        distanceToSpawnNewTileX =  tileWidth/2;
        distanceToSpawnNewTileY = tileHeight/2;
        distanceToSpawnDiagTile = Mathf.Sqrt(distanceToSpawnNewTileX * distanceToSpawnNewTileX + distanceToSpawnNewTileY * distanceToSpawnNewTileY);
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.transform.position.x - this.transform.position.x > distanceToSpawnNewTileX && !rightSideHasSpawned)
        {
            Vector3 spawnPoint = transform.position;
            spawnPoint.x += tileWidth;
            Instantiate(tile, spawnPoint, tile.transform.rotation);
            rightSideHasSpawned = true;
        }

        if (mainCamera.transform.position.x - this.transform.position.x < -distanceToSpawnNewTileX && !leftSideHasSpawned)
        {
            Vector3 spawnPoint = transform.position;
            spawnPoint.x -= tileWidth;
            Instantiate(tile, spawnPoint, tile.transform.rotation);
            leftSideHasSpawned = true;
        }

        if (mainCamera.transform.position.y - this.transform.position.y > distanceToSpawnNewTileY && !upperSideHasSpawned)
        {
            Vector3 spawnPoint = transform.position;
            spawnPoint.y += tileHeight;
            Instantiate(tile, spawnPoint, tile.transform.rotation);
            upperSideHasSpawned = true;
        }

        if (mainCamera.transform.position.y - this.transform.position.y < -distanceToSpawnNewTileY && !lowerSideHasSpawned)
        {
            Vector3 spawnPoint = transform.position;
            spawnPoint.y -= tileHeight;
            Instantiate(tile, spawnPoint, tile.transform.rotation);
            lowerSideHasSpawned = true;
        }

        if (Vector3.Distance(transform.position, mainCamera.transform.position) > 200.0f)
        {
            Destroy(gameObject);
        }
    }
}
