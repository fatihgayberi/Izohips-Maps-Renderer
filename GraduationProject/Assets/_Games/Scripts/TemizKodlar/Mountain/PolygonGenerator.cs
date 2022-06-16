/// <summary>  </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wonnasmith
{
    public class PolygonGenerator : MonoBehaviour
    {
        public delegate void PolygonGenerator_PolygonGenerateData(List<LineForwardData> lineForwardDataList);
        public delegate void PolygonGenerator_MountSaveData(Mount mount);

        [SerializeField] private Transform polygonParentTR;
        [SerializeField] private GameObject testObj;
        [SerializeField] private float height;

        private List<Vector3> positionList = new List<Vector3>();

        private int lineNum = 1;

        //============================================================================

        private void OnEnable()
        {
            PolygonFinder.PolygonGenerateData += OnPolygonGenerateData;
        }

        private void OnDisable()
        {
            PolygonFinder.PolygonGenerateData -= OnPolygonGenerateData;
        }

        //============================================================================

        private void OnPolygonGenerateData(List<LineForwardData> lineForwardDataList)
        {
            int forwardTypeCount = System.Enum.GetValues(typeof(LineForwardType)).Length;

            for (int i = 0; i < forwardTypeCount; i++)
            {
                LineForwardType lineForwardType = (LineForwardType)i;

                LineForwardData lineForwardData = GetAllLineDatasList(lineForwardDataList, (LineForwardType)i);

                if (lineForwardData != null)
                {
                    LineDataTestObjectGenerator(lineForwardData.LineDatasList, lineNum);
                }
            }

            lineNum++;
        }

        //============================================================================

        private LineForwardData GetAllLineDatasList(List<LineForwardData> lineForwardDataList, LineForwardType forwardType)
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

        private void LineDataTestObjectGenerator(List<LineData> lineDataList, int lineNum)
        {
            if (lineDataList == null)
            {
                return;
            }

            int lineDataListCount = lineDataList.Count;
            int elementListCount = 0;

            ElementIdxData elementIdxData = null;

            for (int i = 0; i < lineDataListCount; i++)
            {
                if (lineDataList[i] != null)
                {
                    if (lineDataList[i].elementList != null)
                    {
                        if (lineDataList[i].elementList.Count > 0)
                        {
                            elementListCount = lineDataList[i].elementList.Count;

                            for (int j = 0; j < elementListCount; j++)
                            {
                                if (lineDataList[i].elementList[j] != null)
                                {
                                    elementIdxData = lineDataList[i].elementList[j];

                                    if (elementIdxData != null)
                                    {
                                        Vector3 pos = Pixel2Vector3Position(lineDataList[i].elementList[j]);
                                        pos.y = lineNum * height;

                                        TestObjectGenerator(pos, lineDataList[i].elementList[j].row, lineDataList[i].elementList[j].column);

                                        elementIdxData = null;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //============================================================================

        private Vector3 Pixel2Vector3Position(ElementIdxData elementIdxData)
        {
            return new Vector3(elementIdxData.column, 0, elementIdxData.row);
        }

        //============================================================================

        private void TestObjectGenerator(Vector3 pos, int row, int column)
        {
            Instantiate(testObj, pos, Quaternion.identity, polygonParentTR).name = "[" + row + "]::[" + column + "]";
        }

        //============================================================================
    }
}