using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GrayTextureToEdgeDetection : MonoBehaviour
{
    public Material grayMaterial;
    public Material edgeMaterial;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("TEST_Update");

            Texture texture = grayMaterial.GetTexture("_MainTex");

            edgeMaterial.SetTexture("_MainTex", texture);
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Debug.Log("TEST");

        Texture texture = grayMaterial.GetTexture("_MainTex");

        edgeMaterial.SetTexture("_MainTex", texture);
    }
}
