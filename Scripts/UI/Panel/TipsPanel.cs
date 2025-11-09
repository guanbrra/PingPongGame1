using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TipsPanel : BasePanel
{
    public Button btnBack;
    public Button btnSure;
    public TextMeshProUGUI txtTips;
    //public static bool isSure = false;
    public UnityAction OnConfirmSure;
    public UnityAction OnConfirmBack;
    public override void Init()
    {
        btnBack.onClick.AddListener(() => 
        {
            OnConfirmBack?.Invoke();
            UIManager.Instance.HidePanel<TipsPanel>();
        });
        btnSure.onClick.AddListener(() =>
        {
            OnConfirmSure?.Invoke();
            UIManager.Instance.HidePanel<TipsPanel>();
        });
    }
    public void ChangeInfo(string tips)
    {
        txtTips.text = tips;
    }
}
