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
        /// <summary> pixel </summary>
        public class ElementIdxData
        {
            /// <summary> satır </summary>
            public int row = -1;

            /// <summary> sütun </summary>
            public int column = -1;
        }

        [Serializable]
        /// <summary> yatay veya dikey şeritin kendisi </summary>
        public class LineData
        {
            /// <summary> şeritteki pixeller </summary>
            public List<ElementIdxData> elementList;
        }

        [Serializable]
        /// <summary> Tipine göre şeritler </summary>
        public class LineForwardData
        {
            /// <summary> şeritin çiziliş tipi </summary>
            public LineForwardType lineForwardType;

            /// <summary> şeritlerin listesi </summary>
            public List<LineData> LineDatasList;
        }

        /// <summary> Tipleri ile bütün şeritler </summary>
        [SerializeField] private List<LineForwardData> lineForwardDataList;

        public enum LineForwardType
        {
            /// <summary> Sagdan sola </summary>
            Right2Left,

            /// <summary> Soldan saga </summary>
            Left2Right,

            /// <summary> Yukardan aşağı </summary>
            Up2Down,

            /// <summary> Aşağıdan yukarı </summary>
            Down2Up,
        }

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
            PolygonSaveData?.Invoke(lineForwardDataList);
        }

        //============================================================================

        private void TextureDetection()
        {
            LineForwardMove();
        }

        //============================================================================

        private void LineForwardMove()
        {
            TextureMove_Right2Left(LineForwardType.Right2Left);
            TextureMove_Up2Down(LineForwardType.Up2Down);
            TextureMove_Left2Right(LineForwardType.Left2Right);
            TextureMove_Down2Up(LineForwardType.Down2Up);
        }

        //============================================================================

        private LineForwardData GetAllLineDatasList(LineForwardType forwardType)
        {
            if (lineForwardDataList == null)
            {
                return null;
            }

            int lineForwardDataListCount = lineForwardDataList.Count;

            for (int i = 0; i < lineForwardDataListCount; i++)
            {
                if (lineForwardDataList[i].lineForwardType == forwardType)
                {
                    return lineForwardDataList[i];
                }
            }

            return null;
        }

        //============================================================================

        private bool IsLineInAddedElement(ElementIdxData elementIdxData, LineForwardType currentLineForwardType)
        {
            int forwardTypeCount = System.Enum.GetValues(typeof(LineForwardType)).Length;

            for (int i = 0; i < forwardTypeCount; i++)
            {
                LineForwardType lineForwardType = (LineForwardType)i;

                if (currentLineForwardType != lineForwardType)
                {
                    LineForwardData lineForwardData = GetAllLineDatasList((LineForwardType)i);

                    if (lineForwardData != null)
                    {
                        int LineDatasListCount = lineForwardData.LineDatasList.Count;

                        for (int j = 0; j < LineDatasListCount; j++)
                        {
                            if (lineForwardData.LineDatasList[j] != null)
                            {
                                if (lineForwardData.LineDatasList[j].elementList != null)
                                {
                                    int elementListCount = lineForwardData.LineDatasList[j].elementList.Count;

                                    for (int k = 0; k < elementListCount; k++)
                                    {
                                        if (lineForwardData.LineDatasList[j].elementList[k] == elementIdxData)
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        //============================================================================

        /// <summary> verilen tipte png yi gezer </summary>
        private void TextureMove_Right2Left(LineForwardType lineForwardType)
        {
            if (texture2D == null)
            {
                return;
            }

            int textureRow = texture2D.height;
            int textureColumn = texture2D.width;

            LineForwardData lineForwardData = GetAllLineDatasList(lineForwardType);

            if (lineForwardData == null)
            {
                return;
            }

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
                            if (IsLineInAddedElement(elementIdxData, lineForwardType))
                            {
                                break;
                            }

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
                    lineForwardData.LineDatasList.Add(newLine);
                }
            }
        }

        //============================================================================

        /// <summary> verilen tipte png yi gezer </summary>
        private void TextureMove_Left2Right(LineForwardType lineForwardType)
        {
            if (texture2D == null)
            {
                return;
            }

            int textureRow = texture2D.height;
            int textureColumn = texture2D.width;

            LineForwardData lineForwardData = GetAllLineDatasList(lineForwardType);

            if (lineForwardData == null)
            {
                return;
            }

            for (int row = 0; row < textureRow; row++)
            {
                LineData newLine = null;

                for (int column = textureColumn; column >= 0; column--)
                {
                    if (IsPixelWhite(row, column))
                    {
                        ElementIdxData elementIdxData = new ElementIdxData();

                        elementIdxData.row = row;
                        elementIdxData.column = column;

                        if (elementIdxData != null)
                        {
                            if (IsLineInAddedElement(elementIdxData, lineForwardType))
                            {
                                break;
                            }

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
                    lineForwardData.LineDatasList.Add(newLine);
                }
            }
        }

        //============================================================================

        /// <summary> verilen tipte png yi gezer </summary>
        private void TextureMove_Up2Down(LineForwardType lineForwardType)
        {
            if (texture2D == null)
            {
                return;
            }

            int textureRow = texture2D.height;
            int textureColumn = texture2D.width;

            LineForwardData lineForwardData = GetAllLineDatasList(lineForwardType);

            if (lineForwardData == null)
            {
                return;
            }

            for (int column = 0; column < textureColumn; column++)
            {
                LineData newLine = null;

                for (int row = 0; row < textureRow; row++)
                {
                    if (IsPixelWhite(row, column))
                    {
                        ElementIdxData elementIdxData = new ElementIdxData();

                        elementIdxData.row = row;
                        elementIdxData.column = column;

                        if (elementIdxData != null)
                        {
                            if (IsLineInAddedElement(elementIdxData, lineForwardType))
                            {
                                break;
                            }

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
                    lineForwardData.LineDatasList.Add(newLine);
                }
            }
        }

        //============================================================================

        /// <summary> verilen tipte png yi gezer </summary>
        private void TextureMove_Down2Up(LineForwardType lineForwardType)
        {
            if (texture2D == null)
            {
                return;
            }

            int textureRow = texture2D.height;
            int textureColumn = texture2D.width;

            LineForwardData lineForwardData = GetAllLineDatasList(lineForwardType);

            if (lineForwardData == null)
            {
                return;
            }

            for (int column = textureColumn; column >= 0; column--)
            {
                LineData newLine = null;

                for (int row = textureRow; row > 0; row--)
                {
                    if (IsPixelWhite(row, column))
                    {
                        ElementIdxData elementIdxData = new ElementIdxData();

                        elementIdxData.row = row;
                        elementIdxData.column = column;

                        if (elementIdxData != null)
                        {
                            if (IsLineInAddedElement(elementIdxData, lineForwardType))
                            {
                                break;
                            }

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
                    lineForwardData.LineDatasList.Add(newLine);
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