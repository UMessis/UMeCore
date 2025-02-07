namespace UMeGames
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Core.Messages;
    using Core.MessageSender;
    using Core.Saves;
    using Core.Services;
    using Core.Views;
    using UnityEngine;

    public class TestLoadingService : IService, IMessageReceiver
    {
        private readonly Type[] subscribedMessages =
        {
            typeof(BootMessages)
        };
        
        public List<Type> Dependencies => null;
        
        public IEnumerator Initialize(List<IService> dependencies)
        {
            MessageSender.Register(this, subscribedMessages);
            // TODO : Cant get save component, gives error
            // TestSaveComponent testSaveComponent = SaveSystem.GetSaveComponent<TestSaveComponent>();
            // float testFloat = testSaveComponent.GetTestFloat();
            // Debug.Log(testFloat);
            yield break;
        }

        public void Dispose()
        {
            MessageSender.Unregister(this, subscribedMessages);
        }

        public void ReceiveMessage(object message, object[] data)
        {
            if (message is BootMessages bootMessage)
            {
                switch (bootMessage)
                {
                    case BootMessages.InitializationComplete:
                        ViewManager.Instance.OpenView<TestView>();
                        break;
                }
            }
        }
    }
}