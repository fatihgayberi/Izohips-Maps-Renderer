using UnityEngine;

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
#region "REFERENCES"
//~~~// https://www.alanzucconi.com/2015/12/16/color-blindness/
//~~~// https://web.archive.org/web/20081014161121/http://www.colorjack.com/labs/colormatrix/
#endregion
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

using System;

namespace Wonnasmith
{

    [Serializable]
    [ExecuteInEditMode]
    public class ColorRange : MonoBehaviour
    {
        public bool showDifference = false;

        public Material material;

        [Serializable]
        public class ColorDatas
        {
            public Color R = new Color(.618f, .32f, .062f);
            public Color G = new Color(.163f, .775f, .062f);
            public Color B = new Color(.163f, .320f, .516f);
        }

        public ColorDatas colorDatas;

        void Awake()
        {
            // material = new Material(Shader.Find("Wonnasmith_Object/ChanelMixer"));
            // material.SetColor("_R", colorDatas.R);
            // material.SetColor("_G", colorDatas.G);
            // material.SetColor("_B", colorDatas.B);
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (material == null)
            {
                Debug.LogError("Materila not found");
                return;
            }

            material.SetColor("_R", colorDatas.R);
            material.SetColor("_G", colorDatas.G);
            material.SetColor("_B", colorDatas.B);

            // Apply effect
            Graphics.Blit(source, destination, material, showDifference ? 1 : 0);
        }
    }
}