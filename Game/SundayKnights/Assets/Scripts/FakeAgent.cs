using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeAgent : MonoBehaviour
{
    public Text ScoreText;
    private TilesManager tilesManager;
    [HideInInspector] public int score;
    private readonly int[] scoreList = { 60, 60, 60, 120, 180, 60, 60, 1120, 420 };
    private IEnumerator FakePlayCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        tilesManager = GameObject.Find("TilesManager").GetComponent<TilesManager>();
        FakePlayCoroutine = StartFakePlay();
        StartCoroutine(FakePlayCoroutine);
        ScoreText.text = "0";
    }

    // Simulate an opponent
    private IEnumerator StartFakePlay()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 3));
            score += scoreList[Random.Range(0, scoreList.Length)];
            ShowScore();
        }
    }

    private void ShowScore()
    {
        ScoreText.text = score.ToString();

        if (tilesManager.score < score)
        {
            tilesManager.ScoreText.color = Color.white;
            ScoreText.color = Color.yellow;
        }
        else
        {
            tilesManager.ScoreText.color = Color.yellow;
            ScoreText.color = Color.white;
        }
    }
}
