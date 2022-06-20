using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wonnasmith
{
    public class UIImageController : MonoBehaviour
    {
        public delegate void UIImageController_TextureChange(Texture2D texture2D);

        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Image uıImage;

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

        private void OnTextureChange(Texture2D texture2D)
        {
            if (uıImage == null)
            {
                return;
            }

            if (texture2D == null)
            {
                return;
            }

            Rect rec = new Rect(0, 0, texture2D.width, texture2D.height);
            uıImage.sprite = Sprite.Create(texture2D, rec, new Vector2(0.5f, 0.5f), 100);
        }
    }
}
