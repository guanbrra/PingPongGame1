using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePanel2 : BasePanel
{
    public TextMeshProUGUI txtScore1;
    public TextMeshProUGUI txtScore2;
    public TextMeshProUGUI txtTime;
    private int score1 = 0;
    private int score2 = 0;
    //private int time = 0;

    public override void Init()
    {
        txtScore1.text = "0";
        txtScore2.text = "0";
    }

    public void ChangeScore2()
    {
        score1++;
        txtScore1.text = score1.ToString();
        //txtScore2.text = score2.ToString();
    }
    public void ChangeScore1()
    {
        score2++;
        txtScore2.text = score2.ToString();
    }

    public void ChangeTime(float t)
    {
        //time--;
        txtTime.text = t.ToString();
    }

}
