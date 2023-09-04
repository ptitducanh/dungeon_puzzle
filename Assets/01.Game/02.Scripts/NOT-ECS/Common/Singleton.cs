using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.Common
{
    public class Singleton<T> : SerializedMonoBehaviour where T : Singleton<T>
    {
        private static T _instance;
        public static T Instance => _instance;

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = (T) this;
        }
    }
}