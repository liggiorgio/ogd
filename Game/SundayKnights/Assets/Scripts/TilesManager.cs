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
    public Text ScoreText;

    public TilesArray tiles;

    private int score;

    public readonly Vector2 TileSize = new Vector2(.71f, .71f);
    public readonly Vector2 BottomRight = new Vector2(-1.775f, -1.775f);

    [HideInInspector] public GameState state = GameState.None;
    [HideInInspector] public GameObject hitGo = null;
    private Vector2[] SpawnPositions;
    public GameObject[] TilePrefabs;
    public GameObject[] ExplosionPrefabs;
    public GameObject[] BonusPrefabs;
    public GameObject[] Cards;

    private IEnumerator CheckPotentialMatchesCoroutine;
    private IEnumerator AnimatePotentialMatchesCoroutine;

    IEnumerable<GameObject> potentialMatches;

    public SoundManager soundManager;

    // Enable/disable debug info
    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        // Set up prefabs
        InitializeTypesOnPrefabTilesAndBonuses();

        // Clear score, fill grid
        InitializeVariables();
        InitializeTileAndSpawnPositions();

        // Start hints
        StartCheckForPotentialMatches();
    }

    // Update is called once per frame
    void Update()
    {
        // game state FSA
        if (state == GameState.None)
        {
            // user has clicked or touched
            if ( Input.GetMouseButton(0) )
            {
                // get the hit position
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)   // we have a hit
                {
                    hitGo = hit.collider.gameObject;
                    state = GameState.Selecting;
                }
            }
        }
        else if (state == GameState.Selecting)
        {
            // user is selecting/dragging
            if ( Input.GetMouseButton(0) )
            {
                // get the hit position
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)   // we have another hit
                {
                    if (hit.collider.gameObject != hitGo)   // check allowed move
                    {
                        //user did a hit, no need to show him hints
                        StopCheckForPotentialMatches();

                        if ( !Utilities.AreVerticalOrHorizontalNeighbors(hitGo.GetComponent<Tile>(),
                            hit.collider.gameObject.GetComponent<Tile>()) )
                        {
                            state = GameState.None;
                        }
                        else
                        {
                            state = GameState.Animating;
                            FixSortingLayer(hitGo, hit.collider.gameObject);
                            StartCoroutine(FindMatchesAndCollapse(hit));
                        }
                    }
                }
            }
        }
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

    private void StartCheckForPotentialMatches()
    {
        StopCheckForPotentialMatches();
        //get a reference to stop it later
        CheckPotentialMatchesCoroutine = CheckPotentialMatches();
        StartCoroutine(CheckPotentialMatchesCoroutine);
    }

    private IEnumerator CheckPotentialMatches()
    {
        yield return new WaitForSeconds(Const.WaitBeforePotentialMatchesCheck);
        potentialMatches = Utilities.GetPotentialMatches(tiles);
        if (potentialMatches != null)
        {
            while (true)
            {
                AnimatePotentialMatchesCoroutine = Utilities.AnimatePotentialMatches(potentialMatches);
                StartCoroutine(AnimatePotentialMatchesCoroutine);
                yield return new WaitForSeconds(Const.WaitBeforePotentialMatchesCheck);
            }
        }
        else
        {
            // no more moves available, shuffle grid
            InitializeTileAndSpawnPositions();
        }
    }

    private void ResetOpacityOnPotentialMatches()
    {
        if (potentialMatches != null)
            foreach (var item in potentialMatches)
            {
                if (item == null) break;

                Color c = item.GetComponent<SpriteRenderer>().color;
                c.a = 1.0f;
                item.GetComponent<SpriteRenderer>().color = c;
            }
    }

    private void StopCheckForPotentialMatches()
    {
        if (AnimatePotentialMatchesCoroutine != null)
            StopCoroutine(AnimatePotentialMatchesCoroutine);
        if (CheckPotentialMatchesCoroutine != null)
            StopCoroutine(CheckPotentialMatchesCoroutine);
        ResetOpacityOnPotentialMatches();
    }

    // Actuate game rules and mechanics: find matches, destroy tiles, spawn new ones, collapse others
    public IEnumerator FindMatchesAndCollapse(RaycastHit2D hit2)
    {
        IEnumerable<GameObject> totalMatches;

        StopCheckForPotentialMatches();

        if (hit2.collider != null)
        {
            // get the second item that was part of the swipe
            var hitGo2 = hit2.collider.gameObject;
            tiles.Swap(hitGo, hitGo2);

            // animate swap movement
            hitGo.transform.positionTo(Const.AnimationDuration, hitGo2.transform.position);
            hitGo2.transform.positionTo(Const.AnimationDuration, hitGo.transform.position);
            yield return new WaitForSeconds(Const.AnimationDuration);

            // get the matches via the helper methods
            var hitGoMatchesInfo = tiles.GetMatches(hitGo);
            var hitGo2MatchesInfo = tiles.GetMatches(hitGo2);
            totalMatches = hitGoMatchesInfo.MatchedTile.Union(hitGo2MatchesInfo.MatchedTile).Distinct();

            // if swap didn't create at least a 3-match, undo their swap
            if (totalMatches.Count() < Const.MinimumMatches)
            {
                hitGo.transform.positionTo(Const.AnimationDuration, hitGo2.transform.position);
                hitGo2.transform.positionTo(Const.AnimationDuration, hitGo.transform.position);
                yield return new WaitForSeconds(Const.AnimationDuration);

                tiles.UndoSwap();
            }
        }
        else
        {
            totalMatches = tiles.GetMatchesBomb(tiles[3, 3]);
        }

        // remove tiles until a rest configuration is reached
        int timesRun = 1;
        while (totalMatches.Count() >= Const.MinimumMatches)
        {
            // increase score
            IncreaseScore( (totalMatches.Count() - 2) * Const.Match3Score );
            if (timesRun > 1)
                IncreaseScore(Const.SubsequentMatchScore);

            // small delay for combos
            if (timesRun > 1)
                yield return new WaitForSeconds(Const.CollapseDelay);

            // play sfx
            soundManager.PlayMatch(timesRun - 1);

            // spawn explosions and remove tiles
            foreach (var item in totalMatches)
            {
                RemoveFromScene(item);
                tiles.Remove(item);
            }

            // get columns with tiles to collapse
            var columns = totalMatches.Select(go => go.GetComponent<Tile>().Column).Distinct();
            // collapse the ones gone
            var collapsedTileInfo = tiles.Collapse(columns);
            // create new ones
            var newTileInfo = CreateNewTileInSpecificColumns(columns);

            int maxDistance = Mathf.Max(collapsedTileInfo.MaxDistance, newTileInfo.MaxDistance);

            MoveAndAnimate(newTileInfo.AlteredTile, maxDistance);
            MoveAndAnimate(collapsedTileInfo.AlteredTile, maxDistance);

            // wait for the both of the above animations
            yield return new WaitForSeconds(Const.MoveAnimationMinDuration * maxDistance);

            // search if there are matches in the next tile configuration
            totalMatches = tiles.GetMatches(collapsedTileInfo.AlteredTile).
                Union(tiles.GetMatches(newTileInfo.AlteredTile)).Distinct();

            timesRun++;
        }

        state = GameState.None;
        StartCheckForPotentialMatches();
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

    // Find out empty columns and spawn new tiles in position
    private AlteredTileInfo CreateNewTileInSpecificColumns(IEnumerable<int> columnsWithMissingTiles)
    {
        AlteredTileInfo newTileInfo = new AlteredTileInfo();

        // find how many null values the column has
        foreach (int column in columnsWithMissingTiles)
        {
            var emptyItems = tiles.GetEmptyItemsOnColumn(column);
            foreach (var item in emptyItems)
            {
                var go = GetRandomTile();
                GameObject newTile = Instantiate(go, SpawnPositions[column], Quaternion.identity) as GameObject;
                newTile.GetComponent<Tile>().Assign( go.GetComponent<Tile>().Type, item.Row, item.Column );

                if ( Const.Rows - item.Row > newTileInfo.MaxDistance )
                    newTileInfo.MaxDistance = Const.Rows - item.Row;

                tiles[item.Row, item.Column] = newTile;
                newTileInfo.AddTile(newTile);
            }
        }

        return newTileInfo;
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
        return ExplosionPrefabs[Random.Range(0, ExplosionPrefabs.Length)];
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
