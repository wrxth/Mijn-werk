using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour, ILogicManager {

	public enum GameState {
		None, PreGame, InPlay, Paused, GameOver
	}

	public delegate void GameStateChangeEventHandler(GameState gameState);
	public delegate void GameUpdateEventHandler();

	public event GameStateChangeEventHandler GameStateChange;
	public event GameUpdateEventHandler GameUpdate;

	[SerializeField] private Vector2Int gridSize = new Vector2Int(10, 24);
	[Header("Controls")]
	[SerializeField] private KeyCode moveLeftKey = KeyCode.LeftArrow;
	[SerializeField] private KeyCode moveRightKey = KeyCode.RightArrow;
	[SerializeField] private KeyCode softDropKey = KeyCode.DownArrow;
	[SerializeField] private KeyCode hardDropKey = KeyCode.Space;
	[SerializeField] private KeyCode rotateKey = KeyCode.UpArrow;
	[SerializeField] private KeyCode startGameKey = KeyCode.Space;
	[SerializeField] private KeyCode pauseGameKey = KeyCode.P;

	private int[,,] iPiece = new int[,,] {
		{
			{ 0, 0, 0, 0 },
			{ 1, 1, 1, 1 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 }
		}, {
			{ 0, 0, 1, 0 },
			{ 0, 0, 1, 0 },
			{ 0, 0, 1, 0 },
			{ 0, 0, 1, 0 }
		}
	};
	private int[,,] jPiece = new int[,,] {
		{
			{ 0, 0, 0 },
			{ 2, 0, 0 },
			{ 2, 2, 2 },

		}, {
			{ 0, 2, 2 },
			{ 0, 2, 0 },
			{ 0, 2, 0 },

		}, {
			{ 0, 0, 0 },
			{ 2, 2, 2 },
			{ 0, 0, 2 },

		}, {
			{ 0, 2, 0 },
			{ 0, 2, 0 },
			{ 2, 2, 0 },

		}
	};
	private int[,,] oPiece = new int[,,]
	{
		{
			{0,0,0 },
			{0,3,3 },
			{0,3,3 },

		}
	};
	private int[,,] tPiece = new int[,,]
	{
		{
			{0,0,0 },
			{0,4,0 },
			{4,4,4 },


		},
		{
			{0,4,0 },
			{0,4,4 },
			{0,4,0 },

		},
		{
			{0,0,0 },
			{4,4,4 },
			{0,4,0 },

		},
		{
			{0,4,0 },
			{4,4,0 },
			{0,4,0 },

		}
	};
	private int[,,] lPiece = new int[,,]
	{
		{
			{0,0,0 },
			{0,0,5 },
			{5,5,5 },

		},
		{
			{0,5,0 },
			{0,5,0 },
			{0,5,5 },

		},
		{
			{0,0,0 },
			{5,5,5 },
			{5,0,0 },

		},
		{
			{5,5,0 },
			{0,5,0 },
			{0,5,0 },

		}
	};
	private int[,,] sPiece = new int[,,]
	{
		{
			{0,0,0 },
			{0,6,6 },
			{6,6,0 },


		},
		{
			{0,6,0 },
			{0,6,6 },
			{0,0,6 },

		},
	};
	private int[,,] zPiece = new int[,,]
	{
		{
			{0,0,0 },
			{7,7,0 },
			{0,7,7 },
			

		},
		{
			{0,0,7 },
			{0,7,7 },
			{0,7,0 },

		},
	};
	private int[][,,] allPieces;

	public Vector2Int GridSize => gridSize;
	public int[,] FixedPieces { get; private set; }
	public int[,,] ActivePiece { get; private set; }
	public Vector2Int ActivePiecePosition { get; private set; }
	public Vector2Int StartPos { get; private set; }
	public int ActivePieceRotation { get; private set; }
	public GameState CurrentGameState { get; private set; }
	public int Level => throw new System.NotImplementedException();
	public int Score => throw new System.NotImplementedException();

	private int CurrentBagPiece;

	public Vector2Int GetActivePieceHardDropPosition() {

		Vector2Int dropPos_ = ActivePiecePosition;
		for (int i = 0; i < gridSize.y; i++)
		{
			if (!HasOverlap(ActivePiece, ActivePieceRotation, dropPos_))
			{
				dropPos_ += Vector2Int.down;
			}
			else
			{
				dropPos_ += Vector2Int.up;
				return dropPos_;
			}
		}
		return dropPos_;
	}

	public int[,,] GetNextPieceInBag() {
		if (CurrentBagPiece == allPieces.Length - 1)
        {
			return allPieces[0];
        }
        else
        {
			return allPieces[CurrentBagPiece + 1];
		}
	}
	private void Awake() {
		allPieces = new int[][,,] {
			iPiece,
			jPiece,
            lPiece,
            oPiece,
            sPiece,
            zPiece,
            tPiece
        };
	}

    private void Start()
	{
		FixedPieces = new int[gridSize.y + 10, gridSize.x + 10];
		ActivePiecePosition = new Vector2Int(5, 20);

		ShuffleBag();
		ActivePiece = allPieces[CurrentBagPiece];
		
		StartCoroutine(MoveDown());
		//Debug.Log(gridSize.x);
	}

    private void Update() {

		//Debug.Log(GetActivePieceHardDropPosition());
		if (HasOverlap(ActivePiece, ActivePieceRotation, ActivePiecePosition))
		{
			ActivePiecePosition += Vector2Int.up;
			Place();
			GameUpdate();
		}
		if (Input.GetKeyDown(KeyCode.A) && !HasOverlap(ActivePiece,ActivePieceRotation,ActivePiecePosition + Vector2Int.left))
        {
			ActivePiecePosition += Vector2Int.left;
			GameUpdate();
		}
		if (Input.GetKeyDown(KeyCode.D) && !HasOverlap(ActivePiece,ActivePieceRotation,ActivePiecePosition + Vector2Int.right))
        {
			ActivePiecePosition += Vector2Int.right;
			GameUpdate();
		}
		if (Input.GetKeyDown(KeyCode.S) && !HasOverlap(ActivePiece,ActivePieceRotation,ActivePiecePosition + Vector2Int.down))
        {
			ActivePiecePosition += Vector2Int.down;
			GameUpdate();
		}

		if(Input.GetKeyDown(KeyCode.Space))
        {
			HardDrop();
			GameUpdate();
		}
 
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (ActivePieceRotation == ActivePiece.GetLength(0) - 1)
            {
				if (!HasOverlap(ActivePiece, 0, ActivePiecePosition))
				{
					ActivePieceRotation = 0;
					GameUpdate();
				}
            }
            else
            {
				if (!HasOverlap(ActivePiece, ActivePieceRotation + 1, ActivePiecePosition))
				{
					ActivePieceRotation++;
					GameUpdate();
				}
            }
        }
	}

	// een piece op het grid placen
	private void Place()
    {
		for (int y = 0; y < ActivePiece.GetLength(1); y++)
		{
			for (int x = 0; x < ActivePiece.GetLength(2); x++)
			{
				if (ActivePiece[ActivePieceRotation, y, x] != 0)
				{
					FixedPieces[ActivePiecePosition.y + ActivePiece.GetLength(1) - y, ActivePiecePosition.x + x] = ActivePiece[ActivePieceRotation, y, x];
				}
			}
		}

		CheckFilledRows();

		if (CurrentBagPiece + 1 == allPieces.Length)
        {
			CurrentBagPiece = 0;
		}
        else
        {
			CurrentBagPiece++;
		}
		ActivePiece = allPieces[CurrentBagPiece];

		ActivePiecePosition = StartPos;
		ActivePieceRotation = 0;
	}

	// Een piece direct plaatsen
	private void HardDrop()
    {
        for (int i = 0; i < gridSize.y; i++)
        {
			if (!HasOverlap(ActivePiece, ActivePieceRotation, ActivePiecePosition))
			{
				ActivePiecePosition += Vector2Int.down;
			}
			else
			{
				ActivePiecePosition += Vector2Int.up;
				Place();
				return;
			}         
        }
    }
    private IEnumerator MoveDown()
    {
        while (true)
        {

			//Debug.Log(ActivePiecePosition);

            if (!HasOverlap(ActivePiece, ActivePieceRotation, ActivePiecePosition))
            {
				ActivePiecePosition += Vector2Int.down;
			}
            else
            {
				ActivePiecePosition += Vector2Int.up;
				Place();
			}
			GameUpdate();
			yield return new WaitForSeconds(1f);
        }
    }
	private void ResetGame() {
		FixedPieces = new int[gridSize.y, gridSize.x + 1];
		ChangeGameState(GameState.PreGame);
	}

	private void ChangeGameState(GameState gameState) {
		if (CurrentGameState != gameState) {
			CurrentGameState = gameState;
			GameStateChange?.Invoke(CurrentGameState);
		}
	}

	private Vector2Int GetPieceSize(int[,,] piece) {
		return new Vector2Int(piece.GetLength(2), piece.GetLength(1));
	}
	// de collision 'physics' voor de pieces 
	private bool HasOverlap(int[,,] piece_, int pieceRot_, Vector2Int pos_)
    {
		Vector2Int pieceSize = GetPieceSize(piece_);

        for (int y = 0; y < pieceSize.x; y++)
        {
            for (int x = 0; x < pieceSize.y; x++)
            {
				if (piece_[pieceRot_, y, x] > 0)
				{
					if (pos_.x + x < 0)
					{
						//Debug.Log("left");
						return true;
					}
					else if (pos_.x + x > gridSize.x - 1)
					{
						//Debug.Log("right");
						return true;
					}
					else if (pos_.y + pieceSize.y - y < 0)
					{
						//Debug.Log("down");

						return true;
					}
                    if (FixedPieces[pos_.y + ActivePiece.GetLength(1) - y , pos_.x + x] > 0)
                    {
						return true;
                    }
                }
			}
        }
		return false;
    }
	private void ShuffleBag()
    {
        for (int i = 0; i < allPieces.Length; i++)
        {
			int[,,] currentPiece = allPieces[i];
			int randomNumber = Random.Range(0, allPieces.Length);
			allPieces[i] = allPieces[randomNumber];
			allPieces[randomNumber] = currentPiece;
		}
    }

	// Check welke rijen zijn gevulled
	private void CheckFilledRows()
	{
		List<int> filledRows = new List<int>();

		for (int y = 0; y < gridSize.y; y++)
		{
			int rowCount = 0;
			for (int x = 0; x < gridSize.x; x++)
			{
				if (FixedPieces[y,x] > 0)
				{
					rowCount++;
				}
			}

			if (rowCount >= gridSize.x)
			{
				filledRows.Add(y);
			}
		}

		for (int i = 0; i < filledRows.Count; i++)
		{
			ClearRow(filledRows[i]);
		}
	}

	// Haal elke rij weg en naar beneden
	private void ClearRow(int row_)
    {
        for (int y = 0; y < gridSize.y - row_; y++)
        {
			if (y == gridSize.y - row_ - 1)
			{
				for (int x = 0; x < gridSize.x; x++)
				{
					FixedPieces[y + row_, x] = 0;
				}
			}
            else
            {
				for (int x = 0; x < gridSize.x; x++)
				{
					FixedPieces[y + row_, x] = FixedPieces[y + row_ + 1, x];
				}
			}
		}

		// kijk of er een nieuwe gevullde rij is ontstaan
		CheckFilledRows();
	}
}
