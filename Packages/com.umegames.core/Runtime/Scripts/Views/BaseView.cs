namespace UMeGames.Core.Views
{
    using UnityEngine;

    public abstract class BaseView : MonoBehaviour
    {
        // todo : make this a const with a name instead of an int
        public abstract int ViewPriority { get; }

        public abstract void OnViewClosed();
    }
}