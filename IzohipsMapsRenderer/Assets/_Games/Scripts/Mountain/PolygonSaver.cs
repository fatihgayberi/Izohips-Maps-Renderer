using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wonnasmith
{
    public class PolygonSaver : MonoBehaviour
    {
        public delegate void PolygonSaver_MountSaveData(Mount mount, Texture2D texture2D);
        public static event PngDataController.PngDataController_PngDataSave PngDataSave;

        //============================================================================

        private void OnEnable()
        {
            PolygonFinder.MountSaveData += OnMountSaveData;
        }

        private void OnDisable()
        {
            PolygonFinder.MountSaveData -= OnMountSaveData;
        }

        //============================================================================

        private void OnMountSaveData(Mount mount, Texture2D texture2D)
        {
            PngDataSave?.Invoke(mount, texture2D);
        }

        //============================================================================
    }
}