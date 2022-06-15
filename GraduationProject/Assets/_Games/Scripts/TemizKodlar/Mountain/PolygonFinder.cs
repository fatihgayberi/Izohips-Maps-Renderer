using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wonnasmith
{
    public class PolygonFinder : MonoBehaviour
    {
        public static event PolygonSaver.PolygonSaver_PolygonSaveData PolygonSaveData;

        [SerializeField] private Color blackColor;
        [SerializeField] private Color whiteColor;

        [SerializeField] private Texture2D texture2D;

        [Serializable]
        public class ElementIdxData
        {
            /// <summary> satır </summary>
            public int row = -1;

            /// <summary> sütun </summary>
            public int column = -1;
        }

        [Serializable]
        public class LineData
        {
            public LineForwardType lineForwardType;
            public List<ElementIdxData> elementList;
        }

        [Serializable]
        class LineForwardData
        {
            public List<LineData> LineDatas;
        }

        [SerializeField] LineForwardData lineForwardData;

        public enum LineForwardType
        {
            /// <summary> Baştan sona </summary>
            Fornt2End,

            /// <summary> Sondan başa </summary>
            End2Fornt,

            /// <summary> Yukardan aşağı </summary>
            Up2Down,

            /// <summary> Aşağıdan yukarı </summary>
            Dow2Up,
        }

        [SerializeField] private List<LineData> _front2End_LineList = new List<LineData>();
        [SerializeField] private List<LineData> _end2Front_LineList = new List<LineData>();

        //============================================================================

        private void OnEnable()
        {
            ButtonManager.PolygonFinderButtonClick += OnPolygonFinderButtonClick;
            ButtonManager.PolygonSaveButtonClick += OnPolygonSaveButtonClick;
        }

        private void OnDisable()
        {
            ButtonManager.PolygonFinderButtonClick -= OnPolygonFinderButtonClick;
            ButtonManager.PolygonSaveButtonClick -= OnPolygonSaveButtonClick;
        }

        //============================================================================

        private void OnPolygonFinderButtonClick()
        {
            TextureDetection();
        }

        //============================================================================

        private void OnPolygonSaveButtonClick()
        {
            PolygonSaveData?.Invoke(_front2End_LineList, _end2Front_LineList);
        }

        //============================================================================

        private void TextureDetection()
        {
            Front2EndMove();
            End2ForntMove();
        }

        //============================================================================

        /// <summary> baştan sona satırları gezer </summary>
        private void Front2EndMove()
        {
            if (texture2D == null)
            {
                return;
            }

            int textureRow = texture2D.height;
            int textureColumn = texture2D.width;

            for (int row = 0; row < textureRow; row++)
            {
                LineData newLine = null;

                for (int column = 0; column < textureColumn; column++)
                {
                    if (IsPixelWhite(row, column))
                    {
                        ElementIdxData elementIdxData = new ElementIdxData();

                        elementIdxData.row = row;
                        elementIdxData.column = column;

                        if (elementIdxData != null)
                        {
                            newLine = new LineData();

                            if (newLine.elementList == null)
                            {
                                newLine.elementList = new List<ElementIdxData>();
                            }

                            newLine.elementList.Add(elementIdxData);
                        }

                        break;
                    }
                }

                if (newLine != null)
                {
                    _front2End_LineList.Add(newLine);
                }
            }
        }

        //============================================================================

        /// <summary> sondan başa satırları gezer </summary>
        private void End2ForntMove()
        {
            if (texture2D == null)
            {
                return;
            }

            int textureRow = texture2D.height;
            int textureColumn = texture2D.width;

            for (int row = 0; row < textureRow; row++)
            {
                LineData newLine = null;

                for (int column = textureColumn - 1; column >= 0; column--)
                {
                    if (IsPixelWhite(row, column))
                    {
                        ElementIdxData elementIdxData = new ElementIdxData();

                        elementIdxData.row = row;
                        elementIdxData.column = column;

                        if (elementIdxData != null)
                        {
                            newLine = new LineData();

                            if (newLine.elementList == null)
                            {
                                newLine.elementList = new List<ElementIdxData>();
                            }

                            newLine.elementList.Add(elementIdxData);
                        }

                        break;
                    }
                }


                if (newLine != null)
                {
                    _end2Front_LineList.Add(newLine);
                }
            }
        }

        //============================================================================

        /// <summary> pixelin renginin beyaz olup olmadığını kontrol eder </summary>
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