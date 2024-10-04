namespace UMeGames.Core.Boot
{
    using System;
    using System.Collections;
    using UMeGames.Core.Services;
    using UnityEngine;

    public class Bootstrap : MonoBehaviour
    {
        public Action OnInitializationComplete;

        void Awake()
        {
            StartCoroutine(Initialize());
        }

        IEnumerator Initialize()
        {
            yield return ServiceInitializer.InitializeServices();
            OnInitializationComplete?.Invoke();
        }
    }
}