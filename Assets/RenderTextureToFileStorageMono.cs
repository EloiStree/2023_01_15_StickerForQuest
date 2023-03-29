using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureToFileStorageMono : MonoBehaviour
{
    public RenderTexture m_texture;
    public Eloi.AbstractMetaAbsolutePathFileMono m_whereToStore;
    public bool m_bitmap = true;
    public bool m_linear=true;

    [ContextMenu("Save Texture")]
    public void SaveTextureAtDestination() {
        Eloi.E_Texture2DUtility.RenderTextureToTexture2D(in m_texture, out Texture2D t);
        SaveTextureAtDestination(t);
    }

    public void SaveTextureAtDestination(Texture2D texture)
    {

        Eloi.E_FileAndFolderUtility.ExportTextureAsPNG(m_whereToStore, texture, true, true);
    }
}
