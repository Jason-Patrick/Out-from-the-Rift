using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Tile", menuName = "Scriptable Objects/Board")]
public class BoardTiles : ScriptableObject
{
    [Header("Tile Properties")]
    public Tile baseTile;
    public List<Tile> tiles;
    public float Offset;
    public int SideLength = 8;

    [Space]

    [Header("Animation")]
    public float startAnimationDelay = 1f;
    public float jumpAnimationDuration = 1f;
    
    private GameObject[,] tileObjs;
    private Dictionary<Tile, float> tileFrequencies;


    public void SetupBoard()
    {
        tileObjs = new GameObject[SideLength, SideLength];
        GenerateRanges();
        PlaceTiles();
    }

    public void RandomlyPlaceTiles()
    {
        for (int i = 0; i < SideLength; i++)
        {
            for (int j = 0; j < SideLength; j++)
            {
                int randomTileKey = Random.Range(0, tiles.Count - 1);
                var tile = tiles[randomTileKey].tileObj;
                tileObjs[i, j] = tile;
            }
        }
    }

    public void GenerateRanges()
    {
        tileFrequencies = new Dictionary<Tile, float>();

        int totalTiles = SideLength * SideLength;
        float previousFreq = 0f;
        foreach (Tile tile in tiles)
        {
            float tileBoardRatio = (float)tile.countOnBoard / totalTiles;
            tileFrequencies.Add(tile, previousFreq + tileBoardRatio);
            previousFreq += tileBoardRatio;
        }

        // sort dict with ascending float range values
        tileFrequencies = (from entry in tileFrequencies orderby entry.Value ascending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public void PlaceTiles()
    {
        for (int i = 0; i < SideLength; i++)
        {
            for (int j = 0; j < SideLength; j++)
            {
                float noise = Mathf.PerlinNoise((float)i * 1f, (float)j * 1f);
                foreach (KeyValuePair<Tile, float> tileFreq in tileFrequencies)
                {
                    if (tileFreq.Value >= noise)
                    {
                        tileObjs[i, j] = tileFreq.Key.tileObj;
                        break;
                    }
                }
                if (!tileObjs[i,j])
                {
                    tileObjs[i, j] = baseTile.tileObj;
                }
            }
        }
    }

    public GameObject[,] GetTiles()
    {
        return tileObjs;
    }
}

[CustomEditor(typeof(BoardTiles))]
public class BoardTilesEditor : Editor
{

}