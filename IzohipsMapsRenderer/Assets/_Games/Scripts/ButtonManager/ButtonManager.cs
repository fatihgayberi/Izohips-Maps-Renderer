using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Wonnasmith
{
    [Serializable]
    public class ButtonManager : MonoBehaviour
    {
        // public delegate void ButtonManager_PlaneResizeButtonClick();
        // public static event /*ButtonManager.*/ButtonManager_PlaneResizeButtonClick PlaneResizeButtonClick;

        public delegate void ButtonManager_TextureColorPolygonFinderButtonClick();
        public static event /*ButtonManager.*/ButtonManager_TextureColorPolygonFinderButtonClick PolygonFinderButtonClick;

        public delegate void ButtonManager_PolygonSimuleButtonClick();
        public static event /*ButtonManager.*/ButtonManager_PolygonSimuleButtonClick PolygonSimuleButtonClick;
      
        public delegate void ButtonManager_PolygonsSaveButtonClick();
        public static event /*ButtonManager.*/ButtonManager_PolygonsSaveButtonClick PolygonSaveButtonClick;

        [Serializable]
        public class BUTTON_MANAGER_Buttons
        {
            public KeyCode polygonFinder_BUTTON;
            public KeyCode polygonSimule_BUTTON;
            public KeyCode polygonsSave_BUTTON;
        }

        public BUTTON_MANAGER_Buttons Button_Manager_Buttons;

        //============================================================================

        private void Update()
        {
            Test_PolygonSimule();
            Test_PolygonFinder();
            Test_PolygonsSave();
        }

        //============================================================================

        private void Test_PolygonSimule()
        {
            if (Input.GetKeyDown(Button_Manager_Buttons.polygonSimule_BUTTON))
            {
                PolygonSimuleButtonClick?.Invoke();

                Debug.Log("<color=blue>:::Test_PolygonSimule:::</color>");
            }
        }

        //============================================================================

        private void Test_PolygonFinder()
        {
            if (Input.GetKeyDown(Button_Manager_Buttons.polygonFinder_BUTTON))
            {
                PolygonFinderButtonClick?.Invoke();

                Debug.Log("<color=blue>:::Test_PolygonFinder:::</color>");
            }
        }

        //============================================================================

        private void Test_PolygonsSave()
        {
            if (Input.GetKeyDown(Button_Manager_Buttons.polygonsSave_BUTTON))
            {
                PolygonSaveButtonClick?.Invoke();

                Debug.Log("<color=blue>:::Test_PolygonsSave:::</color>");
            }
        }

        //============================================================================
    }
}