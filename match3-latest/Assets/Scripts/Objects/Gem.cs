
using System;
using Managers;
using ScriptableObjects;
using UnityEngine;


namespace Objects
{
    public class Gem : MonoBehaviour
    {
        [SerializeField] private GemSo gemSo;
        public GemSo.GemType gemType;

        public static event Action<GameObject> GemSelected;
        private void Awake()
        {
            gemType = gemSo.gemType;
        }
        private void OnMouseDown()
        {
            OnGemSelected(gameObject);
        }
        
        private static void OnGemSelected(GameObject gem)
        {
            if (!SwapManager.IsSwapping&&!GridManager.GridIsUpdating)
            {
                GemSelected?.Invoke(gem);
            }
           
        }
    }

   
}
