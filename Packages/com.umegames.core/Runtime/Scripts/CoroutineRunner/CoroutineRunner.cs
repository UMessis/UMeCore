namespace UMeGames.Core.CoroutineRunner
{
    using System.Collections;
    using System.Collections.Generic;
    using Singleton;
    using UnityEngine;

    public class CoroutineRunner : MonoSingleton<CoroutineRunner>
    {
        private static readonly List<Coroutine> routines = new();

        private void OnDestroy()
        {
            foreach (Coroutine routine in routines)
            {
                StopCoroutine(routine);
            }
        }

        public static Coroutine StartRoutine(IEnumerator routine)
        {
            Coroutine coroutine = Instance.StartCoroutine(routine);
            routines.Add(coroutine);
            return coroutine;
        }
        
        public static void StopRoutine(Coroutine routine)
        {
            if (!routines.Contains(routine)) { return; }
            Instance.StopCoroutine(routine);
            routines.Remove(routine);
        }
    }
}