using System;
using Objects;

namespace Managers
{
    public class EventManager : Singleton<EventManager>
    {
        public event Action<Gem,Gem> SwapGems;

        public void OnSwapGems(Gem gem1,Gem gem2)
        {
            SwapGems?.Invoke(gem1,gem2);
        }
    }
}