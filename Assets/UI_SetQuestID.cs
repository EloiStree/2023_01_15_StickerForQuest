using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static QRCodeEncodeController;

public class UI_SetQuestID : MonoBehaviour
{
    public QRCodeEncodeController m_qrControllerQR;
    public QRCodeEncodeController m_qrControllerBarcode;
    public Eloi.ClassicUnityEvent_Texture2D m_qrGeneratedUpdate;
    public Eloi.ClassicUnityEvent_Texture2D m_barcodeGeneratedUpdate;

    public Eloi.PrimitiveUnityEvent_String m_onContextChanged;
    public Eloi.PrimitiveUnityEvent_String m_onIndexChanged;
    public Eloi.PrimitiveUnityEvent_String m_onFullIdChanged;
    public Eloi.PrimitiveUnityEvent_Bool m_onShowContent;
    public Eloi.PrimitiveUnityEvent_Bool m_onShowBorder;

    public Eloi.ClassicUnityEvent_Color m_onBorderColorChanged;

    public UnityEvent m_finishToUpdate;
    public string m_contextId = "NoContext";
    public int m_headsetIndex = 0;

    public bool m_encodeAtAwake;

    public void Awake()
    {
        m_qrControllerQR.onQREncodeFinished += qrEncodeQRFinished;
        m_qrControllerBarcode.onQREncodeFinished += qrEncodeBarcodeFinished;
        if (m_encodeAtAwake)
        {
            Encode();
        }
    }
    [ContextMenu("Display both")]
    public void DisplayBoth()
    {
        DisplayBorder(true);
        DisplayContent(true);
    }
    [ContextMenu("Display Border")]
    public void DisplayBorder()
    {
        DisplayBorder(true);
        DisplayContent(false);
    }
    [ContextMenu("Display Content")]
    public void DisplayContent()
    {
        DisplayBorder(false);
        DisplayContent(true);
    }


    public void DisplayBorder(bool displayBorder)
    {
        m_onShowBorder.Invoke(displayBorder);
    }
    public void DisplayContent(bool displayContent)
    {

        m_onShowContent.Invoke(displayContent);
    }

    public void SetContext(string context)
    {
        m_contextId = context;
    }
    public void SetHeadsetIndex(int index)
    {
        m_headsetIndex = index;
    }
    public void SetHeadsetIndex(string indexAsText)
    {
       int.TryParse(indexAsText, out m_headsetIndex);
    }
  
    public bool m_qrGenereted;
    public bool m_barcodeGenereted;

    void qrEncodeQRFinished(Texture2D tex)
    {

        m_qrGenereted = true;
        m_qrGeneratedUpdate.Invoke(tex);

    }
    void qrEncodeBarcodeFinished(Texture2D tex)
    {
        m_barcodeGenereted = true;
        m_barcodeGeneratedUpdate.Invoke(tex);
    }

    [ContextMenu("Encode")]
    public void Encode()
    {
        m_qrGenereted = false;
        m_barcodeGenereted = false;

        if(Application.isPlaying)
            StartCoroutine(Coroutine_Encode());

    }

    public Color m_borderColorReceived;
    public void SetBorderColor(Color color) {

        m_borderColorReceived = color;
        m_onBorderColorChanged.Invoke(color);
    }




    public IEnumerator Coroutine_Encode()
    {
        m_qrGenereted = false;
        m_barcodeGenereted = false;

        string indexStr = string.Format("{0:000}", m_headsetIndex);
        string fullId = m_contextId + indexStr;
        m_onContextChanged.Invoke(m_contextId);
        m_onIndexChanged.Invoke(indexStr);
        m_onFullIdChanged.Invoke(fullId);
        m_qrControllerQR.Encode(fullId, QRCodeEncodeController.CodeMode.QR_CODE);
        m_qrControllerBarcode.Encode(fullId, QRCodeEncodeController.CodeMode.CODE_128);


        while (!m_qrGenereted && !m_barcodeGenereted) {
            yield return new WaitForEndOfFrame();
        }
        m_finishToUpdate.Invoke();
        yield break; 

    }

  


}
