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
       
    }
}
