
using DG.Tweening;
using Objects;
using UnityEngine.Events;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public readonly SelectedGems SelectedGems = new SelectedGems();
        public UnityEvent<SelectedGems> onGemSelected = new UnityEvent<SelectedGems>();
        public bool gemsAreSwapping = false;
        public event UnityAction<Gem,Gem> UpdateGem;
    
        private void OnEnable()
        {
            onGemSelected.AddListener(SwapGems);
        }

        private void OnDisable()
        {
            onGemSelected.RemoveListener(SwapGems);
        }

        private void SwapGems(SelectedGems gems)
        {
            gemsAreSwapping = true;
            gems.Gem1.transform.DOMove(gems.Gem2.transform.position, 1f).OnComplete(() => OnUpdateGem(SelectedGems.Gem1, SelectedGems.Gem2));
            gems.Gem2.transform.DOMove(gems.Gem1.transform.position, 1f).OnComplete(() => gemsAreSwapping = false);
        }

        private void OnUpdateGem(Gem gem1,Gem gem2)
        {
            UpdateGem?.Invoke(gem1,gem2);
            
        }
        
        
    }
}