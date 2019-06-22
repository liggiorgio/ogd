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
    public Text CountdownText;
    public Text BuffText;
    public Text DebuffText;
    private float buffAlpha = 0, debuffAlpha = 0;
    [HideInInspector] public int timer;
    [HideInInspector] public int moves;
    private int count = 4;

    // Start is called before the first frame update
    void Start()
    {
        timer = MaxTime;
        moves = MaxMoves;
        MovesText.text = moves.ToString();
        CountdownText.text = "";
        TimeText.text = "-:-";
        BuffText.color = new Color(255f, 255f, 0f, buffAlpha);
        DebuffText.color = new Color(255f, 255f, 0f, debuffAlpha);
        StartCoroutine(StartCountdown());
        Time.timeScale = 1;
    }

    void Update()
    {
        // vfx
        if (buffAlpha > 0)
        {
            buffAlpha -= .025f;
            BuffText.color = new Color(.145098f, .5254902f, .827451f, buffAlpha);
        }

        if (debuffAlpha > 0)
        {
            debuffAlpha -= .025f;
            DebuffText.color = new Color(.8039216f, .1607843f, .254902f, debuffAlpha);
        }
    }

    IEnumerator GameTimer()
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

    IEnumerator StartCountdown()
    {
        for ( int i = 0; i < 3; i++ )
        {
            yield return new WaitForSeconds(1f);
            count--;
            CountdownText.text = count.ToString();
            StartCoroutine(FlashText(CountdownText, Const.CountdownTextSize));
        }
        yield return new WaitForSeconds(1f);
        CountdownText.text = "Go!";
        StartCoroutine(FlashText(CountdownText, Const.CountdownTextSize));
        yield return new WaitForSeconds(1f);
        CountdownText.text = "";
        timer = MaxTime;
        moves = MaxMoves;
        MovesText.text = moves.ToString();
        StartCoroutine(GameTimer());
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

    public void PutBuff(string text)
    {
        BuffText.text = text;
        buffAlpha = 1.5f;
    }

    public void PutDebuff(string text)
    {
        DebuffText.text = text;
        debuffAlpha = 1.5f;
    }
}
