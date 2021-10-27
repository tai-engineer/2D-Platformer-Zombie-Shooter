using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class ObjectPool : MonoBehaviour
    {
        List<GameObject> _instances;
        [SerializeField] int _initialPoolSize;
        [SerializeField] GameObject _prefab;
        void Awake()
        {
            _instances = new List<GameObject>();
            for (int i = 0; i < _initialPoolSize; i++)
            {
                Instantiate();
            }
        }

        void Instantiate()
        {
            GameObject obj = Instantiate(_prefab);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            _instances.Add(obj);
        }

        public void Pop(Vector2 position, bool useLocalPosition)
        {
            Pop(position, 1, useLocalPosition);
        }

        public void Pop(Vector2 position, int amount, bool useLocalPosition)
        {
            int counter = 0;
            for (int i = 0; i < _instances.Count; i++)
            {
                if (counter == amount)
                    return;
                if (!_instances[i].activeSelf)
                {
                    Pop(_instances[i], position, useLocalPosition);
                    counter++;
                }
            }
        }
        public void Pop(GameObject obj, Vector2 position, bool useLocalPosition)
        {
            if (useLocalPosition)
            {
                obj.transform.localPosition = position;
            }
            else
            {
                obj.transform.position = position;
            }
            obj.SetActive(true);
        }
    }
}