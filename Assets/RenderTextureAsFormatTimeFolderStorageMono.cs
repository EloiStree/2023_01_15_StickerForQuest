using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;


public class RenderTextureAsFormatTimeFolderStorageMono : MonoBehaviour
{
    public RenderTexture m_texture;
    public Eloi.AbstractMetaAbsolutePathDirectoryMono m_whereToStore;

    public string m_fileName="DefaultName";
    public string m_timeFormat= "yyyyMMddTHHmmss";

    public bool m_bitmap = true;
    public bool m_linear = true;

    [ContextMenu("Save Texture")]
    public void SaveTextureAtDestination()
    {
        Eloi.E_Texture2DUtility.RenderTextureToTexture2D(in m_texture, out Texture2D t);
        SaveTextureAtDestination(t);

    }

    public void SaveTextureAtDestination(Texture2D texture)
    {
        string fileName = m_fileName + DateTime.Now.ToString(m_timeFormat);

        Eloi.IMetaAbsolutePathFileGet file = Eloi.E_FileAndFolderUtility.Combine(m_whereToStore,
            new Eloi.MetaFileNameWithExtension(fileName, "png"));

        Eloi.E_FileAndFolderUtility.ExportTextureAsPNG(file, texture, true, true);
    }
}
