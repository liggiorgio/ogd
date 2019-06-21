using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeAgent : MonoBehaviour
{
    public Text ScoreText;
    private TilesManager tilesManager;
    [HideInInspector] public int score;
    private readonly int[] scoreList = { 30, 30, 30, 60, 90, 30, 30, 120, 150 };
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
            yield return new WaitForSeconds(Random.Range(2, 4) * Random.value);
            int amount = scoreList[Random.Range(0, scoreList.Length)];
            score += amount;
            ShowScore(amount);
        }
    }

    public void StopFakePlay()
    {
        StopCoroutine(FakePlayCoroutine);
    }

    private void ShowScore(int diff)
    {
        bool wasLeader = (score - diff > tilesManager.score);
        ScoreText.text = score.ToString();
        if (tilesManager.score < score)
        {
            tilesManager.ScoreText.color = Color.white;
            ScoreText.color = Color.yellow;
            if (!wasLeader)
                StartCoroutine(FlashText(ScoreText, Const.ScoreTextSize));
        }
        else
        {
            tilesManager.ScoreText.color = Color.yellow;
            ScoreText.color = Color.white;
        }
    }

    private IEnumerator FlashText(Text t, int size)
    {
        for ( int i = 0; i < 10; i++ )
        {
            t.fontSize = size + (int) Mathf.Round(size * ((float) i) / 15);
            yield return new WaitForSeconds(.01f);
        }
        for ( int i = 0; i < 10; i++ )
        {
            t.fontSize = size + (int) Mathf.Round(size * ((float) (9 - i)) / 15);
            yield return new WaitForSeconds(.01f);
        }
    }
}
