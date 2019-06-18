using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int MaxTime;
    public int MaxMoves;
    public Text TimeText;
    public Text MovesText;
    private int timer;
    public int moves;

    // Start is called before the first frame update
    void Start()
    {
        timer = MaxTime;
        moves = MaxMoves;
        MovesText.text = moves.ToString();
        StartCoroutine("CountDown");
        Time.timeScale = 1;
    }

    IEnumerator CountDown()
    {
        while (timer >= 0)
        {
            ShowTime();
            yield return new WaitForSeconds(1f);
            timer--;
        }
    }

    void ShowTime()
    {
        string minutes = Mathf.Floor(timer / 60).ToString("0");
        string seconds = (timer % 60).ToString("00");

        TimeText.text = string.Format("{0}:{1}", minutes, seconds);

        if (timer <= 10)
            TimeText.color = Color.red;
    }

    void ShowMoves()
    {
        MovesText.text = moves.ToString();

        if (moves <= 5)
            MovesText.color = Color.red;
    }

    public void AddMoves(int i)
    {
        this.moves += i;
        ShowMoves();
    }
}
