
using System;
using DG.Tweening;
using UnityEngine;

namespace Managers
{
    public class SwapManager : MonoBehaviour
    {
        public static bool IsSwapping = false;
        public static event Action<GameObject,GameObject> SwapCompleteUpdateGrid;

        private void OnEnable()
        {
            SelectionManager.ReadyForSwap += SwapElements;
        }

        private void OnDisable()
        {
            SelectionManager.ReadyForSwap -= SwapElements;
        }
        
        private void SwapElements(GameObject element1, GameObject element2)
        {
            IsSwapping = true;
            var sequence = DOTween.Sequence();
            sequence.Append(element1.transform.DOMove(element2.transform.position, 0.3f))
                .SetEase(Ease.Flash).Join(element2.transform.DOMove(element1.transform.position, 0.3f))
                .SetEase(Ease.Flash).OnComplete(OnSwapCompleteUpdateGrid(element1,element2)).OnComplete(() => IsSwapping = false);
        }
        private TweenCallback OnSwapCompleteUpdateGrid(GameObject obj1,GameObject obj2)
        {
            SwapCompleteUpdateGrid?.Invoke(obj1,obj2);
            return null;
        }
    }
}