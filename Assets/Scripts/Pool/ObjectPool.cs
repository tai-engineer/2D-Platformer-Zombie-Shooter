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
            for(int i = 0; i < _instances.Count; i++)
            {
                if(!_instances[i].activeSelf)
                {
                    GameObject obj = _instances[i];
                    if (useLocalPosition)
                    {
                        obj.transform.localPosition = position;
                    }
                    else
                    {
                        obj.transform.position = position;
                    }
                    obj.SetActive(true);
                    return;
                }
            }

            //Instantiate();
        }
    }
}