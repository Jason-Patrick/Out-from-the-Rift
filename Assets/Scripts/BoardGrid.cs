using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoardGrid : MonoBehaviour
{
    [SerializeField] private BoardTiles boardTiles;
    private GameObject[,] board;

    private void Awake()
    {
        boardTiles.SetupBoard();
        board = new GameObject[boardTiles.SideLength, boardTiles.SideLength];
    }

    void Start()
    {
        Initialize();
        StartCoroutine(SetupAnimation());
    }

    private void Initialize()
    {
        var tiles = boardTiles.GetTiles();
        for (int i = 0; i < boardTiles.SideLength; i++)
        {
            for (int j = 0; j < boardTiles.SideLength; j++)
            {
                board[i, j] = Instantiate(tiles[i, j], new Vector3(i * boardTiles.Offset, tiles[i, j].transform.position.y, j * boardTiles.Offset), Quaternion.identity, transform);
            }
        }
    }

    private IEnumerator SetupAnimation()
    {
        yield return new WaitForSeconds(boardTiles.startAnimationDelay);

        Sequence main = DOTween.Sequence();
        for (int i = 0; i < boardTiles.SideLength; i++)
        {
            Sequence sequence = DOTween.Sequence();
            for (int j = 0; j < boardTiles.SideLength; j++)
            {
                var tile = board[i, j].transform;
                sequence.Join(tile.DOJump(tile.position, 1, 1, boardTiles.jumpAnimationDuration));
            }
            main.Append(sequence);
        }
    }
}
