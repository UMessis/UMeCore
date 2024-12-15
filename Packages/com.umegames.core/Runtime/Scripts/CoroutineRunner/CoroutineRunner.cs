namespace UMeGames.Core.CoroutineRunner
{
    using System.Collections;
    using System.Collections.Generic;
    using UMeGames.Core.Singleton;
    using UnityEngine;

    public class CoroutineRunner : MonoSingleton<CoroutineRunner>
    {
        static List<Coroutine> routines = new();

        void OnDestroy()
        {
            foreach (Coroutine routine in routines)
            {
                StopCoroutine(routine);
            }
        }

        public static void StartRoutine(IEnumerator routine)
        {
            Coroutine coroutine = Instance.StartCoroutine(routine);
            routines.Add(coroutine);
        }
    }
}
