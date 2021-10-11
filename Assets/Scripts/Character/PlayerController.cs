using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    // Handle player physics
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        BoxCollider2D _boxCollider;
        [SerializeField] float _verticalCheckDistance;
        void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        void Update()
        {
            VerticalCollisionCheck(true);
            VerticalCollisionCheck(false);
            
        }
        public bool VerticalCollisionCheck(bool above)
        {
            Vector3 size = _boxCollider.size;
            Vector3 center = _boxCollider.bounds.center;
            Vector3 direction = above ? Vector3.up : Vector3.down;
            Vector3 middle = center + direction * size.y * 0.5f;

            Vector3[] raycast = new Vector3[3];
            raycast[0] = middle + Vector3.left * size.x * 0.5f;
            raycast[1] = middle;
            raycast[2] = middle + Vector3.right * size.x * 0.5f;
            for(int i = 0; i < raycast.Length; i++)
            {
                Debug.DrawRay(raycast[i], direction * _verticalCheckDistance);
            }
            return false;
        }
    }
}
