using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBomb : MonoBehaviour
{
    // Get selected GO and then take every tile in the 3x3 range and delete it

    public Tile selected { get; set; }
    private TilesManager tilesManager;
    private int row = -1, column = -1;
    private bool selecting = false;
    private bool consumed = false;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        tilesManager = GameObject.Find("TilesManager").GetComponent<TilesManager>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
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
                        StartCoroutine("CardSelect");
                }
            }
        }
        else if ( (tilesManager.state == GameState.Animating) && selecting )
        {
            // Selecting target tile
            if ( Input.GetMouseButtonDown(0) )
            {
                // get the hit position
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)   // we have a hit
                {
                    if (hit.collider.gameObject == this.gameObject)
                        StartCoroutine("CardDeselect");
                    else if (hit.collider.gameObject.GetComponent<Tile>() != null)
                        StartCoroutine(TargetSelect(hit));
                }
            }
        }
    }

    private IEnumerator CardSelect()
    {
        // Selecting the card
        tilesManager.hitGo = null;
        tilesManager.state = GameState.Animating;
        transform.positionTo(Const.AnimationDuration, startPos + new Vector3(0f, .3f, 0f));
        GameObject.Find("CardGoGoGo").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .3f);
        GameObject.Find("CardJelly").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .3f);
        GameObject.Find("CardTime").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .3f);
        yield return new WaitForSeconds(Const.AnimationDuration);
        selecting = true;
    }

    private IEnumerator CardDeselect()
    {
        // deselecting card
        row = -1;
        column = -1;
        transform.positionTo(Const.AnimationDuration, startPos);
        GameObject.Find("CardGoGoGo").GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("CardJelly").GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("CardTime").GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(Const.AnimationDuration);
        tilesManager.state = GameState.None;
        selecting = false;
    }

    private IEnumerator TargetSelect(RaycastHit2D hit)
    {
        // Aiming
        row = hit.collider.gameObject.GetComponent<Tile>().Row;
        column = hit.collider.gameObject.GetComponent<Tile>().Column;
        StartCoroutine(tilesManager.FindMatchesAndCollapse(new RaycastHit2D()));
        transform.positionTo(2 * Const.AnimationDuration, transform.position + new Vector3(0f, -2f, 0f));
        GameObject.Find("CardGoGoGo").GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("CardJelly").GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("CardTime").GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(Const.AnimationDuration);
        tilesManager.state = GameState.None;
        GameObject.Destroy(gameObject);
    }

    public int GetRow()
    {
        return row;
    }

    public int GetColumn()
    {
        return column;
    }
}
