using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : MonoBehaviour {

    private LogicManager logicManager;
    [SerializeField] private GameObject Tile;
    [SerializeField] private GameObject TileParent;
    [SerializeField] private GameObject Preview;
    [SerializeField] private GameObject[,] PreviewGrid;
    [SerializeField] private Color[] Colors;

    private void Start() {
        logicManager = FindObjectOfType<LogicManager>();
        logicManager.GameUpdate += HandleGameUpdate;
        logicManager.GameStateChange += HandleGameStateChange;

        PreviewGrid = new GameObject[10, 10];
        DrawGrid();
    }

    private void HandleGameStateChange(LogicManager.GameState gameState) {
        if (gameState == LogicManager.GameState.PreGame)
        {
            DrawGrid();
        }
    }

    private void DrawGrid()
    {
        for (int i = 0; i < logicManager.GridSize.y; i++)
        {
            for (int j = 0; j < logicManager.GridSize.x; j++)
            {
                Vector2 placeSpot = new Vector2(1 * j, 1 * i);
                GameObject tile = Instantiate(Tile, placeSpot, Quaternion.identity);
                tile.transform.parent = TileParent.transform;
                tile.name = "x: " + j + " y: " + i;
                tile.GetComponent<SpriteRenderer>().color = Colors[0];
            }
        }

        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                Vector2 placeSpot = new Vector2(Preview.transform.position.x + 1 * x, Preview.transform.position.y + 1 * y);
                GameObject tile = Instantiate(Tile, placeSpot, Quaternion.identity);
                tile.transform.parent = TileParent.transform;
                tile.name = "x: " + Preview.transform.position.x + 1 * x + " y: " + Preview.transform.position.y + 1 * y;
                tile.GetComponent<SpriteRenderer>().color = Colors[0];
                PreviewGrid[y, x] = tile;
                //Debug.Log(PreviewGrid[y, x]);
            }
        }
    }

    private void HandleGameUpdate() {
        UpdateGrid();
        UpdateNextPiece();
    }
    private void UpdateGrid()
    {
        int[,] currentpiece = new int[logicManager.GridSize.y + 10, logicManager.GridSize.x + 34];

        Vector2Int shadowPos = logicManager.GetActivePieceHardDropPosition();

        for (int y = 0; y < logicManager.ActivePiece.GetLength(1); y++)
        {
            for (int x = 0; x < logicManager.ActivePiece.GetLength(2); x++)
            {
                if (logicManager.ActivePiece[logicManager.ActivePieceRotation, y, x] != 0)
                {
                    currentpiece[logicManager.ActivePiecePosition.y + logicManager.ActivePiece.GetLength(1) - y, logicManager.ActivePiecePosition.x + x] = logicManager.ActivePiece[logicManager.ActivePieceRotation, y, x];
                    currentpiece[shadowPos.y + logicManager.ActivePiece.GetLength(1) - y, shadowPos.x + x] = 10;
                }
            }
        }
        for (int y = 0; y < logicManager.GridSize.y; y++)
        {
            for (int x = 0; x < logicManager.GridSize.x; x++)
            {
                GameObject coloredTile = GameObject.Find("x: " + x + " y: " + y);

                
                switch (currentpiece[y, x])
                {
                    case 0:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[0];
                        break;
                    case 1:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[currentpiece[y, x]];
                        break;
                    case 2:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[currentpiece[y, x]];
                        break;
                    case 3:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[currentpiece[y, x]];
                        break;
                    case 4:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[currentpiece[y, x]];
                        break;
                    case 5:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[currentpiece[y, x]];
                        break;
                    case 6:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[currentpiece[y, x]];
                        break;
                    case 7:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[currentpiece[y, x]];
                        break;;
                    case 10:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[8];
                        break;
                    default:
                        break;
                }
            }
        }

        for (int y = 0; y < logicManager.GridSize.y; y++)
        {
            for (int x = 0; x < logicManager.GridSize.x; x++)
            {
                if (logicManager.FixedPieces[y, x] > 0)
                {
                    GameObject coloredTile = GameObject.Find("x: " + x + " y: " + y);

                    switch (logicManager.FixedPieces[y, x])
                    {
                        case 0:
                            coloredTile.GetComponent<SpriteRenderer>().color = Colors[0];
                            break;
                        case 1:
                            coloredTile.GetComponent<SpriteRenderer>().color = Colors[logicManager.FixedPieces[y, x]];
                            break;
                        case 2:
                            coloredTile.GetComponent<SpriteRenderer>().color = Colors[logicManager.FixedPieces[y, x]];
                            break;
                        case 3:
                            coloredTile.GetComponent<SpriteRenderer>().color = Colors[logicManager.FixedPieces[y, x]];
                            break;
                        case 4:
                            coloredTile.GetComponent<SpriteRenderer>().color = Colors[logicManager.FixedPieces[y, x]];
                            break;
                        case 5:
                            coloredTile.GetComponent<SpriteRenderer>().color = Colors[logicManager.FixedPieces[y, x]];
                            break;
                        case 6:
                            coloredTile.GetComponent<SpriteRenderer>().color = Colors[logicManager.FixedPieces[y, x]];
                            break;
                        case 7:
                            coloredTile.GetComponent<SpriteRenderer>().color = Colors[logicManager.FixedPieces[y, x]];
                            break; ;
                        case 10:
                            coloredTile.GetComponent<SpriteRenderer>().color = Colors[8];
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void UpdateNextPiece()
    {
        int[,] nextPiece = new int[4, 4];

        for (int y = 0; y < logicManager.GetNextPieceInBag().GetLength(1); y++)
        {
            for (int x = 0; x < logicManager.GetNextPieceInBag().GetLength(2); x++)
            {
                if (logicManager.GetNextPieceInBag()[0, y, x] != 0)
                {
                    nextPiece[logicManager.GetNextPieceInBag().GetLength(1) - y, x] = logicManager.GetNextPieceInBag()[0, y, x];
                }
            }
        }

        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                GameObject coloredTile = PreviewGrid[y,x];


                switch (nextPiece[y, x])
                {
                    case 0:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[0];
                        break;
                    case 1:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[nextPiece[y, x]];
                        break;
                    case 2:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[nextPiece[y, x]];
                        break;
                    case 3:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[nextPiece[y, x]];
                        break;
                    case 4:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[nextPiece[y, x]];
                        break;
                    case 5:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[nextPiece[y, x]];
                        break;
                    case 6:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[nextPiece[y, x]];
                        break;
                    case 7:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[nextPiece[y, x]];
                        break; ;
                    case 10:
                        coloredTile.GetComponent<SpriteRenderer>().color = Colors[8];
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
