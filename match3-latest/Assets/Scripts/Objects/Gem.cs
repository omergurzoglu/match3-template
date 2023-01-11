using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Objects
{
    public class Gem : MonoBehaviour
    {
        [SerializeField] private GemSo gemSo;
        private GemSo.GemType _gemType;

        private void Awake()
        {
            _gemType = gemSo.gemColor;
        }

        private void OnMouseDown()
        {
            if (GameManager.Instance.gemsAreSwapping) return;
            if (!GameManager.Instance.SelectedGems.SecondGemSelected)
            {
                GameManager.Instance.SelectedGems.Gem1 = this;
                GameManager.Instance.SelectedGems.SecondGemSelected = true;
            }
            else
            {
            
                GameManager.Instance.SelectedGems.Gem2 = this;
                GameManager.Instance.onGemSelected.Invoke(GameManager.Instance.SelectedGems);
                GameManager.Instance.SelectedGems.SecondGemSelected = false;
            }
        }
    }
}
