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
    private Vector2[] spawnPositions;
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
}
