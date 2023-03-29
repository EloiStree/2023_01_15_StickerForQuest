using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExportPagesQuestLabelAsCoroutineMono : MonoBehaviour
{
    public SetupQuestLabelPageWithIndexManagerMono m_pageSetupManager;
    public float m_timeBetweenScreenshot = 2;
    public int m_startIndex;
    public int m_endIndex;
    public UnityEvent m_takePicture;

    public bool m_useBorder;

    public Coroutine m_coroutine;
    public Color m_borderColor = Color.magenta; 



    [ContextMenu("Export all pages")]
    public void ExportAllPages()
    {
        if(m_coroutine!=null)
            StopCoroutine(m_coroutine);
         m_coroutine = StartCoroutine(ExportAllPagesCoroutine());
    }

    public IEnumerator ExportAllPagesCoroutine() {

        m_pageSetupManager.GetPageCountFor(m_startIndex, m_endIndex, out int pagesCount);
        m_pageSetupManager.SetStartIndexTo(m_startIndex);

        m_pageSetupManager.DisplayContent(false);
        m_pageSetupManager.DisplayBorder(true);
        yield return new WaitForSeconds(m_timeBetweenScreenshot);
        m_takePicture.Invoke();
        yield return new WaitForSeconds(m_timeBetweenScreenshot);

        
        m_pageSetupManager.DisplayContent(true);
        m_pageSetupManager.DisplayBorder(m_useBorder);
        m_pageSetupManager.SetBorderColor(m_borderColor);

        for (int i = 0; i < pagesCount; i++)
        {
            m_pageSetupManager.SetToPageInt(i);
            yield return new WaitForSeconds(m_timeBetweenScreenshot);
            m_takePicture.Invoke();
        }

    }

}
