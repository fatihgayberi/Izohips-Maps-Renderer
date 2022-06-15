using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Wonnasmith
{
    [Serializable]
    public class ButtonManager : MonoBehaviour
    {
        public delegate void PlaneResizeButtonClick();
        public static event /*ButtonManager.*/PlaneResizeButtonClick planeResizeButtonClick;

        public delegate void TextureColorDetectionButtonClick();
        public static event /*ButtonManager.*/TextureColorDetectionButtonClick TextureColorDetection;

        [Serializable]
        public class BUTTON_MANAGER_Buttons
        {
            public KeyCode textureColorDetection_Button;
            public KeyCode planeResize_Button;
        }

        public BUTTON_MANAGER_Buttons Button_Manager_Buttons;

        //============================================================================

        private void Update()
        {
            Test_PlaneResize(); //R
        }

        //============================================================================

        private void Test_PlaneResize()
        {
            if (Input.GetKeyDown(Button_Manager_Buttons.planeResize_Button))
            {
                planeResizeButtonClick?.Invoke();

                Debug.Log("<color=blue>:::PlaneResizeButtonClick:::</color>");
            }
        }

        //============================================================================
    }
}