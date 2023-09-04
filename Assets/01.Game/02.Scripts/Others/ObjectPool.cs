using System;
using System.Collections.Generic;
using Scripts.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.Others
{
    /// <summary>
    /// A simple object pool implementation.
    /// </summary>
    public class ObjectPool : Singleton<ObjectPool>
    {
        [SerializeField]            private GameObject[]                         prefabs;
        [SerializeField] [ReadOnly] private Dictionary<string, List<GameObject>> _pool = new();

        private Dictionary<string, GameObject> _prefabMap = new();

        private void Start()
        {
            InitializePool();
        }

        public GameObject Get(string name)
        {
            if (!_pool.ContainsKey(name))
            {
                _pool.Add(name, new List<GameObject>());
            }

            if (_pool[name].Count == 0)
            {
                var prefab = _prefabMap[name];
                var go = Instantiate(prefab);
                go.name = name;
                go.SetActive(true);
                return go;
            }

            var last = _pool[name].Count - 1;
            var result = _pool[name][last];
            _pool[name].RemoveAt(last);
            result.SetActive(true);
            return result;
        }
        
        public void Return(GameObject go)
        {
            if (!_pool.ContainsKey(go.name))
            {
                _pool.Add(go.name, new List<GameObject>());
            }

            _pool[go.name].Add(go);
            go.SetActive(false);
            go.transform.SetParent(null);
        }

        private void InitializePool()
        {
            foreach (var prefab in prefabs)
            {
                _prefabMap.Add(prefab.name, prefab);
                for (int i = 0; i < 10; i++)
                {
                    var itemObject = Instantiate(prefab);
                    itemObject.name = prefab.name;
                    Return(itemObject);
                }
            }
        }
    }
}