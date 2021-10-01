using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class UIMainMenu : MonoBehaviour
    {
        const int SCENESTART_INDEX = 1;
        public void Play()
        {
            SceneController.Instance.LoadScene(SCENESTART_INDEX);
        }
    }
}
