using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace Wonnasmith
{
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    //ListExtensionMethods======================================================//
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    #region "LIST<T> ile ilgili extension metodlar"
    public static class ListExtensionMethods
    {
        /// <summary>
        /// verilen listedeki elemanları karıştıp yenidien sıralar
        /// </summary>
        /// <param name="ts">liste</param>
        /// <typeparam name="T">listenin tipi</typeparam>
        public static void Shuffle<T>(this IList<T> ts)
        {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = UnityEngine.Random.Range(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }

        //......................................................................//

        /// <summary>
        /// Verilen listeyi clonelayıp return eder
        /// </summary>
        /// <param name="source"> clonelanacak liste </param>
        /// <typeparam name="T"> listenin tipi </typeparam>
        /// <returns></returns>
        public static List<T> GetClone<T>(this List<T> source)
        {
            return source.GetRange(0, source.Count);
        }
    }
    #endregion
    //============================================================================



    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    //ArrayExtensionMethods======================================================//
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    #region "T[] Array ile ilgili extension metodlar"
    public static class ArrayExtensionMethods
    {
        /// <summary>
        /// verilen array elemanını indexini return eder eğer listede yoksa -1 döner
        /// </summary>
        /// <param name="array"> aranacak array </param>
        /// <param name="item"> indexi blunacak item </param>
        /// <typeparam name="T"> arrayin tipi </typeparam>
        /// <returns></returns>
        public static int FindArrayIndex<T>(this T[] array, T item)
        {
            return Array.FindIndex(array, val => val.Equals(item));
        }
    }
    #endregion
    //============================================================================



    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    //TransformExtensionMethods=================================================//
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    #region "Transformlar ile ilgili extension metodlar"
    public static class TransformExtensionMethods
    {
        /// <summary>
        /// Transform verilerini arguman olarak verilen transform ile aynı yapar
        /// </summary>
        /// <param name="fromTR"></param>
        /// <param name="toTR"></param>
        public static void TransformCopy(this Transform fromTR, Transform toTR)
        {
            if (fromTR == null)
            {
                return;
            }

            if (toTR == null)
            {
                return;
            }

            fromTR.position = toTR.position;
            fromTR.localScale = toTR.localScale;
            fromTR.rotation = toTR.rotation;
        }
    }
    #endregion
    //============================================================================



    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    //ClassObjectExtensionMethods===============================================//
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    #region "Class objeleri ile ilgili extension metodlar"
    public static class ClassObjectExtensionMethods
    {
        /// <summary>
        /// Class objesini clonelar
        /// </summary>
        /// <param name="obj"> clonelanacak obje </param>
        /// <typeparam name="T"> classın kendisi </typeparam>
        /// <returns></returns>
        public static T CreateDeepCopy<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(ms);
            }
        }
    }
    #endregion
    //============================================================================



    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    //LayerMaskExtensionMethods=================================================//
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    #region "LayerMask ile ilgili extension metodlar"
    public static class LayerMaskExtensionMethods
    {
        /// <summary>
        /// verilen layeri int degerine döndürür
        /// </summary>
        /// <param name="layerMask"></param>
        /// <returns></returns>
        public static int LayerMask2Int(LayerMask layerMask)
        {
            return (int)Mathf.Log(layerMask.value, 2);
        }
    }
    #endregion
    //============================================================================



    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    //FloatExtensionMethods=====================================================//
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    #region "Float ile ilgili extension metodlar"
    public static class FloatExtensionMethods
    {
        /// <summary>
        /// flaot degerin a-b aralığından x-y aralığına remapler
        /// </summary>
        /// <param name="value"></param>
        /// <param name="from1"></param>
        /// <param name="to1"></param>
        /// <param name="from2"></param>
        /// <param name="to2"></param>
        /// <returns></returns>
        public static float FloatRemap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        //......................................................................//

        /// <summary>
        /// verilen max min degerlerin yüzdelik degerini hesaplar
        /// </summary>
        /// <param name="percent">yüzde kaçını istiyorsun</param>
        /// <param name="minValue">min deger</param>
        /// <param name="maxValue">max deger</param>
        /// <returns></returns>
        public static float FloatPercentRate(float percent, float minValue, float maxValue)
        {
            return minValue + ((maxValue - minValue) * (percent / 100));
        }
    }
    #endregion
    //============================================================================



    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    //ColorExtensionMethods=====================================================//
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    #region "Color ile ilgili extension metodlar"
    public static class ColorExtensionMethods
    {
        /// <summary>
        /// verilen max min renklerine göre yüzdelik degerdeki rengi hesaplar
        /// </summary>
        /// <param name="percent"> yüzde kaçını istiyorsun </param>
        /// <param name="minColor"> max Color </param>
        /// <param name="maxColor"> min Color </param>
        /// <returns></returns>
        public static Color32 ColorPercentRate(float percent, Color32 minColor, Color32 maxColor)
        {
            float r = FloatExtensionMethods.FloatPercentRate(percent, minColor.r, maxColor.r) / 256;
            float g = FloatExtensionMethods.FloatPercentRate(percent, minColor.g, maxColor.g) / 256;
            float b = FloatExtensionMethods.FloatPercentRate(percent, minColor.b, maxColor.b) / 256;
            float a = FloatExtensionMethods.FloatPercentRate(percent, minColor.a, maxColor.a) / 256;

            return new Color(r, g, b, a);
        }
    }
    #endregion
    //============================================================================



    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    //StringExtensionMethods====================================================//
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    #region "String ile ilgili extension metodlar"
    public static class StringExtensionMethods
    {
        /// <summary>
        /// string içerisindeki arguman olarka verilen karakteri siler
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="removeCharacter"></param>
        /// <returns></returns>
        public static string[] StringSplit(this string strValue, string removeCharacter)
        {
            return strValue.Split(new string[] { removeCharacter }, StringSplitOptions.None);
        }

        //......................................................................//

        /// <summary>
        /// string içerisindeki verilen indexler arasındaki karakterleri siler
        /// </summary>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string Slice(this string source, int start, int end)
        {
            int sourceLength = source.Length;

            string newString = "";

            if (start < 0)
            {
                start = 0;
            }

            for (int i = start; i < end; i++)
            {
                if (i >= sourceLength)
                {
                    return newString;
                }

                newString += source[i];
            }

            return newString;
        }
    }
    #endregion
    //============================================================================
}