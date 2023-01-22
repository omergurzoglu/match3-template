
using System;
using Objects;
using UnityEngine;

namespace Managers
{
    public class SelectionManager : MonoBehaviour
    { 
        [SerializeField] private GameObject selection1;
        [SerializeField] private GameObject selection2;
        public static event Action<GameObject,GameObject> ReadyForSwap;
        private void OnEnable() => Gem.GemSelected += Select;

        private void OnDisable() => Gem.GemSelected -= Select;

        private void Select(GameObject gem)
        {
            if (selection1 == null)
            {
                selection1 = gem;
            }
            else if (selection2 == null)
            {
                Vector2Int pos1 = GridManager.GetVector2OfElement(selection1);
                Vector2Int pos2 = GridManager.GetVector2OfElement(gem);
                if (Mathf.Abs(pos1.x - pos2.x) <= 1 && Mathf.Abs(pos1.y - pos2.y) <= 1)
                {
                    selection2 = gem;
                    OnReadyForSwap(selection1, selection2);
                    selection1 = null;
                    selection2 = null;
                }
                else
                {
                    
                    Debug.Log("move not allowed");
                }
            }
        }
        
        private static void OnReadyForSwap(GameObject obj1,GameObject obj2) => ReadyForSwap?.Invoke(obj1,obj2);
    }
    
}