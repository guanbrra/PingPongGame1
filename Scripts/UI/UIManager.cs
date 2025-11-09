using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance;
    public static UIManager Instance => instance ?? (instance = new UIManager());

    private Transform canvas;
    private UIManager() 
    {
        canvas = GameObject.Find("Canvas").transform;
        GameObject.DontDestroyOnLoad(canvas.gameObject);
    }

    private Dictionary<string, BasePanel> panels = new Dictionary<string, BasePanel>();

    public T ShowPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panels.ContainsKey(panelName))
            return panels[panelName] as T;

        //Debug.Log($"ShowPanel:ArtRes/Perfebs/UI/{panelName}");
        GameObject panelPrefab = GameObject.Instantiate(Resources.Load<GameObject>("ArtRes/Perfebs/UI/" + panelName));
        panelPrefab.transform.SetParent(canvas, false);

        T Panel = panelPrefab.GetComponent<T>();
        panels.Add(panelName, Panel);
        Panel.Show();

        return Panel;
    }

    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panels.ContainsKey(panelName))
        {
            if (isFade)
            {
                panels[panelName].Hide(() =>
                {
                    GameObject.Destroy(panels[panelName].gameObject);
                    Debug.Log("HidePanel");
                    panels.Remove(panelName);
                });
                
            }
            else
            {
                GameObject.Destroy(panels[panelName].gameObject);
                panels.Remove(panelName);
            }
        }

    }

    public T GetPanel<T>() where T : BasePanel
    {
        
        string panelName = typeof(T).Name;
        if (panels.ContainsKey(panelName))
        {
            return panels[panelName] as T;
        }
        Debug.Log("GetPanel");
        return null;
    }

}
