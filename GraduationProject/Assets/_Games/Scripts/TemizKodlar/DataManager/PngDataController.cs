using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Wonnasmith
{
    [Serializable]
    public class PngDataController : MonoBehaviour
    {
        public struct JsonFileName
        {
            private const string allPngDatasJsonFileName = "/AllPngDatas.json";

            public string GetAllPngDatasJsonFileName() { return allPngDatasJsonFileName; }
        }

        public DataManager dataManager;

        public JsonFileName jsonFileName;

        public class PngData
        {
            public List<MountainDatas> mountainDatasList;
        }

        [Serializable]
        public class MountainDatas
        {
            public string saveTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            public string pngName;
            public int pngWidth;
            public int pngHeight;

            public List<MountainPolygon> mountainPolygonList;

            [Serializable]
            public class MountainPolygon
            {
                public int[,] whitePoint;
            }
        }

        public void SavePngDatas(MountainDatas newMountainDatas)
        {
            PngData pngData = LoadPngDatas();

            if (pngData == null)
            {
                pngData = new PngData();
            }

            pngData.mountainDatasList.Add(newMountainDatas);

            dataManager = GetComponent<DataManager>();
            dataManager.SetDatas(pngData, jsonFileName.GetAllPngDatasJsonFileName());
        }

        private PngData LoadPngDatas()
        {
            dataManager = GetComponent<DataManager>();
            return dataManager.GetDatas<PngData>(jsonFileName.GetAllPngDatasJsonFileName());
        }

        public bool jsonParse;
        private void Update()
        {
            if (jsonParse)
            {
                jsonParse = false;
                MountainDatas newMountainDatas = new MountainDatas();
                SavePngDatas(newMountainDatas);
            }
        }
    }
}
