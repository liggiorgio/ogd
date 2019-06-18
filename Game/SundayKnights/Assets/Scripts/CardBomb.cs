using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBomb : MonoBehaviour
{
    // Get selected GO and then take every tile in the 3x3 range and delete it

    public Tile selected { get; set; }
    private TilesManager tilesManager;

    // Start is called before the first frame update
    void Start()
    {
        tilesManager = GameObject.Find("TilesManager").GetComponent<TilesManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if ( (tilesManager.state == GameState.None) || (tilesManager.state == GameState.Selecting) )
        {
            // user has clicked or touched
            if ( Input.GetMouseButton(0) )
            {
                // get the hit position
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)   // we have a hit
                {
                    //
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        tilesManager.hitGo = null;
                        tilesManager.state = GameState.Animating;
                        //Debug.Log("Henlo");
                        StartCoroutine(tilesManager.FindMatchesAndCollapse(new RaycastHit2D()));
                    }
                }
            }
        }
    }
}
