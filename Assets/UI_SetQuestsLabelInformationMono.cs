using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class UI_SetQuestsLabelInformationMono : MonoBehaviour
{

    public UI_SetQuestID[] m_questIdtoSet;
    public string m_questContext="";
    public int m_startIndex=0;
    public bool m_setAtAwake;
    private void Awake()
    {
        if(m_setAtAwake)    
            SetQuestsIdFromIndex(m_startIndex);
    }
    public void SetContext(string context) {
        m_questContext = context;
        for (int i = 0; i < m_questIdtoSet.Length; i++)
        {
            m_questIdtoSet[i].SetContext(context);
            m_questIdtoSet[i].Encode();
        }
    }


    public void SetQuestsIdFromIndex(int index) {

        m_startIndex = index;
        for (int i = 0; i < m_questIdtoSet.Length; i++)
        {
            m_questIdtoSet[i].SetHeadsetIndex(index);
            index++;
            m_questIdtoSet[i].Encode();

        }

    }

    [ContextMenu("Refresh with Inspector")]
    private void RefreshWithInspector()
    {
        SetContext(m_questContext);
        SetQuestsIdFromIndex(m_startIndex);
    }

    [ContextMenu("Fetch script in scene")]
    public void FertchScriptOfQuestInScene() {
        Eloi.E_SearchInSceneUtility.TryToFetchWithActiveInScene(ref m_questIdtoSet);
    }

    public void GetQuestsLabelCount(out int labelCount)
    {
        labelCount = m_questIdtoSet.Length;
    }

    internal void DisplayAllContent(bool displayAllContent)
    {
        foreach (var item in m_questIdtoSet) {
            item.DisplayContent(displayAllContent);
        }
    }

    internal void DisplayAllBorder(bool border)
    {
        foreach (var item in m_questIdtoSet)
        {
            item.DisplayBorder(border);
        }
    }

    internal void SetBorderColor(Color borderColor)
    {
        foreach (var item in m_questIdtoSet)
        {
            item.SetBorderColor(borderColor);
        }
    }
}
