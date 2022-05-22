using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[ExecuteInEditMode]
public class TextureSaver : MonoBehaviour
{
    public RenderTexture ResultTexture;
    public int SizeWidth = 2048;
    public int SizeHeight = 2048;

    public Material ReplaceMaterial;
    public bool DoBake = false;
    
    void Update()
    {
        if (DoBake)
        {
            DoBake = false;
            Bake();
        }
    }

    // Use this for initialization
    public void Bake()
    {
        if (ResultTexture == null)
        {
            ResultTexture = new RenderTexture(SizeWidth, SizeHeight, 0);
            ResultTexture.name = "Baked Texture";
        }

        bakeTexture();

        if (ReplaceMaterial != null)
        {
            GetComponent<Renderer>().material = ReplaceMaterial;
            ReplaceMaterial.mainTexture = ResultTexture;
        }
    }

    void bakeTexture()
    {
        var renderer = GetComponent<Renderer>();
        var material = Instantiate(renderer.material);
        Graphics.Blit(material.mainTexture, ResultTexture, material);

        Texture2D frame = new Texture2D(ResultTexture.width, ResultTexture.height);
        frame.ReadPixels(new Rect(0, 0, ResultTexture.width, ResultTexture.height), 0, 0, false);
        frame.Apply();
        byte[] bytes = frame.EncodeToPNG();
        FileStream file = File.Open(@"C:\Works.png", FileMode.Create);
        BinaryWriter binary = new BinaryWriter(file);
        binary.Write(bytes);
        file.Close();
    }
}
