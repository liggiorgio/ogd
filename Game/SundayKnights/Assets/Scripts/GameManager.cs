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
    [HideInInspector] public int timer;
    [HideInInspector] public int moves;

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
            if ( (timer == 60) || (timer == 30) || (timer == 10) || (timer <= 5) )
                StartCoroutine(FlashText(TimeText, Const.TimeTextSize));
            yield return new WaitForSeconds(1f);
            timer--;
        }
        GameObject.Find("FakeAgent").GetComponent<FakeAgent>().StopFakePlay();
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

        if (moves <= 10)
            MovesText.color = Color.red;
    }

    public void AddMoves(int i)
    {
        this.moves += i;
        ShowMoves();
        if (moves <= 5)
            StartCoroutine(FlashText(MovesText, Const.MovesTextSize));
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
