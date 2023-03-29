using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texture2DToFileStorageMono : MonoBehaviour
{
    public Eloi.AbstractMetaAbsolutePathFileMono m_whereToStore;

    public void SaveTextureAtDestination(Texture2D texture) {

        Eloi.E_FileAndFolderUtility.ExportTextureAsPNG(m_whereToStore, texture, true, true);
    }
}
