using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Wonnasmith
{
    [Serializable]
    public class WonnaTransform
    {
        Vector3 _position;
        Vector3 _loaclScale;
        Quaternion _rotation;

        public WonnaTransform()
        {
            _position = Vector3.zero;
            _loaclScale = Vector3.zero;
            _rotation = Quaternion.Euler(Vector3.zero);
        }

        public WonnaTransform(Transform objTR)
        {
            _position = objTR.position;
            _loaclScale = objTR.localScale;
            _rotation = objTR.rotation;
        }
    }
}