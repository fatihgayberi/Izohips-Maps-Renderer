using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wonnasmith
{
    public class TestQuadController : MonoBehaviour
    {
        public delegate void TestQuadController_TextureChange(Texture texture);

        [SerializeField] private MeshRenderer meshRenderer;

        //============================================================================

        private void OnEnable()
        {
            PolygonFinder.TextureChange += OnTextureChange;
        }

        private void OnDisable()
        {
            PolygonFinder.TextureChange -= OnTextureChange;
        }

        //============================================================================

        private void OnTextureChange(Texture texture)
        {
            if (meshRenderer == null)
            {
                return;
            }

            if (meshRenderer.material == null)
            {
                return;
            }

            Debug.Log("OnTextureChange::");

            meshRenderer.material.mainTexture = texture;
        }
    }
}
