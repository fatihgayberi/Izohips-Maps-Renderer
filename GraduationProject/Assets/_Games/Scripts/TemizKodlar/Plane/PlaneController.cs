using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Wonnasmith
{
    public class PlaneController : MonoBehaviour
    {
        [SerializeField] private Material material;

        [SerializeField] private Transform planeObjectTR;
        [SerializeField][Range(0, 1)] private float percent;

        private WonnaTransform _wonnaTransform;

        private const string strTexture = "_MainTex";

        private Texture _texture;

        //============================================================================

        private void Awake()
        {
            _wonnaTransform = new WonnaTransform(planeObjectTR);
            _texture = material.GetTexture(strTexture);
        }

        //============================================================================

        private void OnEnable()
        {
            ButtonManager.planeResizeButtonClick += OnPlaneResizeButtonClick;
        }

        private void OnDisable()
        {
            ButtonManager.planeResizeButtonClick -= OnPlaneResizeButtonClick;
        }

        //============================================================================

        private void OnPlaneResizeButtonClick()
        {
            ImageWithPlaneResize();
        }

        //============================================================================

        private void ImageWithPlaneResize()
        {
            if (_texture == null)
            {
                return;
            }

            if (planeObjectTR == null)
            {
                return;
            }

            Vector3 newScale = Vector3.zero;

            newScale.x = _texture.width;
            newScale.y = _texture.height;
            newScale.z = 1;

            planeObjectTR.localScale = newScale * percent;
        }

        //============================================================================
    }
}