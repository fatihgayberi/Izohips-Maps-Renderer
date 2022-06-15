using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MountainDatas = Wonnasmith.PngDataController.MountainDatas;
using MountainPolygon = Wonnasmith.PngDataController.MountainDatas.MountainPolygon;

namespace Wonnasmith
{
    public class PolygonSaver : MonoBehaviour
    {
        public delegate void PolygonSaver_PolygonSaveData(List<PolygonFinder.LineData> front2End_LineList, List<PolygonFinder.LineData> end2Front_LineList);

        [SerializeField] private UnityEngine.PrimitiveType primitiveTestObject;
        // [SerializeField] private List<GameObject> polygonParent;
        [SerializeField] private Transform polygonParentTR;
        [SerializeField] private GameObject testObj;

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

        private void OnPolygonSaveData(List<PolygonFinder.LineData> front2End_LineList, List<PolygonFinder.LineData> end2Front_LineList)
        {
            LineDataTestObjectGenerator(front2End_LineList);
            LineDataTestObjectGenerator(end2Front_LineList);
        }

        //============================================================================

        private void LineDataTestObjectGenerator(List<PolygonFinder.LineData> lineDataList)
        {
            if (lineDataList == null)
            {
                return;
            }

            int lineDataListCount = lineDataList.Count;
            int elementListCount = 0;

            PolygonFinder.ElementIdxData elementIdxData = null;
            // Debug.Log("LineDataTestObjectGenerator_BAŞLADI");

            for (int i = 0; i < lineDataListCount; i++)
            {
                if (lineDataList[i] != null)
                {
                    // Debug.Log("LineDataTestObjectGenerator_1");

                    if (lineDataList[i].elementList != null)
                    {
                        if (lineDataList[i].elementList.Count > 0)
                        {
                            // Debug.Log("LineDataTestObjectGenerator_2");

                            elementListCount = lineDataList[i].elementList.Count;

                            for (int j = 0; j < elementListCount; j++)
                            {
                                if (lineDataList[i].elementList[j] != null)
                                {
                                    // Debug.Log("LineDataTestObjectGenerator_3");

                                    elementIdxData = lineDataList[i].elementList[j];

                                    if (elementIdxData != null)
                                    {
                                        Vector3 pos = Pixel2Vector3Position(lineDataList[i].elementList[j]);

                                        TestObjectGenerator(pos);

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

        private void TestObjectGenerator(Vector3 pos)
        {
            Instantiate(testObj, pos, Quaternion.identity, polygonParentTR);
        }

        //============================================================================
    }
}