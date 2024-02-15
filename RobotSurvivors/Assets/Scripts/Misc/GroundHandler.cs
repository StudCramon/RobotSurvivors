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
        if(mainCamera.transform.position.x - this.transform.position.x > distanceToSpawnNewTileX * 3)
        {
            Destroy(gameObject);
        }
    }
}
