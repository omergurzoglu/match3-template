
using UnityEngine;

namespace Managers
{
    public class Display : MonoBehaviour
    { 
        private void Awake() => DontDestroyOnLoad(this);
        private void Start()
        {
            Application.targetFrameRate = 60;
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
    }
}