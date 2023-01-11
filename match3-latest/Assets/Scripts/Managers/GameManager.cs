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
    
        private void Start()
        {
            onGemSelected.AddListener(SwapGems);
        }

        private void SwapGems(SelectedGems gems)
        {
            gemsAreSwapping = true;
            gems.Gem1.transform.DOMove(gems.Gem2.transform.position, 1f);
            gems.Gem2.transform.DOMove(gems.Gem1.transform.position, 1f).OnComplete(() => gemsAreSwapping = false);
        }

    }
}