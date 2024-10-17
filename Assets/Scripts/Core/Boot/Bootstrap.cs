namespace UMeGames.Core.Boot
{
    using System;
    using System.Collections;
    using UMeGames.Core.Records;
    using UMeGames.Core.Services;
    using UMeGames.Core.Views;
    using UMeGames.Game.Views;
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

            ViewManager.Instance.OpenView<SampleView>();
        }
    }
}