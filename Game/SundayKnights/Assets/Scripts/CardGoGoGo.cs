using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGoGoGo : MonoBehaviour
{
    // Count combos, add to MoveCounter

    private TilesManager tilesManager;
    private Vector3 startPos;
    private bool consumed;

    // Start is called before the first frame update
    void Start()
    {
        tilesManager = GameObject.Find("TilesManager").GetComponent<TilesManager>();
        startPos = transform.position;
    }

    // LateUpdate is called once per frame, after all other Updates
    void LateUpdate()
    {
        if (consumed)
            return;
        if ( (tilesManager.state == GameState.None) || (tilesManager.state == GameState.Selecting) )
        {
            // user has clicked or touched
            if ( Input.GetMouseButtonDown(0) )
            {
                // get the hit position
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)   // we have a hit
                {
                    if ( (hit.collider.gameObject == this.gameObject) )
                        StartCoroutine("CardActivate");
                }
            }
        }
    }

    private IEnumerator CardActivate()
    {
        tilesManager.hitGo = null;
        tilesManager.state = GameState.Animating;
        tilesManager.comboCount = true;
        for ( int row = 0; row < Const.Rows; row++ )
        {
            for ( int column = 0; column < Const.Columns; column++ )
            {
                tilesManager.tiles[row, column].GetComponent<Tile>().Shine(Random.value);
            }
        }
        transform.positionTo(2 * Const.AnimationDuration, transform.position + new Vector3(0f, -4f, 0f));
        GameObject.Find("GameManager").GetComponent<GameManager>().PutBuff("Go Go Go!");
        yield return new WaitForSeconds(Const.AnimationDuration);
        tilesManager.state = GameState.None;
        consumed = true;
    }
}
