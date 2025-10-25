using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GlobalPlot : MonoBehaviour
{

    [SerializeField] private int level = 0;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }


    private void OnMouseDown()
    {
        if (!(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()))
        {
            return;
        }
        
        GlobalMap.main.ChangeLevel(level);
    }
}
