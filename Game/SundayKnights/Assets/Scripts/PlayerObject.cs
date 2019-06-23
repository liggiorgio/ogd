using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class PlayerObject : NetworkBehaviour
{
    private Text ScoreText;
    private Text oppoText;
    [SyncVar(hook = "OnScoreChanged")]
     public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        oppoText = GameObject.Find("OppoScoreText").GetComponent<Text>();
        if (isLocalPlayer)
            ScoreText.text = "0";
        else
            oppoText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowScore(int myScore)
    {
        int oppoScore = 0;
        int.TryParse(oppoText.text, out oppoScore);
        bool wasLeader = (ScoreText.color == Color.yellow);
        ScoreText.text = myScore.ToString();
        if (oppoScore < myScore)
        {
            ScoreText.color = Color.yellow;
            oppoText.color = Color.white;
            if (!wasLeader)
                StartCoroutine(FlashText(ScoreText, Const.ScoreTextSize));
        }
        else
            ScoreText.color = Color.white;
    }

    public void ShowOppoScore(int myScore)
    {
        int oppoScore = 0;
        int.TryParse(ScoreText.text, out oppoScore);
        bool wasLeader = (oppoText.color == Color.yellow);
        oppoText.text = myScore.ToString();
        if (oppoScore < myScore)
        {
            oppoText.color = Color.yellow;
            ScoreText.color = Color.white;
            if (!wasLeader)
                StartCoroutine(FlashText(oppoText, Const.ScoreTextSize));
        }
        else
            oppoText.color = Color.white;
    }

    private IEnumerator FlashText(Text t, int size)
    {
        for (int i = 0; i < 10; i++)
        {
            t.fontSize = size + (int)Mathf.Round(size * ((float)i) / 15);
            yield return new WaitForSeconds(.01f);
        }
        for (int i = 0; i < 10; i++)
        {
            t.fontSize = size + (int)Mathf.Round(size * ((float)(9 - i)) / 15);
            yield return new WaitForSeconds(.01f);
        }
    }

    public void OnScoreChanged(int newScore)
    {
        if (isLocalPlayer) // Update local player's score
        {
            ShowScore(newScore);
        }
        else // Update opponent's score
        {
            ShowOppoScore(newScore);
        }
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        gameObject.name = "local";
    }
}
