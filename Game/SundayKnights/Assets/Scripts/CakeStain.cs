using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeStain : MonoBehaviour
{
    public Sprite[] stains;
    private SpriteRenderer renderer;
    private float alpha = 5f;

    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = stains[Random.Range(0, 4)];
        renderer.color = new Color(0f, 0f, 0f, 0f);
        transform.position = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 10);
        StartCoroutine(ScaleUp());
        StartCoroutine(FadeOut());
    }

    IEnumerator ScaleUp()
    {
        for ( int i = 0; i < 6; i++ )
        {
            transform.localScale = new Vector3(((float) i) / 5, ((float) i) / 5, 1);
            yield return new WaitForSeconds(.0125f);
        }
    }

    IEnumerator FadeOut()
    {
        while (alpha > 0)
        {
            alpha -= .05f;
            renderer.color = new Color(1f, 1f, 1f, Mathf.Min(alpha, 1f));
            yield return new WaitForSeconds(.05f);
        }
        GameObject.Destroy(gameObject);
    }
}
