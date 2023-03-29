using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupQuestLabelPageWithIndexManagerMono : MonoBehaviour
{
    public UI_SetQuestsLabelInformationMono m_pageManager;

    public Texture2D m_leftMiniLogo;
    public Texture2D m_rightMiniLogo;
    public Texture2D m_backgroundImage;

    public string m_contextId;
    public int m_startIndex;


    [Header("Debug")]
    public int m_labelToPrint;
    public int m_page;
    public int m_debug_startIndex;
    public int m_debug_endIndex;

    public void SetToPageInt(int pageIndex)
    {
        GetStartAndEndOfPageIndex(pageIndex, out int startIndexPage, out int endIndexPage);
        SetPageFromLabelIndexToLabelIndex(startIndexPage, endIndexPage);

    }

    private void GetStartAndEndOfPageIndex(int pageIndex, out int startIndexPage, out int endIndexPage)
    {
        m_pageManager.GetQuestsLabelCount(out int labelCount);
        int startIndexLocal = pageIndex * labelCount;
        startIndexPage = m_startIndex + startIndexLocal;
        endIndexPage = m_startIndex +  startIndexLocal + labelCount;
    }

    private void SetPageFromLabelIndexToLabelIndex(int startIndex, int endIndex)
    {
        m_pageManager.SetQuestsIdFromIndex(startIndex);
        m_debug_startIndex = startIndex;
        m_debug_endIndex = endIndex;
    }

    [ContextMenu("Set Page One")]
    public void SetToPageOne() => SetToPageInt(0);

    [ContextMenu("Set Page Two")]
    public void SetToPageTwo() => SetToPageInt(1);

    public void GetPageCountFor(int startIndex, int endIndex, out int pagesCount)
    {
        m_pageManager.GetQuestsLabelCount(out int labelcount);
        int itemCount = (endIndex - startIndex);
        pagesCount = 1 + (int) ( itemCount /(float) labelcount);
    }

    public void SetStartIndexTo(int startIndex)
    {
        m_startIndex = startIndex;

    }

    internal void DisplayContent(bool displayAllContent)
    {
        m_pageManager.DisplayAllContent(displayAllContent);
    }

    internal void DisplayBorder(bool displayBorder)
    {
        m_pageManager.DisplayAllBorder(displayBorder);
    }

    internal void SetBorderColor(Color borderColor)
    {
        m_pageManager.SetBorderColor(borderColor);
    }
}
