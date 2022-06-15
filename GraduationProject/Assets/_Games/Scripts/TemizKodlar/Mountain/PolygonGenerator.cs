using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wonnasmith
{
    public class PolygonGenerator : MonoBehaviour
    {
        [SerializeField] private Color blackColor;
        [SerializeField] private Color whiteColor;

        [SerializeField] private Texture2D texture2D;

        enum LineForwardType
        {
            /// <summary> Baştan sona </summary>
            Fornt2End,

            /// <summary> Sondan başa </summary>
            End2Fornt
        }

        class ElementIdxData
        {
            /// <summary> satır </summary>
            public int row = -1;

            /// <summary> sütun </summary>
            public int column = -1;
        }

        class LineData
        {
            public List<ElementIdxData> elementList;
        }

        private List<LineData> front2End_LineList = new List<LineData>();
        private List<LineData> end2Front_LineList = new List<LineData>();

        //============================================================================

        private void OnEnable()
        {
            ButtonManager.TextureColorDetection += OnTextureColorDetection;
        }

        private void OnDisable()
        {
            ButtonManager.TextureColorDetection -= OnTextureColorDetection;
        }

        //============================================================================

        private void OnTextureColorDetection()
        {
            TextureDetection();
        }

        //============================================================================

        private void TextureDetection()
        {
            Front2EndMove();
        }

        //============================================================================

        private void Front2EndMove()
        {
            if (texture2D == null)
            {
                return;
            }

            if (front2End_LineList == null)
            {
                return;
            }

            int textureRow = texture2D.height;
            int textureColumn = texture2D.width;

            for (int row = 0; row < textureRow; row++)
            {
                LineData newLine = new LineData();

                for (int column = 0; column < textureColumn; column++)
                {
                    if (IsPixelWhite(row, column))
                    {
                        ElementIdxData elementIdxData = new ElementIdxData();

                        elementIdxData.row = row;
                        elementIdxData.column = column;

                        if (elementIdxData != null)
                        {
                            newLine.elementList.Add(elementIdxData);
                        }
                    }
                }

                front2End_LineList.Add(newLine);
            }
        }

        //============================================================================

        /// <summary>  </summary>
        private bool IsPixelWhite(int row, int column)
        {
            if (texture2D == null)
            {
                return false;
            }

            if (row == -1)
            {
                return false;
            }

            if (column == -1)
            {
                return false;
            }

            Color pixelColor = texture2D.GetPixel(row, column);

            if (pixelColor == whiteColor)
            {
                return true;
            }

            return false;
        }
    }
}