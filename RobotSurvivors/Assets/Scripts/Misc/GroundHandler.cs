using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundHandler : MonoBehaviour
{
    [SerializeField] GameObject tile;
    GameObject mainCamera;

    float tileWidth; //length on x
    float tileHeight; // length on y
    float offset = 50.0f;
    float distanceToDestroyTiles = 50.0f;

    Dictionary<Vector3, Object> tiles = new Dictionary<Vector3, Object>();

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        tileWidth = tile.gameObject.GetComponentInChildren<SpriteRenderer>().size.x;
        tileHeight = tile.gameObject.GetComponentInChildren<SpriteRenderer>().size.y;
    }

    // Update is called once per frame
    void Update()
    {
        tiles.EnsureCapacity(1);
        Vector3[] pointsOfCheck = new Vector3[4];
        pointsOfCheck[0] = new Vector3(offset, offset, 0);
        pointsOfCheck[1] = new Vector3(-offset, offset, 0);
        pointsOfCheck[2] = new Vector3(offset, -offset, 0);
        pointsOfCheck[3] = new Vector3(-offset, -offset, 0);

        for(int i = 0; i < 4; i++)
        {
            pointsOfCheck[i].x += mainCamera.transform.position.x;
            pointsOfCheck[i].y += mainCamera.transform.position.y;

            Vector3 newTilePosition = GetTilePosOnPoint(pointsOfCheck[i]);
            AddTileOnPosition(newTilePosition);
        }

        Debug.DrawLine(pointsOfCheck[0], pointsOfCheck[1], Color.red);
        Debug.DrawLine(pointsOfCheck[1], pointsOfCheck[2], Color.yellow);
        Debug.DrawLine(pointsOfCheck[2], pointsOfCheck[3], Color.green);
        Debug.DrawLine(pointsOfCheck[3], pointsOfCheck[0], Color.blue);

        DestroyDistantTiles();
    }

    void DestroyDistantTiles()
    {
        Vector3 KeyToRemove = new Vector3();
        bool ShouldRemoveTile = false;
        foreach (KeyValuePair<Vector3, Object> tile in tiles)
        {
            if (Vector3.Distance(tile.Key, mainCamera.transform.position) > 500.0f)
            {
                ShouldRemoveTile = true;
                KeyToRemove = tile.Key;
                break;
            }
        }

        if (ShouldRemoveTile)
        {
            Object objectRef;
            tiles.TryGetValue(KeyToRemove, out objectRef);
            tiles.Remove(KeyToRemove);
            Destroy(objectRef);
        }
    }

    Vector3 GetTilePosOnPoint(Vector3 point)
    {
        int TilePosXScalar;
        int TilePosYScalar;

        float TilePosXScalarPrep = point.x / tileWidth;
        float TilePosYScalarPrep = point.y / tileHeight;

        if (TilePosXScalarPrep < 0)
        {
            TilePosXScalar = (int)TilePosXScalarPrep - 1;
        }
        else
        {
            TilePosXScalar = (int)TilePosXScalarPrep;
        }

        if (TilePosYScalarPrep < 0)
        {
            TilePosYScalar = (int)TilePosYScalarPrep - 1;
        }
        else
        {
            TilePosYScalar = (int)TilePosYScalarPrep;
        }

        float tileX = tileWidth * TilePosXScalar;
        float tileY = tileHeight * TilePosYScalar;
        float tileZ = 0;
        Vector3 newTilePosition = new Vector3(tileX, tileY, tileZ);
        //Debug.Log(newTilePosition);
        return newTilePosition;
    }

    void AddTileOnPosition(Vector3 pos)
    {
        if (!tiles.ContainsKey(pos))
        {
            tiles.Add(pos, Instantiate(tile, pos, tile.transform.rotation));
        }
    }
}
