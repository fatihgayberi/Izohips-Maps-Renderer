using UnityEngine;
using System.Collections;

namespace Wonnasmith
{
    public abstract class Singleton<T> : MonoBehaviour
    {
        //Public Properties:
        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("Singleton not registered! Make sure the GameObject running your singleton is active in your scene and has an Initialization component attached.");
                    return default(T);
                }
                return _instance;
            }
        }

        //Private Variables:
        [SerializeField] bool _dontDestroyOnLoad = false;
        static T _instance;

        //Private Methods:
        protected void Initialize(T instance)
        {
            if (_instance != null)
            {
                //there is already an instance:
                Destroy(gameObject);
                return;
            }

            if (_dontDestroyOnLoad)
            {
                //don't destroy on load only works on root objects so let's force this transform to be a root object:
                transform.parent = null;
                DontDestroyOnLoad(gameObject);
            }

            _instance = instance;
        }
    }
}