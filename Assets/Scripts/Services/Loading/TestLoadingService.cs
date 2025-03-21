namespace UMeGames
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Core.Messages;
    using Core.MessageSender;
    using Core.Pool;
    using Core.Records;
    using Core.Saves;
    using Core.Services;
    using Core.Views;
    using UnityEngine.SceneManagement;

    public class TestLoadingService : IService, IMessageReceiver
    {
        public List<Type> Dependencies => null;

        private TestSaveComponent saveComponent;
        private readonly Type[] subscribedMessages =
        {
            typeof(BootMessages)
        };
        
        public IEnumerator Initialize(List<IService> dependencies)
        {
            MessageSender.Register(this, subscribedMessages);
            saveComponent = SaveSystem.GetSaveComponent<TestSaveComponent>();
            yield break;
        }

        public void Dispose()
        {
            MessageSender.Unregister(this, subscribedMessages);
            PoolSystem.Instance.OnContextLoaded -= OnPoolContextLoaded;
        }

        public void ReceiveMessage(object message, object[] data)
        {
            if (message is BootMessages bootMessage)
            {
                switch (bootMessage)
                {
                    case BootMessages.InitializationComplete:
                        SceneManager.LoadScene("SampleGame");
                        saveComponent.SetTestFloat(3f);
                        PoolSystem.Instance.LoadContext("Test");
                        PoolSystem.Instance.OnContextLoaded += OnPoolContextLoaded;
                        ViewManager.Instance.OpenView<TestView>();
                        break;
                }
            }
        }

        private void OnPoolContextLoaded(string context)
        {
            if (context == "Test")
            {
                PoolSystem.Instance.GetObjectOfType<TestCubePoolItem>();
            }
        }
    }
}