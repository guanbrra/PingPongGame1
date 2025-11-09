using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvas; //控制画布组件淡入淡出
    private float fadeTime = 10f; //淡入出时间
    private bool isShow = false; //是否正在淡入淡出
    
    private UnityAction action; //回调函数
    protected virtual void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
        if (canvas == null)
        {
            canvas = gameObject.AddComponent<CanvasGroup>();
            Debug.LogWarning($"CanvasGroup not found on {gameObject.name}. Added automatically.", this);
        }
    }

    public abstract void Init();
    protected virtual void Start()
    {
        Init();
    }
    
    public virtual void Show()
    {
        canvas.alpha = 0;
        isShow = true;
    }

    public virtual void Hide(UnityAction action)
    {
        canvas.alpha = 1;
        isShow = false;
        this.action = action;//执行淡出动作后，执行回调函数
    }
    /// <summary>
    /// 每帧更新
    /// </summary>
    void Update()
    {

        if (isShow)
        {
            if (canvas.alpha < 1)
            {
                canvas.alpha = Mathf.Clamp(canvas.alpha + Time.deltaTime * fadeTime, 0, 1);
            }
        }
        else
        {
            if (canvas.alpha > 0)
            {
                canvas.alpha = Mathf.Clamp(canvas.alpha - Time.deltaTime * fadeTime, 0, 1);
                if (canvas.alpha <= 0)
                    this.action?.Invoke();
            }
        }
        
    }

}
