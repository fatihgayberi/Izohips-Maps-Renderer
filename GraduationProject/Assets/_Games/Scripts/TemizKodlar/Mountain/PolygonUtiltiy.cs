using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Wonnasmith
{
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    //PngPixels=================================================================//
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    #region "Png üzerindeki pixelleri temsil eder"
    [Serializable]
    public class ElementIdxDataa
    {
        /// <summary> satır </summary>
        public int row = -1;

        /// <summary> sütun </summary>
        public int column = -1;
    }
    #endregion
    //============================================================================



    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    //PngLines==================================================================//
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    #region "Png üzerineki satırları tutan Class"
    [Serializable]
    public class LineDataa
    {
        public List<ElementIdxDataa> elementList;
    }
    #endregion
    //============================================================================
}