namespace UMeGames.Core.Boot
{
    using System.Collections;
    using System.Threading.Tasks;
    using Messages;
    using UMeGames.Core.Records;
    using UMeGames.Core.Services;
    using UMeGames.Core.Views;
    using UnityEngine;
    using MessageSender;
    using Saves;

    public class Bootstrap : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(Initialize());
        }

        private IEnumerator Initialize()
        {
            RecordHub.InitializeRecords();
            SaveSystem.Initialize();
            ServiceInitializer serviceInitializer = new();
            yield return serviceInitializer.InitializeServices();
            ViewManager.Instance.Initialize();
            MessageSender.Send(BootMessages.InitializationComplete);
        }
    }
}