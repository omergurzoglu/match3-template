
using System;
using Objects;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public class SelectionManager : MonoBehaviour
    { 
        [SerializeField] private GameObject selection1;
        [SerializeField] private GameObject selection2;
        public static event Action<GameObject,GameObject> ReadyForSwap;
        private void OnEnable()
        {
            Gem.GemSelected += Select;
        }

        private void OnDisable()
        {
            Gem.GemSelected -= Select;
        }

        private void Select(GameObject gem)
        {
            Debug.Log("select worked");
            if (selection1 == null)
            {
                Debug.Log("selection 1 is null , selecting");
                selection1 = gem;
                Debug.Log("selection 1 confirmed"+selection1.name);
            }
            else if (selection2 == null)
            {
                selection2 = gem;
                OnReadyForSwap(selection1, selection2);
                selection1 = null;
                selection2 = null;
            }

            // else if(selection1 != null && selection2 != null)
            // {
            //     selection1 = null;
            //     selection2 = null;
            // }
            
        }
        
        private static void OnReadyForSwap(GameObject obj1,GameObject obj2)
        {
            
            ReadyForSwap?.Invoke(obj1,obj2);
        }
    }
    
}