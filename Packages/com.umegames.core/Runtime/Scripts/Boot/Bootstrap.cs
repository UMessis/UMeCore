namespace UMeGames.Core.Boot
{
    using System;
    using System.Collections;
    using UMeGames.Core.CoroutineRunner;
    using UMeGames.Core.Logger;
    using UMeGames.Core.Records;
    using UMeGames.Core.Services;
    using UMeGames.Core.Views;
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
            RecordHub.InitializeRecords();
            var serviceInitializer = new ServiceInitializer();
            yield return serviceInitializer.InitializeServices();
            ViewManager.Instance.Initialize();
            OnInitializationComplete?.Invoke();

            CoroutineRunner.StartRoutine(Test());
        }

        IEnumerator Test()
        {
            yield return new WaitForSeconds(2);

            this.Log("Test");
        }
    }
}