namespace UMeGames.Core.Boot
{
    using System.Collections;
    using Messages;
    using UMeGames.Core.Records;
    using UMeGames.Core.Services;
    using UMeGames.Core.Views;
    using UnityEngine;
    using MessageSender;

    public class Bootstrap : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(Initialize());
        }

        private IEnumerator Initialize()
        {
            RecordHub.InitializeRecords();
            ServiceInitializer serviceInitializer = new();
            yield return serviceInitializer.InitializeServices();
            ViewManager.Instance.Initialize();
            MessageSender.Send(BootMessages.InitializationComplete);
        }
    }
}