using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wonnasmith
{
    [ExecuteInEditMode]
    public class CameraMaskController : MonoBehaviour
    {
        [SerializeField] private Material edgeDetectionMaterial;

        //============================================================================

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (edgeDetectionMaterial == null)
            {
                Debug.Log("Material:::NULL");
                return;
            }

            Graphics.Blit(source, destination, edgeDetectionMaterial);
        }
    }
}