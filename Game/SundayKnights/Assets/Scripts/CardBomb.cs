using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBomb : MonoBehaviour
{
    // Get selected GO and then take every tile in the 3x3 range and delete it
    
    public Tile selected { get; set;}
    private GameState state = GameState.None;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.None)
        {
            // user has clicked or touched
            if (Input.GetMouseButton(0))
            {
                // get the hit position
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)   // we have a hit
                {
                    GameObject hitGo = hit.collider.gameObject;
                    Debug.Log(hitGo.name);
                }
            }
        }
    }
}
