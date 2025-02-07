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
    using static Core.Logger.Logger;

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
        }

        public void ReceiveMessage(object message, object[] data)
        {
            if (message is BootMessages bootMessage)
            {
                switch (bootMessage)
                {
                    case BootMessages.InitializationComplete:
                        saveComponent.SetTestFloat(3f);
                        Log($"Test float saved value: {saveComponent.GetTestFloat()}");
                        ViewManager.Instance.OpenView<TestView>();
                        break;
                }
            }
        }
    }
}