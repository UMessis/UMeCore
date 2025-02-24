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
    using Pool;
    using Saves;

    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(Initialize());
        }

        private IEnumerator Initialize()
        {
            RecordHub.InitializeRecords();
            SaveSystem.Initialize();
            PoolSystem.Instance.Initialize();
            ServiceInitializer serviceInitializer = new();
            yield return serviceInitializer.InitializeServices();
            MessageSender.Send(BootMessages.InitializationComplete);
        }
    }
}