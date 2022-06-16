using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wonnasmith
{
    public class PolygonSaver : MonoBehaviour
    {
        public delegate void PolygonSaver_PolygonSaveData(List<PolygonFinder.LineForwardData> lineForwardDataList);
        public delegate void PolygonSaver_MountSaveData(PolygonFinder.Mount mount);

        [SerializeField] private Transform polygonParentTR;
        [SerializeField] private GameObject testObj;
        [SerializeField] private float height;

        private List<Vector3> positionList = new List<Vector3>();

        private int lineNum = 1;

        //============================================================================

        private void OnEnable()
        {
            PolygonFinder.PolygonSaveData += OnPolygonSaveData;
        }

        private void OnDisable()
        {
            PolygonFinder.PolygonSaveData -= OnPolygonSaveData;
        }

        //============================================================================

        private void OnPolygonSaveData(List<PolygonFinder.LineForwardData> lineForwardDataList)
        {
            int forwardTypeCount = System.Enum.GetValues(typeof(PolygonFinder.LineForwardType)).Length;

            for (int i = 0; i < forwardTypeCount; i++)
            {
                PolygonFinder.LineForwardType lineForwardType = (PolygonFinder.LineForwardType)i;

                PolygonFinder.LineForwardData lineForwardData = GetAllLineDatasList(lineForwardDataList, (PolygonFinder.LineForwardType)i);

                if (lineForwardData != null)
                {
                    LineDataTestObjectGenerator(lineForwardData.LineDatasList, lineNum);
                }
            }

            lineNum++;
        }

        //============================================================================

        private PolygonFinder.LineForwardData GetAllLineDatasList(List<PolygonFinder.LineForwardData> lineForwardDataList, PolygonFinder.LineForwardType forwardType)
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

        private void LineDataTestObjectGenerator(List<PolygonFinder.LineData> lineDataList, int lineNum)
        {
            if (lineDataList == null)
            {
                return;
            }

            int lineDataListCount = lineDataList.Count;
            int elementListCount = 0;

            PolygonFinder.ElementIdxData elementIdxData = null;

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

        private Vector3 Pixel2Vector3Position(PolygonFinder.ElementIdxData elementIdxData)
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