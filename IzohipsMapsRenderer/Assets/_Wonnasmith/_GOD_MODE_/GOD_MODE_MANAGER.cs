#if UNITY_EDITOR
using UnityEngine;
using System;
using System.Reflection;
using UnityEditor;

namespace Wonnasmith
{
    [Serializable]
    public class GOD_MODE_MANAGER : Singleton<GOD_MODE_MANAGER>
    {
        [Serializable]
        public class God_Mode_Buttons
        {
            public KeyCode consoleClear_Button;
            public KeyCode planeResize_Button;
        }

        public God_Mode_Buttons god_Mode_Buttons;

        //============================================================================

        private void Update()
        {
            Test_ConsoleClear(); // W
        }

        //============================================================================

        private void Test_ConsoleClear()
        {
            if (Input.GetKeyDown(god_Mode_Buttons.consoleClear_Button))
            {
                var assembly = Assembly.GetAssembly(typeof(SceneView));
                var type = assembly.GetType("UnityEditor.LogEntries");
                var method = type.GetMethod("Clear");
                method.Invoke(new object(), null);

                Debug.Log("<color=red>:::WONNASMITH_IS_HERE <<[]>> GOD_MODE_ConsoleClear:::</color>");
            }
        }

        //============================================================================
    }
}
#endif