using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HowToPlay : MonoBehaviour
{

    public static  HowToPlay main;

    [Header("References")]
    [SerializeField] public TextMeshProUGUI[] sectionButtonTexts;
    [SerializeField] public Button sectionObjectivesButton;
    [SerializeField] public Button sectionResourcesButton;
    [SerializeField] public Button sectionBeesButton;
    [SerializeField] public Button sectionTowersButton;
    [SerializeField] public Button sectionEnemiesButton;
    [SerializeField] public Button sectionBuffsButton;

    [SerializeField] public TextMeshProUGUI informationTitleText;
    [SerializeField] public TextMeshProUGUI navitationText;
    [SerializeField] public Button navigationFarBackButton;
    [SerializeField] public Button navigationBackButton;
    [SerializeField] public Button navigationForwardButton;
    [SerializeField] public Button navigationFarForwardButton;

    [SerializeField] public GameObject[] sections;

    //Trackers
    private GameObject[][] sectionPages = new GameObject[][] { };
    private int sectionNumber = 0;
    private int pageNumber = 0;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        CloseUI();
        Array.Resize(ref sectionPages, sections.Length);
        for (int i = 0; i < sections.Length; i++)
        {
            GameObject[] pages = new GameObject[] { };
            List<GameObject> directChildren = new List<GameObject>();
            int childCount = sections[i].transform.childCount;
            for (int i2 = 0; i2 < childCount; i2++)
            {
                // Get the child Transform by index and access its GameObject
                GameObject child = sections[i].transform.GetChild(i2).gameObject;
                directChildren.Add(child);
            }
            Array.Resize(ref pages, childCount);
            for (int i2 = 0; i2 < pages.Length; i2++)
            {
                pages[i2] = directChildren[i2];
            }
            sectionPages[i] = pages;
        }
        float fontSize = 48f;
        for (int i = 0; i < sectionButtonTexts.Length; i++)
        {
            if (fontSize > sectionButtonTexts[i].fontSize)
            {
                fontSize = sectionButtonTexts[i].fontSize;
            }
        }
        for (int i = 0; i < sectionButtonTexts.Length; i++)
        {
            sectionButtonTexts[i].fontSize = fontSize;
        }
        UpdateSection();
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    private void UpdateSection()
    {
        pageNumber = 0;
        for (int i = 0; i < sections.Length; i++)
        {
            if (i == sectionNumber)
            {
                sections[i].SetActive(true);
            }
            else
            {
                sections[i].SetActive(false);
            }
        }
        informationTitleText.text = sectionButtonTexts[sectionNumber].text;
        UpdatePage();
    }

    private void UpdatePage()
    {
        for (int i = 0; i < sectionPages[sectionNumber].Length; i++)
        {
            if (i == pageNumber)
            {
                sectionPages[sectionNumber][i].SetActive(true);
            }
            else
            {
                sectionPages[sectionNumber][i].SetActive(false);
            }
        }
        navitationText.text = "Page " + (pageNumber + 1) + " of " + sectionPages[sectionNumber].Length;
    }

    public void ChangeSection(int i)
    {
        sectionNumber = i;
        UpdateSection();
    }

    public void ChangePage(int i)
    {
        if (i == 2)
        {
            pageNumber = sectionPages[sectionNumber].Length - 1;
        }
        else if (i == 1)
        {
            if (pageNumber < sectionPages[sectionNumber].Length - 1)
            {
                pageNumber++;
            }
        }
        else if (i == -1)
        {
            if (pageNumber > 0)
            {
                pageNumber--;
            }
        }
        else
        {
            pageNumber = 0;
        }
        UpdatePage();
    }
}
