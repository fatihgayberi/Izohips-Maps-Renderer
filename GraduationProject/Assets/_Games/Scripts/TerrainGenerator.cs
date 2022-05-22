using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int depth = 20;

    public int width = 256;
    public int height = 256;
    public float scale;

    public Terrain terrain;

    public Texture2D texture2D;


    public bool TEST;
    public bool myNoise;

    private void Update()
    {
        if (TEST)
        {
            TEST = false;

            Debug.Log("GenerateTerrain");
            terrain.terrainData = GenerateTerrain(terrain.terrainData);
        }
    }

    private TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (myNoise)
                {
                    heights[i, j] = CalculateHeights_MyNoise(i, j);
                }
                else
                {
                    heights[i, j] = CalculateHeights(i, j);
                }
            }
        }

        return heights;
    }

    private float CalculateHeights_MyNoise(int i, int j)
    {
        float xCoord = (float)texture2D.height / height * scale;
        float yCoord = (float)texture2D.width / width * scale;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    private float CalculateHeights(int i, int j)
    {
        float xCoord = (float)i / height * scale;
        float yCoord = (float)j / width * scale;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
