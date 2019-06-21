using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardJelly : MonoBehaviour
{
    // Change backgound colour of tiles, make animation slower, remove after 3 matches in area (if easy to implement, combos count)

	public GameObject jelly;
	private TilesManager tilesManager;
	[HideInInspector] public float slowFactor;

    // Start is called before the first frame update
    void Start()
    {
        tilesManager = GameObject.Find("TilesManager").GetComponent<TilesManager>();
		jelly.active = false;
		slowFactor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ( (tilesManager.state == GameState.None) || (tilesManager.state == GameState.Selecting) )
        {
            // This input will change once networking is up
            if ( Input.GetMouseButton(0) )
            {
				jelly.active = true;
				slowFactor = 4f;
			}
		}
    }
}
