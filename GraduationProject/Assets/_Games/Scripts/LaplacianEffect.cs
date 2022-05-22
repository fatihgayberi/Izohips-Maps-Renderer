using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LaplacianEffect : MonoBehaviour
{
    public Material edgeDetectionMaterial;

    public bool fotoClick;


    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, edgeDetectionMaterial);

        // Debug.Log("destination::" + destination + " ~~~~ " + "source::" + source);

        // rt = destination;

        if (fotoClick)
        {
            SaveTexture();
            fotoClick = false;
        }
    }

    public RenderTexture rt;
    // Use this for initialization
    public void SaveTexture()
    {
        byte[] bytes = toTexture2D(rt).EncodeToPNG();
        System.IO.File.WriteAllBytes("C:/Users/Gayberi/SavedScreen.png", bytes);
    }

    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

}
