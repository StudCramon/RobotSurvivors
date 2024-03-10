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
    float distanceToDestroyTiles = 350.0f;

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
        Vector3[] pointsOfCheck;

        GetPointsOutsideOfCameraView(out pointsOfCheck);

        for (int i = 0; i < 4; i++)
        {
            Vector3 newTilePosition = GetTilePosOnPoint(pointsOfCheck[i]);
            AddTileOnPosition(newTilePosition);
        }

        DestroyDistantTiles();
    }

    void DestroyDistantTiles()
    {
        Vector3 KeyToRemove = new Vector3();
        bool ShouldRemoveTile = false;
        foreach (KeyValuePair<Vector3, Object> tile in tiles)
        {
            if (Vector3.Distance(tile.Key, mainCamera.transform.position) > distanceToDestroyTiles)
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
        return newTilePosition;
    }

    void AddTileOnPosition(Vector3 pos)
    {
        if (!tiles.ContainsKey(pos))
        {
            tiles.Add(pos, Instantiate(tile, pos, tile.transform.rotation));
        }
    }

    void GetPointsOutsideOfCameraView(out Vector3[] vector)
    {
        Camera camera = mainCamera.GetComponent<Camera>();
        vector = new Vector3[4];
        vector[0] = camera.ViewportToWorldPoint(new Vector3(-0.5f, -0.5f, camera.nearClipPlane));
        vector[1] = camera.ViewportToWorldPoint(new Vector3(-0.5f, 1.5f, camera.nearClipPlane));
        vector[2] = camera.ViewportToWorldPoint(new Vector3(1.5f, -0.5f, camera.nearClipPlane));
        vector[3] = camera.ViewportToWorldPoint(new Vector3(1.5f, 1.5f, camera.nearClipPlane));

        Debug.DrawLine(vector[0], vector[1]);
        Debug.DrawLine(vector[1], vector[2]);
        Debug.DrawLine(vector[2], vector[3]);
        Debug.DrawLine(vector[3], vector[0]);
    }
}
