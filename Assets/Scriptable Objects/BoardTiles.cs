using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "Scriptable Objects/Board")]
public class BoardTiles : ScriptableObject
{
    [Header("Tile Properties")]
    public List<Tile> tiles;
    public float Offset;
    public int SideLength = 8;
    [Space]
    [Header("Animation")]
    public float startAnimationDelay = 1f;
    public float jumpAnimationDuration = 1f;
    
    private GameObject[,] tileObjs;


    public void SetupBoard()
    {
        tileObjs = new GameObject[SideLength, SideLength];
        for (int i = 0; i < SideLength; i++)
        {
            for (int j = 0; j < SideLength; j++)
            {
                int randomTileKey = Random.Range(0, tiles.Count-1);
                var tile = tiles[randomTileKey].GetTile();
                tileObjs[i, j] = tile;
            }
        }
    }

    public GameObject[,] GetTiles()
    {
        return tileObjs;
    }
}
