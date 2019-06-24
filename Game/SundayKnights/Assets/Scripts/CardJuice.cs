using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardJuice : MonoBehaviour
{
    // Count combos, add to MoveCounter
    public Text TimerText;
    private TilesManager tilesManager;
    private Vector3 startPos;
    private bool consumed;

    // Start is called before the first frame update
    void Start()
    {
        tilesManager = GameObject.Find("TilesManager").GetComponent<TilesManager>();
        startPos = transform.position;
        TimerText.text = "";
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
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        tilesManager.scoreMultiplier = 2;
        for ( int row = 0; row < Const.Rows; row++ )
        {
            for ( int column = 0; column < Const.Columns; column++ )
            {
                tilesManager.tiles[row, column].GetComponent<Tile>().Shine(((float) (row + column))/50);
            }
        }
        transform.positionTo(Const.AnimationDuration, startPos + new Vector3(0f, .3f, 0f));
        GameObject.Find("GameManager").GetComponent<GameManager>().PutBuff("Double Score!");
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayJuice();
        yield return new WaitForSeconds(Const.AnimationDuration);
        tilesManager.state = GameState.None;
        for ( int i = 0; i < Const.JuiceTimer; i++ )
        {
            TimerText.text = (Const.JuiceTimer - i).ToString();
            yield return new WaitForSeconds(1f);
        }
        transform.positionTo(2 * Const.AnimationDuration, transform.position + new Vector3(0f, -4f, 0f));
        tilesManager.scoreMultiplier = 1;
        TimerText.text = "";
        consumed = true;
    }
}
