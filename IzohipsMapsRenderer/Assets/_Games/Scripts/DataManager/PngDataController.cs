using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Wonnasmith
{
    [Serializable]
    public class PngDataController : MonoBehaviour
    {
        public delegate void PngDataController_PngDataSave(Mount mountDatas, Texture2D texture2D);

        public struct JsonFileName
        {
            private const string allPngDatasJsonFileName = "/AllPngDatas.json";

            public string GetAllPngDatasJsonFileName() { return allPngDatasJsonFileName; }
        }

        public DataManager dataManager;

        public JsonFileName jsonFileName;

        public class PngData
        {
            public List<MountainDatas> mountainDatasList = new List<MountainDatas>();
        }

        [Serializable]
        public class MountainDatas
        {
            public string saveTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            public string pngName;
            public int pngWidth;
            public int pngHeight;

            public Mount mount;

            [Serializable]
            public class MountainPolygon
            {
                public int[,] whitePoint;
            }
        }

        private void OnEnable()
        {
            PolygonSaver.PngDataSave += OnPngDataSave;
        }

        private void OnDisable()
        {
            PolygonSaver.PngDataSave -= OnPngDataSave;
        }

        private void OnPngDataSave(Mount mountDatas, Texture2D texture2D)
        {
            dataManager = GetComponent<DataManager>();

            MountainDatas mountainDatas = new MountainDatas();

            PngData pngData = LoadPngDatas();

            if (pngData == null)
            {
                pngData = new PngData();
            }

            mountainDatas.mount = mountDatas;
            mountainDatas.pngName = texture2D.name;
            mountainDatas.pngWidth = texture2D.width;
            mountainDatas.pngHeight = texture2D.height;

            pngData.mountainDatasList.Add(mountainDatas);

            dataManager = GetComponent<DataManager>();
            dataManager.SetDatas(pngData, jsonFileName.GetAllPngDatasJsonFileName());
        }

        private PngData LoadPngDatas()
        {
            dataManager = GetComponent<DataManager>();
            return dataManager.GetDatas<PngData>(jsonFileName.GetAllPngDatasJsonFileName());
        }

    }
}
