using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePanel1 : BasePanel
{
    public TextMeshProUGUI texScore;
    public override void Init()
    {
        ChangeInfo($"LIfe:{3}  Score:{0}");
    }
    public void ChangeInfo(string score)
    {
        texScore.text = score;
    }
}
