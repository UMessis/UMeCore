namespace UMeGames.Core.Boot
{
    using System.Collections;
    using UMeGames.Core.Records;
    using UMeGames.Core.Services;
    using UMeGames.Core.Views;
    using UMeGames.Game.Views;
    using UnityEngine;

    public class Bootstrap : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(Initialize());
        }

        private IEnumerator Initialize()
        {
            RecordHub.InitializeRecords();
            yield return ServiceInitializer.InitializeServices();
            yield return ViewManager.Instance.Initialize();
            ViewManager.Instance.OpenView<SampleView>();
        }
    }
}