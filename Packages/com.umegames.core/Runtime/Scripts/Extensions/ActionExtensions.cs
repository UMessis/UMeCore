namespace UMeGames.Core.Extensions
{
    using System;

    public static class ActionExtensions
    {
        public static void SafeInvoke(this Action action)
        {
            if (action != null)
            {
                action.Invoke();
            }
        }
    }
}