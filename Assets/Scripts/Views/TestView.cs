namespace UMeGames
{
    using UMeGames.Core.Views;
    using UnityEngine;

    public class TestView : BaseView
    {
        public override int ViewPriority => 0;

        public override void OnViewClosed()
        {
        }
    }
}
