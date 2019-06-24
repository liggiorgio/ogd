using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardJelly : MonoBehaviour
{
    // Change backgound colour of tiles, make animation slower, remove after 3 matches in area (if easy to implement, combos count)
	public GameObject jellyBox;
	private TilesManager tilesManager;
	private bool consumed;

    // Start is called before the first frame update
    void Start()
    {
        tilesManager = GameObject.Find("TilesManager").GetComponent<TilesManager>();
    }

    // Update is called once per frame
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
		//tilesManager.speedMultiplier = .2f;
		//jellyBox.transform.position = new Vector3(0f, 0f, 1f);
		GameObject.Find("local").GetComponent<PlayerObject>().PlayJelly();
        transform.positionTo(2 * Const.AnimationDuration, transform.position + new Vector3(0f, -4f, 0f));
		GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayJelly();
        yield return new WaitForSeconds(Const.AnimationDuration);
        tilesManager.state = GameState.None;
        consumed = true;
    }
}
