
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
        [SerializeField] private ParticleSystem prefabFx;

        public static event Action<GameObject> GemSelected;
        private void Awake() => gemType = gemSo.gemType;
        private void OnMouseDown() => OnGemSelected(gameObject);

        private static void OnGemSelected(GameObject gem)
        {
            if (!SwapManager.IsSwapping&&!GridManager.GridIsUpdating)
            {
                GemSelected?.Invoke(gem);
            }
           
        }
       
        private void OnDisable()
        {
            ParticleSystem fx = Instantiate(prefabFx, transform.position, Quaternion.identity);
            fx.Play();
        }
    }

   
}
