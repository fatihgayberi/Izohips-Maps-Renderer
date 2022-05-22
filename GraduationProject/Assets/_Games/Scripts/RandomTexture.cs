using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class RandomTexture : MonoBehaviour
{

    void Start()
    {
        BuildQuad();
        BuildTexture();
    }

    void BuildTexture()
    {

        Texture2D texture = new Texture2D(16, 16, TextureFormat.RGBA32, false);

        // fill texture with random color
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                texture.SetPixel(x, y, Random.Range(0, 2) == 0 ? Color.red : Color.blue);
            }
        }
        texture.Apply();

        // create a material holding the texture
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material material = new Material(Shader.Find("Custom/HeightShader"));
        material.color = Color.white;
        material.SetTexture("texture", texture);

        // apply material to mesh
        meshRenderer.sharedMaterial = material;
        GetComponent<Renderer>().material.mainTexture = texture;
    }

    void BuildQuad()
    {
        Mesh mesh = new Mesh();
        // [...]
        // create vertices, triangles, normals and uv for a single quad

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }
}