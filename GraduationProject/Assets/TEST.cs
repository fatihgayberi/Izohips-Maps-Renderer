using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wonnasmith
{
    public class TEST : MonoBehaviour
    {
        public static event UIImageController.UIImageController_TextureChange TextureChange;
        [SerializeField] private Texture2D texture2D;

        public int x;
        public int y;
        public Color selectColor;

        [SerializeField] private Image uıImage;

        public bool isTEST;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isTEST)
            {
                isTEST = false;

                if (uıImage == null)
                {
                    return;
                }

                if (texture2D == null)
                {
                    return;
                }

                texture2D.SetPixel(x, y, selectColor);
                texture2D.Apply();

                Rect rec = new Rect(0, 0, texture2D.width, texture2D.height);
                uıImage.sprite = Sprite.Create(texture2D, rec, new Vector2(0.5f, 0.5f), 100);
            }

        }
    }
}