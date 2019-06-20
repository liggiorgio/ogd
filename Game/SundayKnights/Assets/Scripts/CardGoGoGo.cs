using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGoGoGo : MonoBehaviour
{
    // Count combos, add to MoveCounter

    private TilesManager tilesManager;

    // Start is called before the first frame update
    void Start()
    {
        tilesManager = GameObject.Find("TilesManager").GetComponent<TilesManager>();
    }

    // LateUpdate is called once per frame, after all other Updates
    void LateUpdate()
    {
        if ((tilesManager.state == GameState.None) || (tilesManager.state == GameState.Selecting))
        {
            // user has clicked or touched
            if (Input.GetMouseButton(0))
            {
                // get the hit position
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)   // we have a hit
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        tilesManager.state = GameState.None;
                        tilesManager.comboCount = true;
                    }
                }
            }
        }
    }
}
