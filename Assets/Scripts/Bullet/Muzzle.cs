using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class Muzzle : MonoBehaviour
    {
        public bool startingFacingLeft = false;

        public void Enable()
        {
            gameObject.SetActive(true);
        }
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
