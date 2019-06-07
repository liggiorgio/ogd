using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/*
    This is the main class of the game. It creates the game grid, keeps track
    of the game score, and creates and destroys tiles during the game.
*/

public class TilesManager : MonoBehaviour
{
    public Text DebugText, ScoreText;
    public bool ShowDebugInfo = false;

    public TilesArray tiles;

    private int score;

    public readonly Vector2 BottomRight = new Vector2(-2.37f, -4.27f);
    public readonly Vector2 TileSize = new Vector2(0.7f, 0.7f);

    private GameState state = GameState.None;
    private GameObject hitGo = null;
    private Vector2[] SpawnPositions;
    public GameObject[] TilePrefabs;
    public GameObject[] ExplosionPrefabs;
    public GameObject[] BonusPrefabs;

    // check potential matches
    // animate potential matches

    // enumerable potential matches

    public SoundManager soundManager;

    // Enable/disable debug info
    void Awake()
    {
        DebugText.enabled = ShowDebugInfo;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeTypesOnPrefabTilesAndBonuses();

        //InitializeTileAndSpawnPositions();

        //StartCheckForPotentialMatches();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Assign a type and a bonus type to tiles
    private void InitializeTypesOnPrefabTilesAndBonuses()
    {
        // just assign the name of the prefab
        foreach (var item in TilePrefabs)
            item.GetComponent<Tile>().Type = item.name;

        // assign the name of the respective "normal"
        // candy as the type of the Bonus
        foreach (var item in BonusPrefabs)
            item.GetComponent<Tile>().Type = TilePrefabs.
            Where(x => x.GetComponent<Tile>().Type.Contains(item.name.Split('_')[1].Trim())).Single().name;
    }

    // Setup objects on the grid for a new game
    public void InitializeTileAndSpawnPositions()
    {
        InitializeVariables();

        if (tiles != null)
            DestroyAllTiles();

        tiles = new TilesArray();
        SpawnPositions = new Vector2[Const.Columns];

        for ( int row = 0; row < Const.Rows; row++ )
        {
            for ( int column = 0; column < Const.Columns; column++ )
            {
                GameObject newTile = GetRandomTile();

                // check if two previous horizontal are of the same type
                while ( (column > 2) &&
                    (tiles[row, column-1].GetComponent<Tile>().IsSameType(newTile.GetComponent<Tile>())) &&
                    (tiles[row, column-2].GetComponent<Tile>().IsSameType(newTile.GetComponent<Tile>())) )
                {
                    newTile = GetRandomTile();
                }

                // check if two previous vertical are of the same type
                while ( (row > 2) &&
                    (tiles[row-1, column].GetComponent<Tile>().IsSameType(newTile.GetComponent<Tile>())) &&
                    (tiles[row-2, column].GetComponent<Tile>().IsSameType(newTile.GetComponent<Tile>())) )
                {
                    newTile = GetRandomTile();
                }

                // it's safe to place such a tile
                InstantiateAndPlaceNewTile(row, column, newTile);
            }
        }

        SetupSpawnPositions();
    }

    // Scoring
    private void InitializeVariables()
    {
        score = 0;
        ShowScore();
    }

    private void IncreaseScore(int amount)
    {
        score += amount;
        ShowScore();
    }

    private void ShowScore()
    {
        ScoreText.text = "Score: " + score.ToString();
    }

    // Return a random tile prefab
    private GameObject GetRandomTile()
    {
        return TilePrefabs[Random.Range(0, TilePrefabs.Length)];
    }

    // Add a tile to the grid at a given position
    private void InstantiateAndPlaceNewTile(int row, int column, GameObject newTile)
    {
        GameObject go = Instantiate(newTile,
            BottomRight + new Vector2(column * TileSize.x, row * TileSize.y), Quaternion.identity)
            as GameObject;

        // assign specific properties
        go.GetComponent<Tile>().Assign(newTile.GetComponent<Tile>().Type, row, column);
        tiles[row, column] = go;
    }

    // Setup spawn positions (for new tiles after destroying old ones)
    private void SetupSpawnPositions()
    {
        // create the spawn positions for the new tiles (will pop from above)
        for ( int column = 0; column <  Const.Columns; column++)
            SpawnPositions[column] = BottomRight + new Vector2(column * TileSize.x, Const.Rows * TileSize.y);
    }

    // Clear scene from all tiles
    private void DestroyAllTiles()
    {
        for ( int row = 0; row < Const.Rows; row++)
            for ( int column = 0; column < Const.Columns; column++)
                Destroy(tiles[row, column]);
    }

    // Animate swapping tiles
    private void MoveAndAnimate(IEnumerable<GameObject> movedGameObjects, int distance)
    {
        foreach (var item in movedGameObjects)
        {
            item.transform.positionTo( Const.MoveAnimationMinDuration * distance, BottomRight +
                new Vector2(item.GetComponent<Tile>().Column * TileSize.x, item.GetComponent<Tile>().Row * TileSize.y) );
        }
    }

    // Make sure swapping tiles are always drawn on top of others
    private void FixSortingLayer(GameObject hitGo, GameObject hitGo2)
    {
        SpriteRenderer sp1 = hitGo.GetComponent<SpriteRenderer>();
        SpriteRenderer sp2 = hitGo2.GetComponent<SpriteRenderer>();

        if ( sp1.sortingOrder <= sp2.sortingOrder )
        {
            sp1.sortingOrder = 1;
            sp2.sortingOrder = 0;
        }
    }

    // Return a random explosion prefab
    private GameObject GetRandomExplosion()
    {
        return TilePrefabs[Random.Range(0, ExplosionPrefabs.Length)];
    }

    // Animate item explosion
    private void RemoveFromScene(GameObject item)
    {
        GameObject explosion = GetRandomExplosion();
        var newExplosion = Instantiate(explosion, item.transform.position, Quaternion.identity) as GameObject;
        Destroy(newExplosion, Const.ExplosionDuration);
        Destroy(item);
    }
}
