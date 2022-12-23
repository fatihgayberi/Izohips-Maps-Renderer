using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[ExecuteInEditMode]
public class TextureSaver : MonoBehaviour
{
    [SerializeField] RenderTexture rt;

    [SerializeField] Camera cam;

    [SerializeField] string path;

    public bool testSave;

    private void Update()
    {
        if (testSave)
        {
            testSave = false;

            SavePNG();
        }
    }

    public void SavePNG()
    {
        RenderTexture mRt = new RenderTexture(rt.width, rt.height, rt.depth, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
        mRt.antiAliasing = rt.antiAliasing;

        var tex = new Texture2D(mRt.width, mRt.height, TextureFormat.ARGB32, false);
        cam.targetTexture = mRt;
        cam.Render();
        RenderTexture.active = mRt;

        tex.ReadPixels(new Rect(0, 0, mRt.width, mRt.height), 0, 0);
        tex.Apply();

        string fileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

        var newPath = path + fileName + ".png";
        File.WriteAllBytes(newPath, tex.EncodeToPNG());
        Debug.Log("Saved file to: " + newPath);

        DestroyImmediate(tex);

        cam.targetTexture = rt;
        cam.Render();
        RenderTexture.active = rt;

        DestroyImmediate(mRt);
    }
}
