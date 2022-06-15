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
        public delegate void PolygonSaver_PolygonSaveData(List<PolygonFinder.LineForwardData> lineForwardDataList);

        [SerializeField] private UnityEngine.PrimitiveType primitiveTestObject;
        // [SerializeField] private List<GameObject> polygonParent;

        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform polygonParentTR;
        [SerializeField] private GameObject testObj;

        private List<Vector3> positionList = new List<Vector3>();

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
                    LineDataTestObjectGenerator(lineForwardData.LineDatasList);
                }
            }
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

                                        // positionList.Add(pos);
                                        TestObjectGenerator(pos, lineDataList[i].elementList[j].row, lineDataList[i].elementList[j].column);

                                        elementIdxData = null;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // LineRendererGenerator();
        }

        //============================================================================

        private Vector3 Pixel2Vector3Position(PolygonFinder.ElementIdxData elementIdxData)
        {
            return new Vector3(elementIdxData.column, 0, elementIdxData.row);
        }

        //============================================================================

        private void LineRendererGenerator()
        {
            if (lineRenderer == null)
            {
                return;
            }

            lineRenderer.positionCount = positionList.Count;

            lineRenderer.SetPositions(positionList.ToArray());
        }

        //============================================================================

        private void TestObjectGenerator(Vector3 pos, int row, int column)
        {
            Instantiate(testObj, pos, Quaternion.identity, polygonParentTR).name = "[" + row + "]::[" + column + "]";
        }

        //============================================================================
    }
}